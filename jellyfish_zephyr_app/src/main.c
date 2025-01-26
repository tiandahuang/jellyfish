/* main.c - Application main entry point */

/*
 * Copyright (c) 2023 Nordic Semiconductor ASA
 *
 * SPDX-License-Identifier: Apache-2.0
 */

#include "peripheral_mtu_update.h"
#include "app_packet.h"
#include "emg.h"
#include "haptics.h"
#include "heartbeat.h"
#include "butterworth.h"

#include <stdint.h>
#include <stdbool.h>

#include <zephyr/kernel.h>
#include <zephyr/sys/printk.h>
#include <zephyr/drivers/gpio.h>

#define ADC_SAMPLE_BUFFER_SIZE 64
#define BUZZER_PERIOD 1000
#define BUZZER_INTENSITY 500
#define BUZZER_THRESH 0x40

static void do_we_buzz(uint8_t power)
{
	if (power > BUZZER_THRESH) {
		gpio_pin_set_dt(&leds[LED_GREEN], 1);
		buzzer_on(BUZZER_INTENSITY);
	} else {
		gpio_pin_set_dt(&leds[LED_GREEN], 0);
		buzzer_off();
	}
}

int main(void)
{
	int i = 0;
	ble_packet_t packet = {0};
	int16_t adc_value = 0;
	int32_t adc_filt_values[ADC_SAMPLE_BUFFER_SIZE] = {0};
	int32_t adc_filt_filt_max = 0;
	int32_t adc_filt_max = 0;
	uint8_t power_rough = 0;

	heartbeat_init(200, LED_BLUE);
	send_peripheral_init();
	adc_init();
	buzzer_init(BUZZER_PERIOD);
	filter_init();

	while (true) {
		heartbeat_beat();
		
		if (adc_sample(&adc_value)) runtime_error();
		adc_filt_values[i % ADC_SAMPLE_BUFFER_SIZE] = (int32_t)adc_value;

		if (i % ADC_SAMPLE_BUFFER_SIZE == 0) {
			// we've filled the buffer, so average and ship it
			adc_filt_max = array_max(adc_filt_values, ADC_SAMPLE_BUFFER_SIZE);
			adc_filt_filt_max = filter(adc_filt_max);
			power_rough = map_to_u8(adc_filt_filt_max, 0, 255);

			// send packet & do haptics response
			do_we_buzz(power_rough);
			packet.data.gesture = power_rough;
			send_peripheral_sample(packet.raw, sizeof(packet.raw), 0);
		}

		i++;
		k_sleep(K_MSEC(1));
	}
	
	return 0;
}

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

#include <stdint.h>
#include <stdbool.h>

#include <zephyr/kernel.h>
#include <zephyr/sys/printk.h>
#include <zephyr/drivers/gpio.h>

int main(void)
{
	int i = 0;
	int buzzer_status = 0;
	ble_packet_t packet = {0};
	int16_t adc_value = 0;

	heartbeat_init(100, LED_BLUE);
	send_peripheral_init();
	adc_init();
	buzzer_init(1000);

	while (true) {
		heartbeat_beat();
		
		if (adc_sample(&adc_value)) runtime_error();

		packet.data.gesture = (uint16_t)adc_value;

		if (i % 100 == 0) {
			send_peripheral_sample(packet.raw, sizeof(packet.raw), 0);
		}
		if (i % 1000 == 0) {
			buzzer_status ^= 0x1;
			if (buzzer_status) {
				gpio_pin_set_dt(&leds[LED_GREEN], 1);
				buzzer_on(500);
			} else {
				gpio_pin_set_dt(&leds[LED_GREEN], 0);
				buzzer_off();
			}
		}

		i++;
		k_sleep(K_MSEC(1));
	}
	
	return 0;
}

/* main.c - Application main entry point */

/*
 * Copyright (c) 2023 Nordic Semiconductor ASA
 *
 * SPDX-License-Identifier: Apache-2.0
 */

#include "peripheral_mtu_update.h"
#include "app_packet.h"

#include <stdint.h>
#include <stdbool.h>

#include <zephyr/kernel.h>
#include <zephyr/sys/printk.h>

static void gen_sample_ble_packet(ble_packet_t *pack) {
	pack->data.gesture += 1;
	pack->data.accel.x += 2;
	pack->data.accel.y += 2;
	pack->data.accel.z += 2;
	pack->data.gyro.x += 3;
	pack->data.gyro.y += 3;
	pack->data.gyro.z += 3;
}

int main(void)
{
	int i = 0;
	ble_packet_t packet = {0};

	send_peripheral_init();

	while (true) {
		gen_sample_ble_packet(&packet);
		send_peripheral_sample(packet.raw, sizeof(packet.raw), 0);
		printk("Sent BLE packet %d\n", i++);
		k_sleep(K_MSEC(100));
	}
	
	return 0;
}

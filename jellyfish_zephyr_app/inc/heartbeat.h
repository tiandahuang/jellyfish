#pragma once

#include <zephyr/drivers/gpio.h>
#include <stdint.h>

enum board_led {
    LED_RED = 0,
    LED_GREEN,
    LED_BLUE,
    LED_NUM_LEDS,
};

extern const struct gpio_dt_spec leds[LED_NUM_LEDS];

int heartbeat_init(uint32_t pd_iters, int hb_led);
int heartbeat_beat(void);
void runtime_error(void);

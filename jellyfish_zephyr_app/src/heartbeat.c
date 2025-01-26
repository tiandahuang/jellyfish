#include "heartbeat.h"

#include <zephyr/kernel.h>


#define LED0_NODE DT_ALIAS(led0)
#define LED1_NODE DT_ALIAS(led1)
#define LED2_NODE DT_ALIAS(led2)

const struct gpio_dt_spec leds[LED_NUM_LEDS] = {
	[LED_RED] = GPIO_DT_SPEC_GET(LED0_NODE, gpios),
	[LED_GREEN] = GPIO_DT_SPEC_GET(LED1_NODE, gpios),
	[LED_BLUE] = GPIO_DT_SPEC_GET(LED2_NODE, gpios),
};

static uint32_t heartbeat_pd_iters;
static int heartbeat_led;

void runtime_error(void)
{
	gpio_pin_set_dt(&leds[LED_RED], 1);
	k_sleep(K_FOREVER);
}

int heartbeat_init(uint32_t pd_iters, int hb_led)
{
    heartbeat_pd_iters = pd_iters;
    heartbeat_led = hb_led;

    for (int i = 0; i < ARRAY_SIZE(leds); i++) {
        if (!gpio_is_ready_dt(&leds[i])) {
            return 1;
        }
        gpio_pin_configure_dt(&leds[i], GPIO_OUTPUT_ACTIVE);
        gpio_pin_set_dt(&leds[i], 0);
    }

    return 0;
}

int heartbeat_beat(void)
{
    static int i = 0;

    if (i % heartbeat_pd_iters == 0) {
        gpio_pin_toggle_dt(&leds[heartbeat_led]);
    }

    i++;
    return 0;
}

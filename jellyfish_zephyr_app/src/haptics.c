#include "haptics.h"

#include <zephyr/drivers/pwm.h>
#include <zephyr/sys/printk.h>

static const struct pwm_dt_spec buzzer = PWM_DT_SPEC_GET(DT_NODELABEL(pwm_led0));

static uint32_t pwm_period_ns;

int buzzer_init(uint32_t pd)
{
    pwm_period_ns = pd;

    if (!pwm_is_ready_dt(&buzzer)) {
        printk("PWM device not ready\n");
        return 1;
    }

    return 0;
}

int buzzer_on(uint32_t pulse_width) {
    int err;

    err = pwm_set_dt(&buzzer, pwm_period_ns, pwm_period_ns - pulse_width);
    if (err) {
        printk("PWM set failed %d\n", err);
        return 1;
    }

    return 0;
}

int buzzer_off() {
    return buzzer_on(0);
}

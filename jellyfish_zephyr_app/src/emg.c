#include <zephyr/drivers/adc.h>
#include <zephyr/sys/printk.h>
#include "emg.h"

static const struct adc_dt_spec adc_channel = ADC_DT_SPEC_GET(DT_PATH(zephyr_user));

static int16_t adc_buffer[1];
static struct adc_sequence adc_sequence = {
    .buffer = adc_buffer,
    .buffer_size = sizeof(adc_buffer),
};

int adc_sample(int16_t *adc_value)
{
    int err = adc_read(adc_channel.dev, &adc_sequence);
    if (err) {
        printk("ADC read failed %d\n", err);
        return 1;
    }

    *adc_value = adc_buffer[0];
    return 0;
}

int adc_init(void)
{   
    int err;

    if (!adc_is_ready_dt(&adc_channel)) {
        printk("ADC device not ready\n");
        return 1;
    }

    err = adc_channel_setup_dt(&adc_channel);
    if (err < 0) {
        printk("ADC channel setup failed %d\n", err);
        return 1;
    }

    err = adc_sequence_init_dt(&adc_channel, &adc_sequence);
    if (err < 0) {
        printk("ADC sequence init failed %d\n", err);
        return 1;
    }

    return 0;
}

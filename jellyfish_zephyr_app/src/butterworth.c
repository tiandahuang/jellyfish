#include "butterworth.h"

static int32_t offset = 0;
static int32_t curr_val = 0;

int filter_init() {
    offset = 0;
    curr_val = 0;
    return 0;
}

int32_t filter(int32_t raw) {
    // calculate two EWMAs -- one for the offset and one for the value
    // advance them at different rates, so offset slowly follows the value

    offset = ((OFFSET_FILT_COEF_DEN - OFFSET_FILT_COEF_NUM) * offset
             + OFFSET_FILT_COEF_NUM * raw) / OFFSET_FILT_COEF_DEN;
    curr_val = ((VAL_FILT_COEF_DEN - VAL_FILT_COEF_NUM) * curr_val
               + VAL_FILT_COEF_NUM * raw) / VAL_FILT_COEF_DEN;

    return curr_val - offset;
}

int array_average(int32_t *buf, uint32_t len) {
    int32_t sum = 0;
    for (int i = 0; i < len; i++) {
        sum += buf[i];
    }
    return sum / len;
}

uint8_t map_to_u8(int32_t val, int32_t min, int32_t max) {
    if (val < min) {
        return 0;
    } else if (val > max) {
        return 255;
    }
    return (uint8_t)((val - min) * 255 / (max - min));
}

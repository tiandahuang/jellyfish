#pragma once

#include <stdint.h>

#define OFFSET_FILT_COEF_NUM 1
#define OFFSET_FILT_COEF_DEN 128
#define VAL_FILT_COEF_NUM 1
#define VAL_FILT_COEF_DEN 16

int filter_init();
int32_t filter(int32_t val);
int array_average(int32_t *buf, uint32_t len);
uint8_t map_to_u8(int32_t val, int32_t min, int32_t max);

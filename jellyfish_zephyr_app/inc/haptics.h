#pragma once

#include <stdint.h>

int buzzer_init(uint32_t pd);
int buzzer_on(uint32_t pulse_width);
int buzzer_off();

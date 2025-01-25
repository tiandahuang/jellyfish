#pragma once

#include <stdint.h>
#include <stddef.h>

int send_peripheral_init();
int send_peripheral_sample(uint8_t *notify_data, size_t notify_data_size, uint16_t seconds);

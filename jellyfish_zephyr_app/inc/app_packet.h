#pragma once

#include <stdint.h>
#include <assert.h>

struct __attribute__((packed)) tri_axial {
    int16_t x;
    int16_t y;
    int16_t z;
};

struct __attribute__((packed)) ble_data_fields {
    uint16_t gesture;
    struct tri_axial accel;
    struct tri_axial gyro;
};

typedef union ble_data_pack {
    struct ble_data_fields data;
    uint8_t raw[20];
} ble_packet_t;

# SPDX-License-Identifier: Apache-2.0

cmake_minimum_required(VERSION 3.20.0)

find_package(Zephyr REQUIRED HINTS $ENV{ZEPHYR_BASE})
project(mtu_update)

target_sources(app PRIVATE
  src/main.c
  src/peripheral_mtu_update.c
  src/emg.c
  src/haptics.c
  src/heartbeat.c
  src/butterworth.c
)
target_include_directories(app PRIVATE inc)

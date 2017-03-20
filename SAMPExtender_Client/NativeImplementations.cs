namespace SAMPExtender_Client
{
    class NativeImplementations
    {
        private const int HUD_MONEYCOLOR_ADDRESS = 0xBAB230;
        private const int HUD_WANTEDCOLOR_ADDRESS = 0xBAB244;
        private const int HUD_HEALTHCOLOR_ADDRESS = 0xBAB22C;
        private const int HUD_RADIOCOLOR_ADDRESS = 0xBAB24C;
        private const int VEHICLECAMERAMODE_ADDRESS = 0xB6F0DC;
        private const int AIRCRAFT_HEIGHT_ADDRESS = 0x8594DC;
        private const int INFINITE_RUN_ADDRESS = 0xB7CEE4;
        private const int PICKUP_SIZE_ADDRESS = 0x858CC8;
        private const int FIREPROOF_ADDRESS = 0xB7CEE6;
        private const int WAVE_HEIGHT_ADDRESS = 0xC812E8;
        private const int WIND_DENSITY_ADDRESS = 0xC812F0;
        private const int TOGGLE_HUD_ADDRESS = 0xB6F065;
        private const int NV_ADDRESS = 0xC402B8;
        private const int TH_ADDRESS = 0xC402B9;
        private const int GRAV_ADDRESS = 0x863984;
        private const int GRAY_RADAR_ADDRESS = 0xA444A4;
        private const int TIME_FLIES_ADDRESS = 0x96913B;
        private const int WHEELS_ONLY_ADDRESS = 0x96914B;
        private const int PLAYER_OXYGEN_ADDRESS = 0xB7CDE0;
        private const int PLAYER_INF_OXYGEN_ADDRESS = 0x96916E;
        private const int GREEN_TRAFFIC_LIGHTS_ADDRESS = 0x96914E;
        private const int PLAYER_INVISIBLE_ENTEREXITS_ADDRESS = 0x53E213;
        private const int PLAYER_RADIO_VOLUME_ADDRESS = 0xB5FCC8;
        private const int PLAYER_SFX_VOLUME_ADDRESS = 0xB5FCCC;
        private const int STRANGE_2D_COLOR_ADDRESS = 0x7170C0;
        private const int HEALTHBAR_BORDER_ADDRESS = 0x589353;
        private const int PLAYER_FOG_DENSITY_ADDRESS = 0xC81410;
        private const int PLAYER_RAIN_DENSITY_ADDRESS = 0xC81324;
        private const int PLAYER_WEIRD_DEFORM_ADDRESS = 0xC81340;
        private const int PLAYER_CAMERA_NEAR_DISTANCE_ADDRESS = 0xB6F27C;
        private const int PLAYER_CAMERA_DEFAULT_DISTANCE_ADDRESS = 0xB6F284;
        private const int PLAYER_CAMERA_FAR_DISTANCE_ADDRESS = 0xB6F280;
        private const int DISABLE_AIMING_ADDRESS = 0xB6F062;
        private const int DISABLE_ONFOOT_CAMERA_CHANGE_ADDRESS = 0xB6F060;
        private const int PLAYER_WAVE_HEIGHT_EX_ADDRESS = 0x8D38C8;
        private const int UNDERWATER_CAMERA_EFFECT_ADDRESS = 0xC402D3;
        private const int HORIZONTAL_SCREEN_LINES_ADDRESS = 0xC402C5;
        private const int PLAYER_TAKE_PHOTO_ADDRESS = 0xC8A7C1;
        private const int PLAYER_RAINING_SCREEN_EFFECT_ADDRESS = 0xC812B0;
        private const int PLAYER_BRIGHT_HORIZON_EFFECT_ADDRESS = 0xC812CC;
        private const int PLAYER_FLAME_COLOR_1_ADDRESS = 0x004A394E;
        private const int PLAYER_FLAME_COLOR_2_ADDRESS = 0x004A3967;
        private const int PLAYER_FPS_ADDRESS = 0xB7CB50;
        private const int NOISE_TH_LEVEL_ADDRESS = 0x8D50B4;
        private const int NOISE_NV_LEVEL_ADDRESS = 0x8D50A8;
        private const int NOISE_RED_ADDRESS = 0x7038ED;
        private const int NOISE_GREEN_ADDRESS = 0x7038E8;
        private const int NOISE_BLUE_ADDRESS = 0x7038E3;
        private const int DRAWDISTANCE_BASE_ADDRESS = 0xB7B1D0;
        private const int LIGHT_COLOR_RED_ADDRESS = 0x75742E;
        private const int LIGHT_COLOR_GREEN_ADDRESS = 0x757438;
        private const int LIGHT_COLOR_BLUE_ADDRESS = 0x757442;
        private const int LIGHT_COLOR_ALPHA_ADDRESS = 0x75744C;
        private const int WATER_COLOR_RED_ADDRESS = 0xB7AAA0;
        private const int WATER_COLOR_GREEN_ADDRESS = 0xB7A9E8;
        private const int WATER_COLOR_BLUE_ADDRESS = 0xB7A930;
        private const int WATER_COLOR_ALPHA_ADDRESS = 0xB7A878;
        private const int RADAR_DISABLED_ADDRESS = 0xBA676C;
        private const int RACE_CP_INVISIBLE_ADDRESS = 0xC7F15A;
        private const int LOW_CLOUD_RED_ADDRESS = 0xB7AEF0;
        private const int LOW_CLOUD_GREEN_ADDRESS = 0xB7AE38;
        private const int LOW_COULD_BLUE_ADDRESS = 0xB7AD80;
        private const int LOW_CLOUD_ALPHA_ADDRESS = 0xB7A200;
        private const int MID_CLOUD_RED_ADDRESS = 0xB7ACC8;
        private const int MID_CLOUD_GREEN_ADDRESS = 0xB7AC10;
        private const int MID_CLOUD_BLUE_ADDRESS = 0xB7AB58;
        private const int MID_CLOUD_ALPHA_ADDRESS = 0xB7A148;

        public static void SetMidCloudColor(uint color, ref MemoryEdit mEdit)
        {
            byte[] a = System.BitConverter.GetBytes(color);
            for (int time = 0; time != 8; time++)
            {
                for (int weather = 0; weather != 23; weather++)
                {
                    int o = weather + time * 23;
                    mEdit.writeToMemory(MID_CLOUD_RED_ADDRESS + o, a[0]);
                    mEdit.writeToMemory(MID_CLOUD_GREEN_ADDRESS + o, a[1]);
                    mEdit.writeToMemory(MID_CLOUD_BLUE_ADDRESS + o, a[2]);
                    mEdit.writeToMemory(MID_CLOUD_ALPHA_ADDRESS + o, a[3]);
                }
            }
        }

        public static void SetLowCloudColor(uint color, ref MemoryEdit mEdit)
        {
            byte[] a = System.BitConverter.GetBytes(color);
            for (int time = 0; time != 8; time++)
            {
                for (int weather = 0; weather != 23; weather++)
                {
                    int o = weather + time * 23;
                    mEdit.writeToMemory(LOW_CLOUD_RED_ADDRESS + o, a[0]);
                    mEdit.writeToMemory(LOW_CLOUD_GREEN_ADDRESS + o, a[1]);
                    mEdit.writeToMemory(LOW_COULD_BLUE_ADDRESS + o, a[2]);
                    mEdit.writeToMemory(LOW_CLOUD_ALPHA_ADDRESS + o, a[3]);
                }
            }
        }

        public static void TogglePlayerRaceCPInvisible(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(RACE_CP_INVISIBLE_ADDRESS, System.Convert.ToByte(state));
        }

        public static void SetRadarDisabled(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(RADAR_DISABLED_ADDRESS, System.Convert.ToByte(state));
        }

        public static void SetWaterColor(uint color, ref MemoryEdit mEdit)
        {
            byte[] a = System.BitConverter.GetBytes(color);
            for (int time = 0; time != 8; time++)
            {
                for (int weather = 0; weather != 23; weather++)
                {
                    int o = weather + time * 23;
                    mEdit.writeToMemory(WATER_COLOR_RED_ADDRESS+o, a[0]);
                    mEdit.writeToMemory(WATER_COLOR_GREEN_ADDRESS+o, a[1]);
                    mEdit.writeToMemory(WATER_COLOR_BLUE_ADDRESS+o, a[2]);
                    mEdit.writeToMemory(WATER_COLOR_ALPHA_ADDRESS+o, a[3]);
                }
            }
        }

        public static void SetLightColor(uint color, ref MemoryEdit mEdit)
        {
            byte[] a = System.BitConverter.GetBytes(color);
            float red = (float)a[0] / 255F;
            float green = (float)a[1] / 255F;
            float blue = (float)a[2] / 255F;
            float alpha = (float)a[3] / 255F;
            mEdit.writeToMemory(LIGHT_COLOR_RED_ADDRESS, red);
            mEdit.writeToMemory(LIGHT_COLOR_GREEN_ADDRESS, green);
            mEdit.writeToMemory(LIGHT_COLOR_BLUE_ADDRESS, blue);
            mEdit.writeToMemory(LIGHT_COLOR_ALPHA_ADDRESS, alpha);
        }

        public static void SetDrawDistance(int weather, int time, ushort drawDistance, ref MemoryEdit mEdit, bool correctTime=false)
        {
            //Time == -1 = all times:
            if (time == -1)
            {
                for (int i = 0; i != 8; i++)
                {
                    if (weather == -1)
                    {
                        for (int j = 0; j != 23; j++)
                        {
                            SetDrawDistance(j, i, drawDistance, ref mEdit, true);
                        }
                    }
                    else
                        SetDrawDistance(weather, i, drawDistance, ref mEdit, true);
                }
                return;
            }
            int _time = time;
            if (!correctTime)
            {
                if (time >= 0 && time < 5)
                    _time = 0;
                else if (time == 5)
                    _time = 1;
                else if (time == 6)
                    _time = 2;
                else if (time >= 7 && time < 12)
                    _time = 3;
                else if (time >= 12 && time < 19)
                    _time = 4;
                else if (time == 19)
                    _time = 5;
                else if (time >= 20 && time < 21)
                    _time = 6;
                else
                    _time = 7;
            }
            int addr = DRAWDISTANCE_BASE_ADDRESS;
            addr += weather * 2;
            addr += _time * 23 * 2;
            mEdit.writeToMemory(addr, drawDistance);
        }
        public static void HUD_SetMoneyColor(uint color,ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(HUD_MONEYCOLOR_ADDRESS, color);
        }
        public static void HUD_SetWantedColor(uint color, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(HUD_WANTEDCOLOR_ADDRESS, color);
        }
        public static void HUD_SetHealthColor(uint color, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(HUD_HEALTHCOLOR_ADDRESS, color);
        }
        public static void HUD_SetRadioColor(uint color, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(HUD_RADIOCOLOR_ADDRESS, color);
        }
        public static void SetPlayerVehicleCameraMode(int mode, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(VEHICLECAMERAMODE_ADDRESS, mode);
        }
        public static void SetPlayerAircraftHeight(float height, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(AIRCRAFT_HEIGHT_ADDRESS, height);
        }
        public static void ToggleInfiniteRun(bool toggle, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(INFINITE_RUN_ADDRESS, System.Convert.ToByte(toggle));
        }
        public static void SetPickupSize(float size, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PICKUP_SIZE_ADDRESS, size);
        }
        public static void TogglePlayerFireproofState(bool toggle,ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(FIREPROOF_ADDRESS, System.Convert.ToByte(toggle));
        }
        public static void SetWaveHeight(float height, ref MemoryEdit mEdit)
        {
            //NOP:
            mEdit.NOPMemory(0x72C665, 6);
            mEdit.NOPMemory(0x72C659, 4);
            mEdit.writeToMemory(WAVE_HEIGHT_ADDRESS,height);
        }
        public static void SetWindDensity(float density, ref MemoryEdit mEdit)
        {
            //NOP:
            mEdit.NOPMemory(0x72C3C2, 6);
            mEdit.writeToMemory(WIND_DENSITY_ADDRESS, density);
        }
        public static void SetHUDVisible(bool visible, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(TOGGLE_HUD_ADDRESS, System.Convert.ToByte(visible));
        }
        public static void SetNVState(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(NV_ADDRESS, System.Convert.ToByte(state));
        }
        public static void SetTHVisible(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(TH_ADDRESS, System.Convert.ToByte(state));
        }
        public static void SetPlayerGravity(float gravity, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(GRAV_ADDRESS, gravity);
        }
        public static void SetGreyRadarState(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(GRAY_RADAR_ADDRESS, System.Convert.ToByte(state));
        }
        public static void SetTimeFlies(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(TIME_FLIES_ADDRESS, System.Convert.ToByte(state));
        }
        public static void SetPlayerWheelVisibility(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(WHEELS_ONLY_ADDRESS, System.Convert.ToByte(state));    
        }
        public static void SetPlayerOxygen(float oxygen, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_OXYGEN_ADDRESS, oxygen);
        }
        public static void SetPlayerInfOxygenState(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_INF_OXYGEN_ADDRESS, System.Convert.ToByte(state));
        }
        public static void SetPlayerTrafficLightState(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(GREEN_TRAFFIC_LIGHTS_ADDRESS, System.Convert.ToByte(state));
        }
        public static void SetPlayerEnterExitVisibility(ref MemoryEdit mEdit)
        {
            mEdit.NOPMemory(PLAYER_INVISIBLE_ENTEREXITS_ADDRESS, 5);
        }
        public static void SetPlayerRadioVolume(float volume, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_RADIO_VOLUME_ADDRESS, volume);
        }
        public static void SetPlayerSFXVolume(float volume, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_SFX_VOLUME_ADDRESS, volume);
        }
        public static void SetPlayerStrange2DEffect(ref MemoryEdit mEdit)
        {
            mEdit.NOPMemory(STRANGE_2D_COLOR_ADDRESS, 4);
        }
        public static void SetPlayerHealthBarBorderState(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(HEALTHBAR_BORDER_ADDRESS, System.Convert.ToByte(state));
        }
        public static void SetPlayerFogDensity(float density, ref MemoryEdit mEdit,ref localInformation localInf)
        {
            if (density != 0)
                localInf.fog_density = density;
            else
                localInf.fog_density = -1;
            mEdit.writeToMemory(PLAYER_FOG_DENSITY_ADDRESS, density);
        }
        public static void SetPlayerRainDensity(float density, ref MemoryEdit mEdit,ref localInformation localInf)
        {
            if (density != 0)
                localInf.rain_density = density;
            else
                localInf.rain_density = -1;
            mEdit.writeToMemory(PLAYER_RAIN_DENSITY_ADDRESS, density);
        }
        public static void SetPlayerWeirdDeformFactor(float factor, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_WEIRD_DEFORM_ADDRESS, factor);
        }
        public static void SetPlayerNearCameraDistance(float distance, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_CAMERA_NEAR_DISTANCE_ADDRESS, distance);
        }
        public static void SetPlayerDefaultCameraDistance(float distance, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_CAMERA_DEFAULT_DISTANCE_ADDRESS, distance);
        }
        public static void SetPlayerFarCameraDistance(float distance, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_CAMERA_FAR_DISTANCE_ADDRESS, distance);
        }
        public static void DisablePlayerAiming(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(DISABLE_AIMING_ADDRESS, System.Convert.ToByte(state));
        }
        public static void DisablePlayerCameraChangeOnFoot(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(DISABLE_ONFOOT_CAMERA_CHANGE_ADDRESS, System.Convert.ToByte(state));
        }
        public static void SetPlayerWaveHeightEx(float height, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_WAVE_HEIGHT_EX_ADDRESS, height);
        }
        public static void TogglePlayerCameraUnderwaterEffect(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(UNDERWATER_CAMERA_EFFECT_ADDRESS, System.Convert.ToByte(state));
        }
        public static void TogglePlayerHorizontalLinesEffect(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(HORIZONTAL_SCREEN_LINES_ADDRESS, System.Convert.ToByte(state));
        }
        public static void TakePlayerPhoto(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_TAKE_PHOTO_ADDRESS, System.Convert.ToByte(state));
        }
        public static void SetPlayerRainDensityEx(float density, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_RAINING_SCREEN_EFFECT_ADDRESS, density);
        }
        public static void SetPlayerBrightHorizonEffect(bool state, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_BRIGHT_HORIZON_EFFECT_ADDRESS, System.Convert.ToByte(state));
            mEdit.NOPMemory(0x72BB02, 7);
        }
        public static void SetPlayerFlameColor1(int color, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_FLAME_COLOR_1_ADDRESS, color);
        }
        public static void SetPlayerFlameColor2(int color, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(PLAYER_FLAME_COLOR_2_ADDRESS, color);
        }
        public static void SetNoiseInfo(NoiseEffectInfo nInfo, ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(NOISE_TH_LEVEL_ADDRESS, nInfo.thermal_noise_level);
            mEdit.writeToMemory(NOISE_NV_LEVEL_ADDRESS, nInfo.nv_noise_level);
            mEdit.writeToMemory(NOISE_RED_ADDRESS, nInfo.red_noise);
            mEdit.writeToMemory(NOISE_GREEN_ADDRESS, nInfo.green_noise);
            mEdit.writeToMemory(NOISE_BLUE_ADDRESS, nInfo.blue_noise);
        }
    }
}
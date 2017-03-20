using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
//using System.Threading.Tasks;

namespace SAMPExtender_Client
{
    class InternalImplementations
    {
        private const int HUD_MONEYCOLOR_ADDRESS = 0xBAB230;
        private const int HUD_WANTEDCOLOR_ADDRESS = 0xBAB244;
        private const int HUD_HEALTHCOLOR_ADDRESS = 0xBAB22C;
        private const int HUD_RADIOCOLOR_ADDRESS = 0xBAB24C;
        private const int VEHICLECAMERAMODE_ADDRESS = 0xB6F0DC;
        private const int AIRCRAFT_HEIGHT_ADDRESS = 0x8594DC;
        private const int INFINITE_RUN_ADDRESS = 0xB7CEE4;
        private const int HYDRA_ROCKET_DELAY_ADDRESS_1 = 0x6D462E;
        private const int HYDRA_ROCKET_DELAY_ADDRESS_2 = 0x6D4634;
        private const int RESOLUTION_ADDRESS = 0xBA6820;
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
        private const int PLAYER_RADIOSTATION_ADDRESS = 0x8CB7A5;
        private const int PLAYER_INVISIBLE_ENTEREXITS_ADDRESS = 0x53E213;
        private const int PLAYER_RADIO_VOLUME_ADDRESS = 0xB5FCC8;
        private const int PLAYER_SFX_VOLUME_ADDRESS = 0xB5FCCC;
        private const int STRANGE_2D_COLOR_ADDRESS = 0x7170C0;
        private const int PLAYER_RESOLUTION_WIDTH_ADDRESS = 0xC17044;
        private const int PLAYER_RESOLUTION_HEIGHT_ADDRESS = 0xC17048;
        private const int HEALTHBAR_BORDER_ADDRESS = 0x589353;
        private const int PLAYER_REGION_ID_ADDRESS = 0xC81314;
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
        private const int MENU_ID_ADDRESS = 0xBA68A5;
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

        public static RakNet.RakString ListFiles(string folderName, string basePath, bool recursive, ref MemoryEdit mEdit)
        {
            /*if (folderName.Contains(".."))
                return "Invalid folder call!";

            string path = basePath.Remove(basePath.Length - 7, 7);
            path += folderName;
            IEnumerable<string> fileList = Directory.EnumerateFiles(path, "*", recursive ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly);

            string returnStr = "";
            System.Security.Cryptography.MD5CryptoServiceProvider crypto = new System.Security.Cryptography.MD5CryptoServiceProvider();
            foreach (string s in fileList)
            {
                //Setup:
                string file=s.Remove(0, path.Length).Replace("\\", "/");
                string fileInfo = "";

                //File Name:
                //File is in a subfolder:
                if (file.Contains("/"))
                {
                    string[] t = file.Split('/');
                    fileInfo += t[t.Length - 1];
                    //Subfolder:
                    fileInfo += "|";
                    for (int i = 0; i != t.Length - 1;i++)
                    {
                        fileInfo += t[i];
                        if (i < t.Length - 2)
                            fileInfo += "/";
                    }
                }
                else //File is not in a subfolder:
                    fileInfo += file+"||";

                //FileSize:
                fileInfo += "|";
                long fileSize = new FileInfo(s).Length;
                fileInfo += fileSize.ToString();

                //FileHash:
                fileInfo += "|";

                //if (fileSize < 104857600) //100MB
               // {
               //     Stream f = new System.IO.FileStream(s, FileMode.Open, FileAccess.Read);
               //     byte[] hash = crypto.ComputeHash(f);
               //     f.Close();
               //     foreach (byte b in hash)
               //     {
               //         fileInfo += b.ToString();
                //    }
                //}
                //else
                    fileInfo += "-1";
                
                //End:
                fileInfo += "\n";
                returnStr += fileInfo;
            }
            RakNet.RakString rakStr = new RakNet.RakString(returnStr);*/
            return "";
        }

        public static RakNet.RakString ListFolders(string folderName, string basePath, bool recursive, ref MemoryEdit mEdit)
        {
            /*if (folderName.Contains(".."))
                return "Invalid folder call!";

            string path = basePath.Remove(basePath.Length - 7, 7);
            path+=folderName;
            IEnumerable<string> dirList=Directory.EnumerateDirectories(path, "*", recursive?SearchOption.AllDirectories:SearchOption.TopDirectoryOnly);

            string returnStr = "";
            foreach (string s in dirList)
                returnStr += s.Remove(0,path.Length).Replace('\\','/') + '|';

            RakNet.RakString rakStr= new RakNet.RakString(returnStr);
            return rakStr;*/
            return "";
        }

        public static bool GetPlayerRaceCPInvisibleState(ref MemoryEdit mEdit)
        {
            return System.Convert.ToBoolean(mEdit.readByteFromMemory(RACE_CP_INVISIBLE_ADDRESS));
        }

        public static bool GetRadarDisabled(ref MemoryEdit mEdit)
        {
            return System.Convert.ToBoolean(mEdit.readByteFromMemory(RADAR_DISABLED_ADDRESS));
        }

        public static uint GetWaterColor(ref MemoryEdit mEdit)
        {
            byte[] a = new byte[4];
            a[0] = mEdit.readByteFromMemory(WATER_COLOR_RED_ADDRESS);
            a[1] = mEdit.readByteFromMemory(WATER_COLOR_GREEN_ADDRESS);
            a[2] = mEdit.readByteFromMemory(WATER_COLOR_BLUE_ADDRESS);
            a[3] = mEdit.readByteFromMemory(WATER_COLOR_ALPHA_ADDRESS);
            return System.BitConverter.ToUInt32(a, 0);
        }

        public static uint GetLightColor(ref MemoryEdit mEdit)
        {
            byte[] a = new byte[4];
            byte red = (byte)(255*mEdit.readFloatFromMemory(LIGHT_COLOR_RED_ADDRESS));
            byte green = (byte)(255*mEdit.readFloatFromMemory(LIGHT_COLOR_GREEN_ADDRESS));
            byte blue = (byte)(255*mEdit.readFloatFromMemory(LIGHT_COLOR_BLUE_ADDRESS));
            byte alpha = (byte)(255*mEdit.readFloatFromMemory(LIGHT_COLOR_ALPHA_ADDRESS));
            a[0] = red;
            a[1] = green;
            a[2] = blue;
            a[3] = alpha;
            return System.BitConverter.ToUInt32(a, 0);
        }

        public static byte GetMenuID(ref MemoryEdit mEdit)
        {
            byte menuID = mEdit.readByteFromMemory(MENU_ID_ADDRESS);
            return menuID;
        }

        public static uint GetMoneyColor(ref MemoryEdit mEdit)
        {
            uint color = mEdit.readUIntFromMemory(HUD_MONEYCOLOR_ADDRESS);
            return color;
        }
        public static uint GetWantedColor(ref MemoryEdit mEdit)
        {
            uint color = mEdit.readUIntFromMemory(HUD_WANTEDCOLOR_ADDRESS);
            return color;
        }
        public static uint GetHealthColor(ref MemoryEdit mEdit)
        {
            uint color = mEdit.readUIntFromMemory(HUD_HEALTHCOLOR_ADDRESS);
            return color;
        }
        public static uint GetRadioColor(ref MemoryEdit mEdit)
        {
            uint color = mEdit.readUIntFromMemory(HUD_RADIOCOLOR_ADDRESS);
            return color;
        }
        public static int GetVehicleCameraMode(ref MemoryEdit mEdit)
        {
            int mode = mEdit.readByteFromMemory(VEHICLECAMERAMODE_ADDRESS);
            return mode;
        }
        public static float GetAircraftHeight(ref MemoryEdit mEdit)
        {
            float height = mEdit.readFloatFromMemory(AIRCRAFT_HEIGHT_ADDRESS);
            return height;
        }
        public static bool GetInfiniteRunState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(INFINITE_RUN_ADDRESS));
            return state;
        }
        public static void SetHydraRocketDelay(uint delay,ref MemoryEdit mEdit)
        {
            mEdit.writeToMemory(HYDRA_ROCKET_DELAY_ADDRESS_1, delay);
            mEdit.writeToMemory(HYDRA_ROCKET_DELAY_ADDRESS_2, delay);
        }
        public static int GetResolution(ref MemoryEdit mEdit)
        {
            int res = mEdit.readByteFromMemory(RESOLUTION_ADDRESS);
            return res;
        }
        public static float GetPickupSize(ref MemoryEdit mEdit)
        {
            float size = mEdit.readFloatFromMemory(PICKUP_SIZE_ADDRESS);
            return size;
        }
        public static bool GetPlayerFireproofState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(FIREPROOF_ADDRESS));
            return state;
        }
        public static float GetPlayerWaveHeight(ref MemoryEdit mEdit)
        {
            float height = mEdit.readFloatFromMemory(WAVE_HEIGHT_ADDRESS);
            return height;
        }
        public static float GetPlayerWindDensity(ref MemoryEdit mEdit)
        {
            float wd = mEdit.readFloatFromMemory(WIND_DENSITY_ADDRESS);
            return wd;
        }
        public static bool GetPlayerHUDState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(TOGGLE_HUD_ADDRESS));
            return state;
        }
        public static bool GetPlayerNVState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(NV_ADDRESS));
            return state;
        }
        public static bool GetPlayerTHState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(TH_ADDRESS));
            return state;
        }
        public static float GetPlayerGravity(ref MemoryEdit mEdit)
        {
            float grav = mEdit.readFloatFromMemory(GRAV_ADDRESS);
            return grav;
        }
        public static bool GetPlayerRadarState(ref MemoryEdit mEdit)
        {
            bool rstate = Convert.ToBoolean(mEdit.readByteFromMemory(GRAY_RADAR_ADDRESS));
            return rstate;
        }
        public static bool GetTimeFliesBy(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(TIME_FLIES_ADDRESS));
            return state;
        }
        public static bool GetPlayerWheelsVisibility(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(WHEELS_ONLY_ADDRESS));
            return state;
        }
        public static float GetPlayerOxygen(ref MemoryEdit mEdit)
        {
            float oxygen = mEdit.readFloatFromMemory(PLAYER_OXYGEN_ADDRESS);
            return oxygen;
        }
        public static bool GetPlayerInfOxygenState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(PLAYER_INF_OXYGEN_ADDRESS));
            return state;
        }
        public static bool GetPlayerTrafficLightState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(GREEN_TRAFFIC_LIGHTS_ADDRESS));
            return state;
        }
        public static int GetPlayerRadioStation(ref MemoryEdit mEdit)
        {
            int state = mEdit.readByteFromMemory(PLAYER_RADIOSTATION_ADDRESS);
            return state;
        }
        public static bool GetPlayerEnterExitVisibility(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(PLAYER_INVISIBLE_ENTEREXITS_ADDRESS));
            return state;
        }
        public static float GetPlayerRadioVolume(ref MemoryEdit mEdit)
        {
            float volume = mEdit.readFloatFromMemory(PLAYER_RADIO_VOLUME_ADDRESS);
            return volume;
        }
        public static float GetPlayerSFXVolume(ref MemoryEdit mEdit)
        {
            float volume = mEdit.readFloatFromMemory(PLAYER_SFX_VOLUME_ADDRESS);
            return volume;
        }
        public static bool GetPlayerStrange2DEffect(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(STRANGE_2D_COLOR_ADDRESS));
            return state;
        }
        public static int GetPlayerResolutionWidth(ref MemoryEdit mEdit)
        {
            int width = mEdit.readUShortFromMemory(PLAYER_RESOLUTION_WIDTH_ADDRESS);
            return width;
        }
        public static int GetPlayerResolutionHeight(ref MemoryEdit mEdit)
        {
            int height = mEdit.readUShortFromMemory(PLAYER_RESOLUTION_HEIGHT_ADDRESS);
            return height;
        }
        public static bool GetPlayerHealthBarBorderState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(HEALTHBAR_BORDER_ADDRESS));
            return state;
        }
        public static int GetPlayerCurrentRegion(ref MemoryEdit mEdit)
        {
            int region = mEdit.readByteFromMemory(PLAYER_REGION_ID_ADDRESS);
            return region;
        }
        public static float GetPlayerFogDensity(ref MemoryEdit mEdit)
        {
            float density = mEdit.readFloatFromMemory(PLAYER_FOG_DENSITY_ADDRESS);
            return density;
        }
        public static float GetPlayerRainDensity(ref MemoryEdit mEdit)
        {
            float density = mEdit.readFloatFromMemory(PLAYER_RAIN_DENSITY_ADDRESS);
            return density;
        }
        public static float GetPlayerWeirdDeformFactor(ref MemoryEdit mEdit)
        {
            float factor = mEdit.readFloatFromMemory(PLAYER_WEIRD_DEFORM_ADDRESS);
            return factor;
        }
        public static float GetPlayerNearCameraDistance(ref MemoryEdit mEdit)
        {
            float distance = mEdit.readFloatFromMemory(PLAYER_CAMERA_NEAR_DISTANCE_ADDRESS);
            return distance;
        }
        public static float GetPlayerDefaultCameraDistance(ref MemoryEdit mEdit)
        {
            float distance = mEdit.readFloatFromMemory(PLAYER_CAMERA_DEFAULT_DISTANCE_ADDRESS);
            return distance;
        }
        public static float GetPlayerFarCameraDistance(ref MemoryEdit mEdit)
        {
            float distance = mEdit.readFloatFromMemory(PLAYER_CAMERA_FAR_DISTANCE_ADDRESS);
            return distance;
        }
        public static bool GetDisablePlayerAimingState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(DISABLE_AIMING_ADDRESS));
            return state;
        }
        public static bool GetDisableCameraChangeOnFootState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(DISABLE_AIMING_ADDRESS));
            return state;
        }
        public static float GetPlayerWaveHeightEx(ref MemoryEdit mEdit)
        {
            float height = mEdit.readFloatFromMemory(PLAYER_WAVE_HEIGHT_EX_ADDRESS);
            return height;
        }
        public static bool GetPlayerCameraUnderWaterEffectState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(UNDERWATER_CAMERA_EFFECT_ADDRESS));
            return state;
        }
        public static bool GetPlayerHorizontalLinesEffectState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(HORIZONTAL_SCREEN_LINES_ADDRESS));
            return state;
        }
        public static float GetPlayerRainingValue(ref MemoryEdit mEdit)
        {
            float value = mEdit.readFloatFromMemory(PLAYER_RAINING_SCREEN_EFFECT_ADDRESS);
            return value;
        }
        public static bool GetPlayerBrightHorizonState(ref MemoryEdit mEdit)
        {
            bool state = Convert.ToBoolean(mEdit.readByteFromMemory(PLAYER_BRIGHT_HORIZON_EFFECT_ADDRESS));
            return state;
        }
        public static int GetPlayerFlameColor1(ref MemoryEdit mEdit)
        {
            int color = mEdit.readIntFromMemory(PLAYER_FLAME_COLOR_1_ADDRESS);
            return color;
        }
        public static int GetPlayerFlameColor2(ref MemoryEdit mEdit)
        {
            int color = mEdit.readIntFromMemory(PLAYER_FLAME_COLOR_2_ADDRESS);
            return color;
        }
        public static float GetPlayerFPS(ref MemoryEdit mEdit)
        {
            float fps = mEdit.readByteFromMemory(PLAYER_FPS_ADDRESS);
            return fps;
        }
    }
}

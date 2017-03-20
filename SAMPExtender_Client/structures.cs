using System.Runtime.InteropServices;
namespace SAMPExtender_Client
{
    public enum RakNetStates
    {
        ID_CLIENTINFO = RakNet.DefaultMessageIDTypes.ID_USER_PACKET_ENUM + 1,
        ID_SERVERINFO,
        ID_NOISEINFO,
        ID_KICK,
        ID_HUD_SETMONEYCOLOR,
        ID_HUD_SETWANTEDCOLOR,
        ID_HUD_SETHEALTHCOLOR,
        ID_HUD_SETRADIOCOLOR,
        ID_SETPLAYERVEHICLECAMERAMODE,
        ID_SETAIRCRAFTHEIGHT,
        ID_TOGGLEINFINITERUN,
        ID_SETPICKUPSIZE,
        ID_TOGGLEFIREPROOF,
        ID_SETWAVEHEIGHT,
        ID_SETWINDDENSITY,
        ID_SETHUDVISIBLE,
        ID_SETNV,
        ID_SETTH,
        ID_SETGRAVITY,
        ID_SETGRAYRADER,
        ID_SETTIMEFLIES,
        ID_SETWHEELSONLY,
        ID_SETOXYGEN,
        ID_SETINFINITYOXYGEN,
        ID_SETALLGREENLIGHTS,
        ID_SETPLAYERRADIOSTATION,
        ID_SETTELEMARKERSINVISIBLE,
        ID_SETDRUGEFFECT,
        ID_SETRADIOVOL,
        ID_SETSFXVOL,
        ID_DRAWHEALTHBARBORDER,
        ID_SETFOGDENSITY,           
        ID_SETRAINDENSITY,
        ID_SETPLAYERDEFORM,
        ID_SETNEARCAM,
        ID_SETNORMCAM,
        ID_SETFARCAM,
        ID_SETDISABLEDAIMING,
        ID_SETDISABLEDCAMCHANGE,
        ID_SETCALMWAVEHEIGHT,
        ID_TOGGLEUNDERWARTEREFFECT,
        ID_TOGGLEHORISCREENLINES,
        ID_FIREPLAYERCAMERA,
        ID_RAINEFFECT,
        ID_HORIZONEFFECT,
        ID_SETGREENFLAMES1, 
        ID_SETGREENFLAMES2,
        ID_SETDRAWDISTANCE,
        ID_SETLIGHTCOLOR,
        ID_SETWATERCOLOR,
        ID_SETRADARDISABLE,
        ID_SETRACECPINVISIBLE,
        ID_SETCLOULDCOLORLOW,
        ID_SETCLOUDCOLORMID,
        ID_GETFOLDERS,
        ID_GETFILES,
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ClientInfo
    {
        public int type;

        public uint money_color;
        public uint wanted_color;
        public uint health_color;
        public uint radio_color;
        public int camera_mode;
        public float aircraft_height;
        public bool infinite_run;
        public int resolution_w;
        public int resolution_h;
        public float pickup_size;
        public bool fireproof;
        public float wave_height;
        public float wind_density;
        public bool hud_visible;
        public bool nv_active;
        public bool th_active;
        public float gravity;
        public bool grey_radar;
        public bool time_flies;
        public bool wheels_only;
        public float oxygen;
        public bool infinite_oxygen;
        public bool green_lights;
        public int radio_station;
        public bool tele_marker_state;
        public bool drug_effect;
        public float radio_volume;
        public float sfx_volume;
        public bool healthbar_border;
        public int current_region;
        public float fog_density;
        public float rain_density;
        public float player_deform;
        public float near_cam_dist;
        public float far_cam_dist;
        public float norm_cam_dist;
        public bool disabled_aiming;
        public bool disabled_camchange;
        public float calm_wave_height;
        public bool underwater_effect;
        public bool horizontal_screen_lines;
        public float rain_value;
        public bool horizon_effect;
        public int green_flames_1;
        public int green_flames_2;
        public float fps;
        public int version;
        public byte menuID;
        public uint light_color;
        public uint water_color;
        public bool radar_disabled;
        public bool race_cp_invisible;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct ServerInfo
    {
        public int type;

        public uint hydra_rocket_delay;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct NoiseEffectInfo
    {
        public int type;
        public uint thermal_noise_level;
        public uint nv_noise_level;
        public uint red_noise;
        public uint green_noise;
        public uint blue_noise;
    }

    struct localInformation
    {
        public float fog_density;
        public float rain_density;
        public localInformation(float init)
        {
            fog_density = init;
            rain_density = init;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    struct drawDistanceParameters
    {
        public int type;
        public int weather;
        public int time;
        public ushort drawDistance;
    }

    [StructLayout(LayoutKind.Sequential)]
    struct fileInformationRequest
    {
        public int type;
        public char[] folder;
        public bool recursive;
    }

    /*[StructLayout(LayoutKind.Sequential)]
    struct fileInformationResponse
    {
        public int type;
        public fixed char response[5000];
    }*/

    class SBConverter
    {
        static public byte[] getBytesFromStruct(object o)
        {
            int size = Marshal.SizeOf(o);
            byte[] a = new byte[size];
            System.IntPtr p = Marshal.AllocHGlobal(size);
            Marshal.StructureToPtr(o, p, false);
            Marshal.Copy(p, a, 0, size);
            Marshal.FreeHGlobal(p);
            return a;
        }
    }
}


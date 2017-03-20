using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;
using RakNet;

namespace SAMPExtender_Client
{
    public partial class MainForm : Form
    {
        MemoryEdit mEdit = null;
        networkingClient net = null;
        bool noGUI = true;
        bool quitSoftware = false;
        int processCheckTickCount;
        bool connectedOnce = false;
        static int version = 5;                             //Internal SAMPFox version designation
        localInformation localInf;
        static int currentVersionFileContent = 3;           //3 for 0.3.7
        private static int SAMP_IP_OFFSET_03e = 0x20D77D;
        private static int SAMP_PORT_OFFSET_03e = 0x20D87E;
        private static int SAMP_IP_OFFSET_03x = 0x2121AD;
        private static int SAMP_PORT_OFFSET_03x = 0x2122AE;
        private static int SAMP_IP_OFFSET_03z = 0x2121F5;
        private static int SAMP_PORT_OFFSET_03z = 0x2122F6;
        private static int SAMP_IP_OFFSET_037 = 0x21986D;
        private static int SAMP_PORT_OFFSET_037 = 0x21996E;
        public delegate void delegateSendClientInfoToServer(ClientInfo info);
        public delegate void delegateSendMessageToServer(RakNetStates messageType, RakString content);

        public MainForm(string param)
        {
            InitializeComponent();
            if (param == "gui")
            {
                noGUI = false;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            mEdit = new MemoryEdit();
            localInf = new localInformation(-1);
            CheckForProcessTimer.Start();
            if (noGUI)
            {
                connectionLabel.Hide();
                manconnectlabel.Hide();
                statusLabel.Text = "SAMPFox is running in NO GUI mode.\nLaunch SAMPFox via the directory shortcut to enable\nthe GUI.";
                statusLabel.TextAlign = ContentAlignment.TopCenter;
                this.Size = new Size(300, 180);
                this.WindowState = System.Windows.Forms.FormWindowState.Minimized;
            }
            else
            {
                string path = Application.StartupPath + "\\forcedport.data";
                StreamReader s;
                if (File.Exists(path))
                {
                    s = new StreamReader(path);
                    string forced_port = s.ReadToEnd();
                    s.Close();
                    forcedPortTB.Text = forced_port;
                }
                //Version:
                path = Application.StartupPath + "\\version.data";
                if (!File.Exists(path))
                {
                    MessageBox.Show("Error: Version file not found. Creating a new one.", "Error");
                    File.WriteAllText(Application.StartupPath + "\\version.data", currentVersionFileContent.ToString());
                }
                s = new StreamReader(path);
                string str = s.ReadToEnd();
                s.Close();
                int index = 0;
                int.TryParse(str, out index);
                versionCB.SelectedIndex = index;
            }
            this.MinimumSize = this.Size;
            this.MaximumSize = this.Size;
        }

        private void clientTimer_Tick(object sender, EventArgs e)
        {
            mEdit.checkProcess();
            if (!mEdit.processActive)   //Process not active
            {
                if (connectedOnce)
                {
                    if (noGUI)
                    {
                        quitSoftware = true;
                        this.Close();
                    }
                }

                if (networkThread.IsBusy)   //No process anymore, but networking stuff still running:
                {
                    networkThread.CancelAsync();
                    if (noGUI)
                    {
                        quitSoftware = true;
                        this.Close();
                    }
                }
                if (noGUI)
                {
                    processCheckTickCount++;
                    if (processCheckTickCount > 200)
                    {
                        quitSoftware = true;
                        this.Close();
                    }
                }

                bool s = mEdit.getProcess("gta_sa");
                if (s)
                {
                    connectionLabel.Text = "Process found, ready to connect!";
                    if (noGUI)
                    {
                        networkThread.RunWorkerAsync();
                    }
                }
            }
            else    //Process active
            {
                //Set values that reset themselves: 
                if (localInf.fog_density != -1)
                    NativeImplementations.SetPlayerFogDensity(localInf.fog_density, ref mEdit, ref localInf);
                if (localInf.rain_density != -1)
                    NativeImplementations.SetPlayerRainDensity(localInf.rain_density, ref mEdit, ref localInf);
            }
        }

        private void connectBtn_Click(object sender, EventArgs e)
        {
            if (!networkThread.IsBusy)
                networkThread.RunWorkerAsync();
            else
                networkThread.CancelAsync();
        }

        private void networkThread_DoWork(object sender, DoWorkEventArgs e)
        {
            //Check if the San Andreas process is active. If yes, continue. If not, abort.
            if (!mEdit.processActive)
            {
                MessageBox.Show("Error: No SA process found.", "Error");
                if (noGUI)
                {
                    quitSoftware = true;
                    this.Close();
                }
                return;
            }

            //Start the connection process:
            ushort port = 0;
            string addStr = "";

            int samp_ip_addr = 0;
            int samp_port_addr = 0;

            //Check for a possible forced port:
            string path = Application.StartupPath + "\\forcedport.data";
            ushort forced_port = 0;
            StreamReader s;
            if (File.Exists(path))
            {
                s = new StreamReader(path);
                string p = s.ReadToEnd();
                s.Close();
                ushort.TryParse(p, out port);
                if (port == 0)
                {
                    MessageBox.Show("Error: Invalid forced port.", "Error");
                    if (noGUI)
                    {
                        quitSoftware = true;
                        this.Close();
                    }
                    return;
                }
            }

            //Check for version:
            path = Application.StartupPath + "\\version.data";
            if (!File.Exists(path))
            {
                MessageBox.Show("Error: Version file not found.", "Error");
                if (noGUI)
                {
                    quitSoftware = true;
                    this.Close();
                }
                return;
            }
            s = new StreamReader(path);
            string str = s.ReadToEnd();
            s.Close();
            int index = 0;
            int.TryParse(str, out index);
            str = versionCB.Items[index].ToString();
            if (str == "0.3e")
            {
                samp_ip_addr = SAMP_IP_OFFSET_03e;
                samp_port_addr = SAMP_PORT_OFFSET_03e;
            }
            else if (str == "0.3x")
            {
                samp_ip_addr = SAMP_IP_OFFSET_03x;
                samp_port_addr = SAMP_PORT_OFFSET_03x;
            }
            else if (str == "0.3z R1")
            {
                samp_ip_addr = SAMP_IP_OFFSET_03z;
                samp_port_addr = SAMP_PORT_OFFSET_03z;
            }
            else if (str == "0.3.7")
            {
                samp_ip_addr = SAMP_IP_OFFSET_037;
                samp_port_addr = SAMP_PORT_OFFSET_037;
            }
            else
            {
                MessageBox.Show("Error: Version unknown.", "Error");
                if (noGUI)
                {
                    quitSoftware = true;
                    this.Close();
                }
            }

            //Get game information:
            int base_addr = 0;
            for (int i = 0; i != 50; i++)
            {
                base_addr = mEdit.getModuleAddress("samp.dll");
                if (base_addr != 0)
                    break;
                mEdit.getProcess("gta_sa");
                Thread.Sleep(100);
            }
            if (base_addr == 0)
            {
                MessageBox.Show("Error: Could not find samp.dll module.", "Error");
                if (noGUI)
                {
                    quitSoftware = true;
                    this.Close();
                }
                return;
            }

            //Get game port:
            ushort game_port = 0;
            byte[] a = mEdit.readFromMemory(base_addr + samp_port_addr, 16);
            string gpstr = System.Text.Encoding.Default.GetString(a);
            ushort.TryParse(gpstr, out game_port);
            if (game_port == 0)
            {
                MessageBox.Show("Error: Could not find SA:MP port.", "Error");
                if (noGUI)
                {
                    quitSoftware = true;
                    this.Close();
                }
                return;
            }

            //If the GUI mode is active, use the port stated in the textbox:
            if (!noGUI)
            {
                //If there is a forced port, use it:
                if (forced_port != 0)
                    port = forced_port;
                else
                {
                    ushort.TryParse(portTB.Text, out port);
                    if (port == 0)
                    {
                        MessageBox.Show("Error: Invalid port.", "Error");
                        return;
                    }
                }
            }
            else    //GUI mode not active
            {
                //There is a forced port..
                if (forced_port != 0)
                {
                    port = forced_port;
                }
                else    //There is no forced port, use standard port
                {
                    //Use standard port and look for IP:
                    port = 42690;
                }

                //Get the server IP and convert the address to a string:
                a = mEdit.readFromMemory(base_addr + samp_ip_addr, 16);
                for (int i = 0; i != a.Length; i++)
                {
                    if (a[i] == 0)
                    {
                        addStr = System.Text.Encoding.Default.GetString(a, 0, i - 1);
                    }
                }
            }
            //Report that everything went fine:
            networkThread.ReportProgress(0, (noGUI ? addStr : addressTB.Text) + ":" + port);
            //Start the networking server:
            connectedOnce = true;
            net = new networkingClient(ref this.networkThread);
            net.start(noGUI ? addStr : addressTB.Text, port, game_port);
        }

        private void networkThread_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            switch (e.ProgressPercentage)
            {
                case 0:
                    connectionLabel.Text = "Trying to connect...";
                    connectBtn.Text = "Disconnect";
                    logRTB.Text += "Trying to connect to " + e.UserState.ToString() + " ...\n";
                    break;
                //RAKNET:
                case (int)RakNet.DefaultMessageIDTypes.ID_CONNECTION_REQUEST_ACCEPTED:
                    connectionLabel.Text = "Connected!";
                    logRTB.Text += "Connected to " + e.UserState.ToString() + ".\n";
                    break;
                //CLIENT&SERVERINFO:
                case (int)RakNetStates.ID_CLIENTINFO:
                    ClientInfo info = new ClientInfo();
                    info.type = (int)RakNetStates.ID_CLIENTINFO;
                    info.money_color = InternalImplementations.GetMoneyColor(ref mEdit);
                    info.health_color = InternalImplementations.GetHealthColor(ref mEdit);
                    info.wanted_color = InternalImplementations.GetWantedColor(ref mEdit);
                    info.radio_color = InternalImplementations.GetRadioColor(ref mEdit);
                    info.camera_mode = InternalImplementations.GetVehicleCameraMode(ref mEdit);
                    info.aircraft_height = InternalImplementations.GetAircraftHeight(ref mEdit);
                    info.infinite_run = InternalImplementations.GetInfiniteRunState(ref mEdit);
                    info.pickup_size = InternalImplementations.GetPickupSize(ref mEdit);
                    info.fireproof = InternalImplementations.GetPlayerFireproofState(ref mEdit);
                    info.wave_height = InternalImplementations.GetPlayerWaveHeight(ref mEdit);
                    info.wind_density = InternalImplementations.GetPlayerWindDensity(ref mEdit);
                    info.hud_visible = InternalImplementations.GetPlayerHUDState(ref mEdit);
                    info.nv_active = InternalImplementations.GetPlayerNVState(ref mEdit);
                    info.th_active = InternalImplementations.GetPlayerTHState(ref mEdit);
                    info.gravity = InternalImplementations.GetPlayerGravity(ref mEdit);
                    info.time_flies = InternalImplementations.GetTimeFliesBy(ref mEdit);
                    info.wheels_only = InternalImplementations.GetPlayerWheelsVisibility(ref mEdit);
                    info.oxygen = InternalImplementations.GetPlayerOxygen(ref mEdit);
                    info.infinite_oxygen = InternalImplementations.GetPlayerInfOxygenState(ref mEdit);
                    info.green_lights = InternalImplementations.GetPlayerTrafficLightState(ref mEdit);
                    info.radio_station = InternalImplementations.GetPlayerRadioStation(ref mEdit);
                    info.radio_volume = InternalImplementations.GetPlayerRadioVolume(ref mEdit);
                    info.sfx_volume = InternalImplementations.GetPlayerSFXVolume(ref mEdit);
                    info.tele_marker_state = InternalImplementations.GetPlayerEnterExitVisibility(ref mEdit);
                    info.drug_effect = InternalImplementations.GetPlayerStrange2DEffect(ref mEdit);
                    info.resolution_w = InternalImplementations.GetPlayerResolutionWidth(ref mEdit);
                    info.resolution_h = InternalImplementations.GetPlayerResolutionHeight(ref mEdit);
                    info.healthbar_border = InternalImplementations.GetPlayerHealthBarBorderState(ref mEdit);
                    info.current_region = InternalImplementations.GetPlayerCurrentRegion(ref mEdit);
                    info.player_deform = InternalImplementations.GetPlayerWeirdDeformFactor(ref mEdit);
                    info.near_cam_dist = InternalImplementations.GetPlayerNearCameraDistance(ref mEdit);
                    info.norm_cam_dist = InternalImplementations.GetPlayerDefaultCameraDistance(ref mEdit);
                    info.far_cam_dist = InternalImplementations.GetPlayerFarCameraDistance(ref mEdit);
                    info.disabled_aiming = InternalImplementations.GetDisablePlayerAimingState(ref mEdit);
                    info.disabled_camchange = InternalImplementations.GetDisableCameraChangeOnFootState(ref mEdit);
                    info.calm_wave_height = InternalImplementations.GetPlayerWaveHeightEx(ref mEdit);
                    info.underwater_effect = InternalImplementations.GetPlayerCameraUnderWaterEffectState(ref mEdit);
                    info.horizontal_screen_lines = InternalImplementations.GetPlayerHorizontalLinesEffectState(ref mEdit);
                    info.horizon_effect = InternalImplementations.GetPlayerBrightHorizonState(ref mEdit);
                    info.fps = InternalImplementations.GetPlayerFPS(ref mEdit);
                    info.green_flames_1 = InternalImplementations.GetPlayerFlameColor1(ref mEdit);
                    info.green_flames_2 = InternalImplementations.GetPlayerFlameColor2(ref mEdit);
                    info.rain_value = InternalImplementations.GetPlayerRainingValue(ref mEdit);
                    info.grey_radar = InternalImplementations.GetPlayerRadarState(ref mEdit);
                    info.menuID = InternalImplementations.GetMenuID(ref mEdit);
                    info.light_color = InternalImplementations.GetLightColor(ref mEdit);
                    info.water_color = InternalImplementations.GetWaterColor(ref mEdit);
                    info.radar_disabled = InternalImplementations.GetRadarDisabled(ref mEdit);
                    info.race_cp_invisible = InternalImplementations.GetPlayerRaceCPInvisibleState(ref mEdit);
                    info.version = version;
                    delegateSendClientInfoToServer d = net.sendClientInfoToServer;
                    d(info);
                    break;
                case (int)RakNetStates.ID_SERVERINFO:
                    ServerInfo sinfo = (ServerInfo)e.UserState;
                    InternalImplementations.SetHydraRocketDelay(sinfo.hydra_rocket_delay, ref mEdit);
                    break;
                case (int)RakNetStates.ID_NOISEINFO:
                    NoiseEffectInfo ninfo = (NoiseEffectInfo)e.UserState;
                    NativeImplementations.SetNoiseInfo(ninfo, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETDRAWDISTANCE:
                    drawDistanceParameters p = (drawDistanceParameters)e.UserState;
                    NativeImplementations.SetDrawDistance(p.weather, p.time, p.drawDistance, ref mEdit, false);
                    break;
                //FUNCTIONS:
                case (int)RakNetStates.ID_HUD_SETMONEYCOLOR:
                    NativeImplementations.HUD_SetMoneyColor((uint)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_HUD_SETWANTEDCOLOR:
                    NativeImplementations.HUD_SetWantedColor((uint)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_HUD_SETHEALTHCOLOR:
                    NativeImplementations.HUD_SetHealthColor((uint)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_HUD_SETRADIOCOLOR:
                    NativeImplementations.HUD_SetRadioColor((uint)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETPLAYERVEHICLECAMERAMODE:
                    NativeImplementations.SetPlayerVehicleCameraMode((int)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETAIRCRAFTHEIGHT:
                    NativeImplementations.SetPlayerAircraftHeight((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_TOGGLEINFINITERUN:
                    NativeImplementations.ToggleInfiniteRun(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETPICKUPSIZE:
                    NativeImplementations.SetPickupSize((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_TOGGLEFIREPROOF:
                    NativeImplementations.TogglePlayerFireproofState(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETWAVEHEIGHT:
                    NativeImplementations.SetWaveHeight((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETWINDDENSITY:
                    NativeImplementations.SetWindDensity((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETHUDVISIBLE:
                    NativeImplementations.SetHUDVisible(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETNV:
                    NativeImplementations.SetNVState(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETTH:
                    NativeImplementations.SetTHVisible(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETGRAVITY:
                    NativeImplementations.SetPlayerGravity((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETTIMEFLIES:
                    NativeImplementations.SetTimeFlies(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETWHEELSONLY:
                    NativeImplementations.SetPlayerWheelVisibility(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETOXYGEN:
                    NativeImplementations.SetPlayerOxygen((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETINFINITYOXYGEN:
                    NativeImplementations.SetPlayerInfOxygenState(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETALLGREENLIGHTS:
                    NativeImplementations.SetPlayerTrafficLightState(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETRADIOVOL:
                    NativeImplementations.SetPlayerRadioVolume((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETSFXVOL:
                    NativeImplementations.SetPlayerSFXVolume((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETTELEMARKERSINVISIBLE:
                    NativeImplementations.SetPlayerEnterExitVisibility(ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETDRUGEFFECT:
                    NativeImplementations.SetPlayerStrange2DEffect(ref mEdit);
                    break;
                case (int)RakNetStates.ID_DRAWHEALTHBARBORDER:
                    NativeImplementations.SetPlayerHealthBarBorderState(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETFOGDENSITY:
                    NativeImplementations.SetPlayerFogDensity((float)e.UserState, ref mEdit, ref localInf);
                    break;
                case (int)RakNetStates.ID_SETRAINDENSITY:
                    NativeImplementations.SetPlayerRainDensity((float)e.UserState, ref mEdit, ref localInf);
                    break;
                case (int)RakNetStates.ID_SETPLAYERDEFORM:
                    NativeImplementations.SetPlayerWeirdDeformFactor((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETNEARCAM:
                    NativeImplementations.SetPlayerNearCameraDistance((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETNORMCAM:
                    NativeImplementations.SetPlayerDefaultCameraDistance((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETFARCAM:
                    NativeImplementations.SetPlayerFarCameraDistance((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETDISABLEDCAMCHANGE:
                    NativeImplementations.DisablePlayerCameraChangeOnFoot(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETDISABLEDAIMING:
                    NativeImplementations.DisablePlayerAiming(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETCALMWAVEHEIGHT:
                    NativeImplementations.SetPlayerWaveHeightEx((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_FIREPLAYERCAMERA:
                    NativeImplementations.TakePlayerPhoto(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_TOGGLEHORISCREENLINES:
                    NativeImplementations.TogglePlayerHorizontalLinesEffect(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_TOGGLEUNDERWARTEREFFECT:
                    NativeImplementations.TogglePlayerCameraUnderwaterEffect(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETGREENFLAMES1:
                    NativeImplementations.SetPlayerFlameColor1((int)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETGREENFLAMES2:
                    NativeImplementations.SetPlayerFlameColor2((int)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_HORIZONEFFECT:
                    NativeImplementations.SetPlayerBrightHorizonEffect(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_RAINEFFECT:
                    NativeImplementations.SetPlayerRainDensityEx((float)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETGRAYRADER:
                    NativeImplementations.SetGreyRadarState(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETLIGHTCOLOR:
                    NativeImplementations.SetLightColor((uint)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETWATERCOLOR:
                    NativeImplementations.SetWaterColor((uint)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETRADARDISABLE:
                    NativeImplementations.SetRadarDisabled(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETRACECPINVISIBLE:
                    NativeImplementations.TogglePlayerRaceCPInvisible(Convert.ToBoolean(e.UserState), ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETCLOULDCOLORLOW:
                    NativeImplementations.SetLowCloudColor((uint)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_SETCLOUDCOLORMID:
                    NativeImplementations.SetMidCloudColor((uint)e.UserState, ref mEdit);
                    break;
                case (int)RakNetStates.ID_GETFOLDERS:
                case (int)RakNetStates.ID_GETFILES:
                    fileInformationRequest f = (fileInformationRequest)e.UserState;
                    RakNet.RakString rakStr;
                    if (e.ProgressPercentage == (int)RakNetStates.ID_GETFOLDERS)
                        rakStr = InternalImplementations.ListFolders(new string(f.folder), Application.StartupPath, f.recursive, ref mEdit);
                    else
                        rakStr = InternalImplementations.ListFiles(new string(f.folder), Application.StartupPath, f.recursive, ref mEdit);
                    net.sendMessageToServer((RakNetStates)e.ProgressPercentage, rakStr);
                    /*delegateSendMessageToServer _d = net.sendMessageToServer;
                    _d(RakNetStates.ID_GETFOLDERS, rakStr);*/
                    break;
            }
        }

        private void networkThread_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            /*if (noGUI)
            {
                quitSoftware = true;
                //this.Close();
            }
            else
            {*/
            logRTB.Text += "Aborted connection.\n";
            if (mEdit.processActive)
                connectionLabel.Text = "Process found. Ready to connect.";
            else
                connectionLabel.Text = "Disconnected. No process found.";
            connectBtn.Text = "Connect";
            //}
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            /*if (noGUI && !quitSoftware)
                e.Cancel = true;
            else
            {*/
            if (quitSoftware || !quitSoftware)
                if (networkThread.IsBusy)
                    networkThread.CancelAsync();
            //}
        }

        private void forcedPortTB_TextChanged(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\forcedport.data";
            if (forcedPortTB.Text.Length == 0)
            {
                File.Delete(path);
                portTB.Enabled = true;
            }
            else
            {
                StreamWriter s = new StreamWriter(path, false);
                s.Write(forcedPortTB.Text);
                s.Close();
                portTB.Enabled = false;
            }
        }

        private void versionCB_SelectedIndexChanged(object sender, EventArgs e)
        {
            string path = Application.StartupPath + "\\version.data";
            StreamWriter s = new StreamWriter(path);
            s.Write(versionCB.SelectedIndex);
            s.Close();
        }

        private void logoPB_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://sareallife.org");
        }
    }
}

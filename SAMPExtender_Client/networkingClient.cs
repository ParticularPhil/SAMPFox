using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.InteropServices;
using RakNet;

namespace SAMPExtender_Client
{
    class networkingClient
    {
        private System.ComponentModel.BackgroundWorker parentWorker;
        RakNet.RakPeerInterface peer = null;
        SystemAddress serverAddress;
        ServerInfo serverInfo;
        bool connected,abortLoop = false;
        ushort game_port;


        public networkingClient(ref System.ComponentModel.BackgroundWorker w)
        {
            parentWorker = w;
        }
        public bool start(string ip,ushort port,ushort game_port)
        {
            this.game_port = game_port;

            peer = RakNet.RakPeerInterface.GetInstance();
            RakNet.Packet packet;
            
            RakNet.SocketDescriptor sd = new SocketDescriptor();
            peer.Startup(1, sd, 1);

            peer.Connect(ip, port, this.game_port.ToString(), this.game_port.ToString().Length);
            
            while (true)
            {
                //Connection active:
                if (connected)
                {
                    if (peer.GetConnectionState(serverAddress) == RakNet.ConnectionState.IS_DISCONNECTED)
                    {
                        //If not, abort.
                        break;
                    }
                }
               

                //Be ready to abort:
                if (parentWorker.CancellationPending)
                    break;

                //Receive messages:
                for (packet = peer.Receive(); packet != null; peer.DeallocatePacket(packet), packet = peer.Receive())
                {
                    int i = 0;
                    uint ui = 0;
                    float f = 0F;
                    /*bool b = false;*/
                    byte[] a = null;
                    switch (packet.data[0])
                    {
                            /*RakNet Addresses:*/
                        case (byte)RakNet.DefaultMessageIDTypes.ID_CONNECTION_REQUEST_ACCEPTED:
                            {
                                serverAddress = packet.systemAddress;
                                parentWorker.ReportProgress((int)RakNet.DefaultMessageIDTypes.ID_CONNECTION_REQUEST_ACCEPTED,ip+":"+port);
                                connected = true;
                                break;
                            }
                            /*Client&ServerInfo:*/
                        case (byte)RakNetStates.ID_KICK:
                            {
                                abortLoop = true;
                                break;
                            }
                        case (byte)RakNetStates.ID_CLIENTINFO:
                            {
                                parentWorker.ReportProgress((int)RakNetStates.ID_CLIENTINFO);
                                break;
                            }
                        case (byte)RakNetStates.ID_SERVERINFO:
                            {
                                GCHandle handle = GCHandle.Alloc(packet.data, GCHandleType.Pinned);
                                ServerInfo info = (ServerInfo)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(ServerInfo));
                                handle.Free();
                                if (!info.Equals(serverInfo))
                                {
                                    parentWorker.ReportProgress((int)RakNetStates.ID_SERVERINFO, info);
                                    serverInfo = info;
                                }
                                break;
                            }
                        case (byte)RakNetStates.ID_NOISEINFO:
                            {
                                GCHandle handle = GCHandle.Alloc(packet.data, GCHandleType.Pinned);
                                NoiseEffectInfo info = (NoiseEffectInfo)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(NoiseEffectInfo));
                                handle.Free();
                                parentWorker.ReportProgress((int)RakNetStates.ID_NOISEINFO, info);
                                break;
                            }
                        case (byte)RakNetStates.ID_SETDRAWDISTANCE:
                            {
                                GCHandle handle = GCHandle.Alloc(packet.data, GCHandleType.Pinned);
                                drawDistanceParameters p = (drawDistanceParameters)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(drawDistanceParameters));
                                handle.Free();
                                parentWorker.ReportProgress((int)RakNetStates.ID_SETDRAWDISTANCE, p);
                                break;
                            }
                        case (byte)RakNetStates.ID_GETFOLDERS:
                        case (byte)RakNetStates.ID_GETFILES:
                            {
                                GCHandle handle = GCHandle.Alloc(packet.data, GCHandleType.Pinned);
                                fileInformationRequest r = (fileInformationRequest)Marshal.PtrToStructure(handle.AddrOfPinnedObject(), typeof(fileInformationRequest));
                                handle.Free();
                                r.recursive = r.recursive ? false : true;
                                parentWorker.ReportProgress((int)packet.data[0], r);
                                break;
                            }
                            /*Unsigned integer parameter:*/
                        case (byte)RakNetStates.ID_HUD_SETMONEYCOLOR:
                        case (byte)RakNetStates.ID_HUD_SETWANTEDCOLOR:
                        case (byte)RakNetStates.ID_HUD_SETHEALTHCOLOR:
                        case (byte)RakNetStates.ID_HUD_SETRADIOCOLOR:
                        case (byte)RakNetStates.ID_SETLIGHTCOLOR:
                        case (byte)RakNetStates.ID_SETWATERCOLOR:
                        case (byte)RakNetStates.ID_SETCLOULDCOLORLOW:
                        case (byte)RakNetStates.ID_SETCLOUDCOLORMID:
                            {
                                RakNet.BitStream stream = new BitStream(packet.data, packet.length, false);
                                stream.IgnoreBytes(1);
                                //We cannot read to uint directly!
                                a = new byte[4];
                                stream.ReadBits(a, 32, false);
                                //Array.Reverse(a); //LittleEndian/BigEndian
                                ui = BitConverter.ToUInt32(a, 0);
                                parentWorker.ReportProgress((int)packet.data[0], ui);
                                break;
                            }
                            /*Signed integer parameter:*/
                        case (byte)RakNetStates.ID_SETPLAYERVEHICLECAMERAMODE:
                        case (byte)RakNetStates.ID_SETGREENFLAMES1:
                        case (byte)RakNetStates.ID_SETGREENFLAMES2:
                            {
                                RakNet.BitStream stream=new BitStream(packet.data,packet.length,false);
                                stream.IgnoreBytes(1);
                                stream.Read(out i);
                                parentWorker.ReportProgress((int)packet.data[0],i);
                                break;
                            }
                        /*Boolean parameter:*/
                        case (byte)RakNetStates.ID_TOGGLEINFINITERUN:
                        case (byte)RakNetStates.ID_TOGGLEFIREPROOF:
                        case (byte)RakNetStates.ID_SETHUDVISIBLE:
                        case (byte)RakNetStates.ID_SETNV:
                        case (byte)RakNetStates.ID_SETTH:
                        case (byte)RakNetStates.ID_SETTIMEFLIES:
                        case (byte)RakNetStates.ID_SETWHEELSONLY:
                        case (byte)RakNetStates.ID_SETINFINITYOXYGEN:
                        case (byte)RakNetStates.ID_SETALLGREENLIGHTS:
                        case (byte)RakNetStates.ID_SETTELEMARKERSINVISIBLE:
                        case (byte)RakNetStates.ID_SETDRUGEFFECT:
                        case (byte)RakNetStates.ID_DRAWHEALTHBARBORDER:
                        case (byte)RakNetStates.ID_SETDISABLEDAIMING:
                        case (byte)RakNetStates.ID_SETDISABLEDCAMCHANGE:
                        case (byte)RakNetStates.ID_TOGGLEHORISCREENLINES:
                        case (byte)RakNetStates.ID_TOGGLEUNDERWARTEREFFECT:
                        case (byte)RakNetStates.ID_FIREPLAYERCAMERA:
                        case (byte)RakNetStates.ID_HORIZONEFFECT:
                        case (byte)RakNetStates.ID_SETGRAYRADER:
                        case (byte)RakNetStates.ID_SETRADARDISABLE:
                        case (byte)RakNetStates.ID_SETRACECPINVISIBLE:
                            {
                                RakNet.BitStream stream = new BitStream(packet.data, packet.length, false);
                                stream.IgnoreBytes(1);
                                stream.Read(out i);
                                parentWorker.ReportProgress((int)packet.data[0], i);
                                break;
                            }
                            /*Float parameter:*/
                        case (byte)RakNetStates.ID_SETAIRCRAFTHEIGHT:
                        case (byte)RakNetStates.ID_SETPICKUPSIZE:
                        case (byte)RakNetStates.ID_SETWAVEHEIGHT:
                        case (byte)RakNetStates.ID_SETWINDDENSITY:
                        case (byte)RakNetStates.ID_SETOXYGEN:
                        case (byte)RakNetStates.ID_SETRADIOVOL:
                        case (byte)RakNetStates.ID_SETSFXVOL:
                        case (byte)RakNetStates.ID_SETFOGDENSITY:
                        case (byte)RakNetStates.ID_SETRAINDENSITY:
                        case (byte)RakNetStates.ID_SETPLAYERDEFORM:
                        case (byte)RakNetStates.ID_SETNEARCAM:
                        case (byte)RakNetStates.ID_SETNORMCAM:
                        case (byte)RakNetStates.ID_SETFARCAM:
                        case (byte)RakNetStates.ID_SETCALMWAVEHEIGHT:
                        case (byte)RakNetStates.ID_RAINEFFECT:
                        case (byte)RakNetStates.ID_SETGRAVITY:
                            {
                                RakNet.BitStream stream = new BitStream(packet.data, packet.length, false);
                                stream.IgnoreBytes(1);
                                stream.Read(out f);
                                parentWorker.ReportProgress((int)packet.data[0], f);
                                break;
                            }
                            
                    }
                }
                //Abort server:
                if (abortLoop)
                    break;
                System.Threading.Thread.Sleep(1);
            }
            if (connected)
            {
                peer.CloseConnection(serverAddress, true);
                peer.Shutdown(10);
            }
            RakNet.RakPeerInterface.DestroyInstance(peer);
            return true;
        }
        public void sendClientInfoToServer(ClientInfo info)
        {
            byte[] a = SBConverter.getBytesFromStruct(info);
            int size = Marshal.SizeOf(info);
            peer.Send(a, Marshal.SizeOf(info),
                RakNet.PacketPriority.HIGH_PRIORITY, RakNet.PacketReliability.RELIABLE_ORDERED, (char)0, serverAddress, false);   
        }
        public void sendMessageToServer(RakNetStates messageType, RakString content)
        {
            byte[] b = new byte[1];
            b[0] = (byte)RakNetStates.ID_GETFOLDERS;
            BitStream stream = new BitStream(b, (uint)b.Length, true);
            stream.Write(content);

            peer.Send(stream, RakNet.PacketPriority.HIGH_PRIORITY, RakNet.PacketReliability.RELIABLE_ORDERED, (char)0, serverAddress, false);
        }
    }
}

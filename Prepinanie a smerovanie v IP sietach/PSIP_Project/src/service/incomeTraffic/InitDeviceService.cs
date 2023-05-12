using PacketDotNet;
using PSIP_Project.src.controller;
using PSIP_Project.src.model;
using PSIP_Project.src.service.model;
using PSIP_Project.src.service.queue;
using PSIP_Project.src.service.traffic;
using SharpPcap;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using PcapDotNet;
using SharpPcap.LibPcap;
using PSIP_Project.src.service.incomeTraffic;
using PSIP_Project.src.service.filters;
using PSIP_Project.src.service.SysLog;
using PSIP_Project.src.service.checkConnectivity;
using PSIP_Project.src.service.guitimer;

namespace PSIP_Project.src.service
{
    /**
     * 
     * Handle: 
     * 1. find and setup Ethernet Device
     * 2. trigger GUIController actions
     * 3. begin sniffing 
     * 4. ...
     * 
     * **/
    internal class InitDeviceService
    {
        public CallbackPacketService incommingPacketService;
        public HandleQueueService handleQueue;
        public ConnectivityService connectivityService;
        private GuiTimer guiTimer;

        private Thread guiThread;
        private Thread queueThread;
        private bool isTwoDev = false;

        private CaptureDeviceList devices;
        private ILiveDevice ethernet1;
        private ILiveDevice ethernet2;

        private List<string> interfaceNames;

        public InitDeviceService(GuiTimer guiTimer)
        {
            this.guiTimer = guiTimer;
        }


        public (List<int>, List<string>, Dictionary<string, int>) initDevices(List<string> interfaceName)
        {
            List<int> idInterface = new List<int>();
            List<string> activeDev = new List<string>();

            Dictionary<string, int> mappingDevices = new Dictionary<string, int>();

            devices = getDeviceList();

            this.interfaceNames = interfaceName;

            this.ethernet1 = this.findEthernetDevice(devices, interfaceName[0]);
            idInterface.Add(ethernet1.GetHashCode());
            activeDev.Add("eth1");
            mappingDevices.Add("eth1", ethernet1.GetHashCode());

            if(interfaceName.ToArray().Length == 2)
            {
                this.ethernet2 = this.findEthernetDevice(devices, interfaceName[1]);
                this.isTwoDev = true;
                idInterface.Add(ethernet2.GetHashCode());
                activeDev.Add("eth2");
                mappingDevices.Add("eth2", ethernet2.GetHashCode());
            }

            return (idInterface, activeDev, mappingDevices);

        }

        public List<ILiveDevice> getActiveDevices(){

            return new List<ILiveDevice> { this.ethernet1, this.ethernet2 };
        }

        public CaptureDeviceList getDeviceList()
        {
            return CaptureDeviceList.Instance;
        }

        private ILiveDevice findEthernetDevice(CaptureDeviceList devices, string ethName)
        {
            foreach (ILiveDevice device in devices)
            {
                if (device.Name == ethName)
                {
                    return device;
                }
            }
            return null;
        }

        private void setupEthernetDevice(ICaptureDevice ethernetDevice)
        {
            //register EventListener
            //meaby trouble with that it start new thread
            ethernetDevice.OnPacketArrival += new PacketArrivalEventHandler(incommingPacketService.onPacketArrival);
            ethernetDevice.Open(DeviceModes.NoCaptureLocal | DeviceModes.Promiscuous);
        }

        public void stopSniffing()
        {
            // not working :'{

            this.connectivityService.stopCheckingConnectivity();

            this.ethernet1.StopCapture();

            this.guiThread.Abort();

            if(this.isTwoDev)
            {
                this.ethernet2.StopCapture();
            }
            
            this.queueThread.Join();
            this.handleQueue.backgroundThreadStop = true;


        }

        public void startSniffing()
        {
            // Creating and initializing thread
            // Using thread class and
            // ThreadStart constructor
            this.queueThread = new Thread(new ThreadStart(this.handleQueue.handlePacketsFromQueue));
            this.handleQueue.backgroundThreadStop = false;

            this.queueThread.Start();

            this.guiThread = new Thread(new ThreadStart(this.guiTimer.startMinusOne));
            this.guiThread.Start();

            setupEthernetDevice(this.ethernet1);
            ethernet1.StartCapture();
            

            if (this.isTwoDev)
            {
                setupEthernetDevice(this.ethernet2);
                ethernet2.StartCapture();
            }
            this.connectivityService.startCheckingConnectivity();
        }

    }
}

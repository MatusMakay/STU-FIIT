using PacketDotNet;
using PSIP_Project.src.service.incomeTraffic;
using PSIP_Project.src.service.model;
using SharpPcap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PSIP_Project.src.service.queue
{
    class HandleQueueService
    {
        private AnalyzePacketService analyzePacketService;
        public CallbackPacketService incommingPacketService;

        public List<(Packet, int)> packetQueue;
        public object queueLock;

        public bool backgroundThreadStop = false;

        private int hashDeviceOne;
        public HandleQueueService(int hashDeviceOne, AnalyzePacketService analyzePacketService)
        {
            this.packetQueue = new List<(Packet, int)>();
            this.hashDeviceOne = hashDeviceOne;
            this.analyzePacketService = analyzePacketService;
            this.queueLock = new object();
        }

        public void handlePacketsFromQueue()
        {
            while (!backgroundThreadStop)
            {
                bool shouldSleep = true;

                lock (queueLock)
                {
                    if (packetQueue.Count != 0)
                    {
                        shouldSleep = false;
                    }
                }

                if (shouldSleep)
                {
                    System.Threading.Thread.Sleep(150);
                }
                else // should process the queue
                {
                    List<(Packet, int)> ourQueue;
                    lock (queueLock)
                    {
                        // swap queues, giving the capture callback a new one
                        ourQueue = packetQueue;
                        packetQueue = new List<(Packet, int)>();
                        incommingPacketService.packetQueue = this.packetQueue;
                    }

                    foreach ((Packet, int) packetInfo in ourQueue)
                    {
                        if (packetInfo.Item2 == this.hashDeviceOne)
                        {
                            analyzePacketService.analyzeBeforeSwitch(packetInfo.Item1, packetInfo.Item2);
                        }
                        else
                        {
                            analyzePacketService.analyzeBeforeSwitch(packetInfo.Item1, packetInfo.Item2);
                        }
                    }
                }
            }
        }

    }
}

    
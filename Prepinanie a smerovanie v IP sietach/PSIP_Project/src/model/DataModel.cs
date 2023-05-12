using PacketDotNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;
using PSIP_Project.src.service;

namespace PSIP_Project.src.model
{
    internal class DataModel
    {
        private Gui GUI;
        public SwitchService switchService;

        private List<string> updateProtocols;
        private List<int> updateCounts;

        object modelLock = new object();

        Dictionary<string, int> inStatisticEth1;
        Dictionary<string, int> outStatisticEth1;

        Dictionary<string, int> inStatisticEth2;
        Dictionary<string, int> outStatisticEth2;

        // mac, port, time
        List<(string, string, int)> macTABLE = new List<(string, string, int)>();

        public int timerRecords = 10000;

        public bool canChange;
       
        public DataModel(Gui GUI) 
        { 
            this.GUI = GUI;
            
            this.canChange = true;
            this.inStatisticEth1 = initializeDic();
            this.outStatisticEth1 = initializeDic();
            this.inStatisticEth2 = initializeDic();
            this.outStatisticEth2 = initializeDic();
        }

        private Dictionary<string,int> initializeDic()
        {
            return new Dictionary<string, int> {
                { "ETH", 0 },
                { "IP", 0 },
                { "ARP", 0 },
                { "TCP", 0 },
                { "UDP", 0 },
                { "ICMP", 0 },
                { "HTTP", 0 },
                { "HTTPS", 0 },
                { "TOTAL", 0 },
            };
        }

        public void updateMacTable(string mac, string port)
        {
            lock (modelLock)
            {

                (string, string, int) newRecord = (mac, port, (int)(this.timerRecords/1000));
                this.macTABLE.Add(newRecord);

                this.updateGuiMacAddRecord(newRecord);
            }
        }

        private Dictionary<string, int> setDic(string end, string eth)
        {
            if (eth == "eth1" && end == "IN1")
                return this.inStatisticEth1;

            if (eth == "eth2" && end == "IN2")
                return this.inStatisticEth2;
            
            if (eth == "eth1" && end == "OUT1")
                return this.outStatisticEth1;

            if (eth == "eth2" && end == "OUT2")
                return this.outStatisticEth2;
            
            return null;
        }

        private void fillUpdateProtocols(string protocol, string end, string eth)
        {

            Dictionary<string, int> dic = setDic(end, eth);
 
            // Only ethernet protocols
            if(protocol == "ARP")
            {
                dic["ARP"] += 1;
                dic["ETH"] += 1;
                dic["TOTAL"] += 1;

                this.updateCounts.Add(dic["ARP"]);
                this.updateCounts.Add(dic["ETH"]);
                this.updateCounts.Add(dic["TOTAL"]);

                this.updateProtocols.Add($"ARP_{end}");
                this.updateProtocols.Add($"ETH_{end}");
                this.updateProtocols.Add($"TOTAL_{end}");
            }

            // IP protocols
            if (protocol == "TCP" || protocol == "UDP" || protocol == "ICMP" || protocol == "HTTP")
            {
                this.updateProtocols.Add($"IP_{end}");
                this.updateProtocols.Add($"TOTAL_{end}");
                this.updateProtocols.Add($"ETH_{end}");

                dic["IP"] += 1;
                dic["ETH"] += 1;
                dic["TOTAL"] += 1;

                this.updateCounts.Add(dic["IP"]);
                this.updateCounts.Add(dic["TOTAL"]);
                this.updateCounts.Add(dic["ETH"]);

                if (protocol == "TCP") 
                {
                    dic["TCP"] += 1;
                    this.updateProtocols.Add($"TCP_{end}");
                    this.updateCounts.Add(dic["TCP"]);

                }

                else if (protocol == "UDP") 
                {
                    dic["UDP"] += 1;
                    this.updateProtocols.Add($"UDP_{end}");
                    this.updateCounts.Add(dic["UDP"]);
                }

                else if (protocol == "ICMP")
                {
                    dic["ICMP"] += 1;
                    this.updateProtocols.Add($"ICMP_{end}");
                    this.updateCounts.Add(dic["ICMP"]);
                }

                else if (protocol == "HTTP")
                {
                    dic["HTTP"] += 1;
                    dic["TCP"] += 1;

                    this.updateProtocols.Add($"HTTP_{end}");
                    this.updateProtocols.Add($"TCP_{end}");

                    this.updateCounts.Add(dic["HTTP"]);
                    this.updateCounts.Add(dic["TCP"]);
                }
            }
           
            else if (protocol == "TOTAL")
            {
                this.updateProtocols.Add($"TOTAL_{end}");
                this.updateCounts.Add(dic["TOTAL"]);
            }

        }

        private void resetDictionary(Dictionary<string, int> dic)
        {
            dic["ETH"] = 0;
            dic["IP"] = 0;
            dic["ARP"] = 0;
            dic["TCP"] = 0;
            dic["UDP"] = 0;
            dic["ICMP"] = 0;
            dic["HTTP"] = 0;
            dic["HTTPS"] = 0;
            dic["TOTAL"] = 0;
        }

        public void resetEth1()
        {
            resetDictionary(inStatisticEth1);
            resetDictionary(outStatisticEth1);
        }

        public void resetEth2()
        {
            resetDictionary(inStatisticEth2);
            resetDictionary(outStatisticEth2);
        }

        // When update from trafic service is triggered you need to call specific method
        public void updateInEth(string protocol, string eth) 
        {

            this.updateProtocols = new List<string> ();
            this.updateCounts = new List<int> ();

            string tmp;

            if (eth == "eth1")
                tmp = "IN1";
            else
                tmp = "IN2";

            this.fillUpdateProtocols(protocol, tmp, eth);

            this.updateGuiStatistic(eth);

        }

        public void updateOutEth(string protocol, string eth)
        {

            this.updateProtocols = new List<string>();
            this.updateCounts = new List<int>();

            string tmp;

            if (eth == "eth1")
                tmp = "OUT1";
            else
                tmp = "OUT2";

            this.fillUpdateProtocols(protocol, tmp, eth);

            this.updateGuiStatistic(eth);

        }
        private void updateGuiStatistic( string eth)
        {
            this.GUI.updateEth(eth, this.updateProtocols, this.updateCounts);  
        }

        private void updateGuiMacAddRecord((string, string, int) newRecord)
        {
            this.GUI.updateMACOneRecord(newRecord);
        }

        private void updateGuiMacALL(List<(string, string, int)> newMacTABLE)
        {
            this.GUI.updateMACAll(newMacTABLE);
        }

        public void cableDisconnect(string ethDisconected)
        {
            if(this.macTABLE.ToArray().Length > 0)
            {
                lock (modelLock)
                {
                    List<(string, string, int)> newMacTABLE = new List<(string, string, int)>();

                    foreach ((string, string, int) record in this.macTABLE)
                    {
                        if (record.Item2 != ethDisconected)
                        {
                            newMacTABLE.Add(record);
                        }
                    }

                    this.macTABLE = newMacTABLE;

                    this.updateGuiMacALL(newMacTABLE);
                }
            }
        }

        public void cableSwitch()
        {
            if(this.macTABLE.ToArray().Length > 0)
            {
                lock (modelLock)
                {
                    List<(string, string, int)> newMacTABLE = new List<(string, string, int)>();

                    foreach ((string, string, int) oldRecord in this.macTABLE)
                    {
                        (string, string, int) newRecord;
                        if (oldRecord.Item2 == "eth1")
                        {
                            newRecord = (oldRecord.Item1, "eth2", oldRecord.Item3);
                        }
                        else
                        {
                            newRecord = (oldRecord.Item1, "eth1", oldRecord.Item3);
                        }
                        newMacTABLE.Add(newRecord);
                    }

                    this.macTABLE = newMacTABLE;

                    this.updateGuiMacALL(newMacTABLE);
                }
            }
           
        }
        public void minusOne()
        {
            if (this.macTABLE.ToArray().Length > 0)
            {

                lock (modelLock)
                {

                    //mac,port,time
                    List<(string, string, int)> newMacTABLE = new List<(string, string, int)>();
                    
                    List<string> deletedMac = new List<string>();
                    bool changed = false;

                    foreach ((string, string, int) record in macTABLE)
                    {
                        if ((record.Item3 - 1) > 0)
                        {
                            (string, string, int) newRecord = (record.Item1, record.Item2, record.Item3 - 1);
                            newMacTABLE.Add(newRecord);
                        }
                        // if record has no time in table inform switch service about it
                        else
                        {
                            changed = true;
                            deletedMac.Add(record.Item1);
                        }
                    }

                    this.macTABLE = newMacTABLE;
                    this.updateGuiMacALL(this.macTABLE);

                    if (changed)
                    {
                        this.switchService.removeMacAfterMinusOne(deletedMac);
                    }

                }
            }
        }

        public void clearMacTable()
        {
            this.macTABLE.Clear();
        }

        public void changeTimeInMacTable(int newTime, int oldTime)
        {
            lock (modelLock)
            {
                List<(string, string, int)> newMac = new List<(string, string, int)>();

                foreach ((string, string, int) record in macTABLE)
                {
                    int time = record.Item3;

                    int timeInTable = oldTime - time;

                    int newTimeINtable = (int)((newTime - timeInTable) / 1000) ;

                    newMac.Add((record.Item1, record.Item2, newTimeINtable));

                }
                this.macTABLE = newMac;
            }

            
        }

        //Diskutabilne spytaj sa niekoho
        private List<string> calculateWhatShouldBeRemovedAndChangeTimeInMac(int newTime, int oldTime)
        {
            List<string> removeMac = new List<string>();

            lock (modelLock)
            {
                List<(string, string, int)> newMac = new List<(string, string, int)>();

                foreach ((string, string, int) record in macTABLE)
                {
                    int time = record.Item3;

                    int timeInTable = oldTime - time;

                    int newTimeINtable = (int)((newTime - timeInTable) / 1000);

                    if (timeInTable > newTime)
                    {
                        removeMac.Add(record.Item1);
                    }
                    else
                    {
                        newMac.Add((record.Item1, record.Item2, newTimeINtable));
                    }
                }

                this.macTABLE = newMac;
            }

            if(removeMac.ToArray().Length == 0)
            {
                return null;
            }
            else 
                return removeMac;
        }
        public void timerChanged(int newTime)
        {
            // if something will be deleted inform switchService
            // newTime > oldTime => nothing to check only change value
            // newTime < oldTime => calculate if should dissapear
            int oldTime = timerRecords;
            this.timerRecords = newTime;

            int difference = oldTime - newTime;

            // only change delete something when 
            if (difference > 0)
            {
                List<string> deleteMac = calculateWhatShouldBeRemovedAndChangeTimeInMac(newTime, oldTime);

                if(deleteMac != null)
                {
                    switchService.removeMacAfterMinusOne(deleteMac);
                }
            }

            else
            {
                this.changeTimeInMacTable(newTime, oldTime);
            }

        }

    }
}

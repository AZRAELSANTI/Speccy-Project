﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.Windows;

namespace WpfApp1.ViewModel
{

    public class PCInfoViewModel
    {
        public string GpuInfo { get; set; }
        public string CpuInfo { get; set; }

        public void GetSystemInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor");
            foreach (ManagementObject obj in searcher.Get())
            {
                CpuInfo = $"{obj["Name"]} ";
                   

            }

            searcher = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            foreach (ManagementObject obj in searcher.Get())
            {
                GpuInfo = $"{obj["Name"]} " ;
            }
        }

        public string DiskInfo { get; set; }

        public void GetDiskInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_DiskDrive");
            foreach (ManagementObject obj in searcher.Get())
            {
                DiskInfo += $"Disk Model: {obj["Model"]} {ConvertBytesToGB(obj["Size"])} GB\n";
            }
        }

        private long ConvertBytesToGB(object bytes)
        {
            return Convert.ToInt64(bytes) / (1024 * 1024 * 1024);
        }

        public string RamInfo { get; set; }
        public string MotherboardInfo { get; set; }

        public void GetRamInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_PhysicalMemory");
            foreach (ManagementObject obj in searcher.Get())
            {
                RamInfo += $"Capacity: {ConvertBytesToGB(obj["Capacity"])} GB - Speed: {obj["Speed"]} MHz\n";
            }
        }
        public string OSInfo { get; set; }
        public void GetOSInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem");
            foreach (ManagementObject os in searcher.Get())
            {
                OSInfo = os["Caption"].ToString() + " " + os["Version"].ToString();
            }
        }
        public void GetMotherboardInfo()
        {
            ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_BaseBoard");
            ManagementObjectCollection collection = searcher.Get();

            foreach (ManagementObject obj in collection)
            {
                MotherboardInfo = $"{obj["Manufacturer"]}|{obj["Product"]}";
                break; // Assuming there is only one motherboard
            }
        }

    }
}

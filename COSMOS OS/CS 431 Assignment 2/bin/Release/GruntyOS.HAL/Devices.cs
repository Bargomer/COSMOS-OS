using Cosmos.Hardware.BlockDevice;
using System;
using System.Collections.Generic;
namespace GruntyOS.HAL
{
	public class Devices
	{
		public class device
		{
			public string name;
			public BlockDevice dev;
		}
		public static List<Devices.device> dev = new List<Devices.device>();
		public static BlockDevice getDevice(string name)
		{
			for (int i = 0; i < Devices.dev.Count; i++)
			{
				if (Devices.dev[i].name == name)
				{
					return Devices.dev[i].dev;
				}
			}
			throw new Exception("Device not found!");
		}
	}
}

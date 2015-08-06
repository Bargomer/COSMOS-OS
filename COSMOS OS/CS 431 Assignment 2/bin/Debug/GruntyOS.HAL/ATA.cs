using Cosmos.Hardware.BlockDevice;
using GruntyOS.IO;
using System;
namespace GruntyOS.HAL
{
	public class ATA
	{
		private class a
		{
			public byte a;
			public byte b;
			public byte c;
			public ushort d;
			public byte e;
			public byte f;
			public byte g;
		}
		private static MemoryStream a = new MemoryStream(1024);
		private static char b = 'a';
		public static void ReadMBR(AtaPio dev)
		{
			BinaryReader binaryReader = new BinaryReader(ATA.a);
			for (int i = 0; i < 4; i++)
			{
				binaryReader.BaseStream.Position = 446 + i * 16 + 8;
				uint num = binaryReader.ReadUInt32();
				uint num2 = binaryReader.ReadUInt32();
				if (num2 != 0u)
				{
					Partition dev2 = new Partition(dev, (ulong)num, (ulong)num2);
					Devices.device device = new Devices.device();
					device.dev = dev2;
					device.name = "/dev/sd" + ATA.b.ToString() + (i + 1).ToString();
					Devices.dev.Add(device);
				}
			}
		}
		public static void Detect()
		{
			int num = 1;
			for (int i = 0; i < BlockDevice.Devices.Count; i++)
			{
				if (BlockDevice.Devices[i] is AtaPio)
				{
					AtaPio ataPio = (AtaPio)BlockDevice.Devices[i];
					ataPio.ReadBlock(0uL, 2u, ATA.a.Data);
					Devices.device device = new Devices.device();
					device.name = "/dev/sd" + ATA.b.ToString();
					device.dev = ataPio;
					Devices.dev.Add(device);
					ATA.b += '\u0001';
				}
				else
				{
					if (BlockDevice.Devices[i] is Partition)
					{
						Partition dev = BlockDevice.Devices[i] as Partition;
						Devices.device device = new Devices.device();
						device.name = "/dev/sda" + num.ToString();
						device.dev = dev;
						num++;
						Devices.dev.Add(device);
					}
				}
			}
		}
	}
}

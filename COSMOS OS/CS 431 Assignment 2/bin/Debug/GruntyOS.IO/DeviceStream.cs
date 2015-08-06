using Cosmos.Hardware.BlockDevice;
using GruntyOS.HAL;
using System;
namespace GruntyOS.IO
{
	public class DeviceStream : ioStream
	{
		private BlockDevice a;
		private byte[] b;
		public DeviceStream(string dname)
		{
			this.a = Devices.getDevice(dname);
			this.b = new byte[this.a.get_BlockSize()];
		}
		public override void Write(byte i)
		{
			uint num = (uint)(this.Position / 512);
			uint num2 = (uint)(this.Position - (int)((num - 1u) * 512u));
			this.a.ReadBlock((ulong)num, 1u, this.b);
			this.b[(int)((UIntPtr)num2)] = i;
			this.a.WriteBlock((ulong)num, 1u, this.b);
			this.Position++;
		}
		public override byte Read()
		{
			uint num = (uint)(this.Position / 512 + 1);
			uint num2 = (uint)(this.Position - (int)((num - 1u) * 512u));
			this.a.ReadBlock((ulong)num, 1u, this.b);
			this.Position++;
			return this.b[(int)((UIntPtr)num2)];
		}
	}
}

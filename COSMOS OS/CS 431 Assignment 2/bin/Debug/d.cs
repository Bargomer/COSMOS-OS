using Cosmos.Hardware.BlockDevice;
using System;
internal class d : BlockDevice
{
	private byte[] a;
	public d(ulong A_0)
	{
		this.a = new byte[A_0 * 512uL];
		this.mBlockCount = A_0;
		this.mBlockSize = 512uL;
	}
	public override void ReadBlock(ulong aBlockNo, uint aBlockCount, byte[] aData)
	{
		uint num = aBlockCount * 512u;
		ulong num2 = aBlockNo * 512uL;
		for (uint num3 = 0u; num3 < num; num3 += 1u)
		{
			aData[(int)((UIntPtr)num3)] = this.a[(int)checked((IntPtr)unchecked(num2 + (ulong)num3))];
		}
	}
	public override void WriteBlock(ulong aBlockNo, uint aBlockCount, byte[] aData)
	{
		uint num = aBlockCount * 512u;
		ulong num2 = aBlockNo * 512uL;
		for (uint num3 = 0u; num3 < num; num3 += 1u)
		{
			this.a[(int)checked((IntPtr)unchecked(num2 + (ulong)num3))] = aData[(int)((UIntPtr)num3)];
		}
	}
}

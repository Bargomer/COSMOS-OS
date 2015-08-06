using System;
namespace GruntyOS.HAL
{
	public class fsEntry
	{
		public string Name;
		public byte Attributes;
		public int Pointer;
		public int Length;
		public byte User = 6;
		public byte Group = 4;
		public byte Global = 4;
		public string Owner;
		public string Time;
		public int Checksum;
	}
}

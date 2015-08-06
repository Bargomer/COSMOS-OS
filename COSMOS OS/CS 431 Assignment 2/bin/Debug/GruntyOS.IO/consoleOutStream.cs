using System;
namespace GruntyOS.IO
{
	public class consoleOutStream : ioStream
	{
		private int a = 0;
		private int b = 0;
		public void Write(string str)
		{
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				this.Write(c);
			}
		}
		public void WriteLine(params object[] obj)
		{
			for (int i = 0; i < obj.Length; i++)
			{
				if (obj[i] is string)
				{
					this.Write(obj[i].ToString());
				}
				else
				{
					if (obj[i] is char)
					{
						this.Write((char)obj[i]);
					}
					else
					{
						if (obj[i] is int)
						{
							this.Write(((int)obj[i]).ToString());
						}
					}
				}
			}
		}
		public void Write(char c)
		{
			ushort num = (ushort)((int)((ushort)Console.BackgroundColor) << 4 | (int)((ushort)Console.ForegroundColor & 15));
			ushort value = (ushort)((int)((byte)c) | (int)num << 8);
			this.Write(BitConverter.GetBytes(value)[0]);
			this.Write(BitConverter.GetBytes(value)[1]);
			this.a++;
			if (this.a > 80)
			{
				this.a = 0;
				this.b++;
			}
		}
		public unsafe override void Write(byte i)
		{
			byte* ptr = 753664;
			ptr[(IntPtr)this.Position / 1] = i;
			this.Position++;
		}
		public unsafe override byte Read()
		{
			byte* ptr = 753664;
			return ptr[(IntPtr)this.Position / 1];
		}
	}
}

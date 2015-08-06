using System;
namespace GruntyOS.IO
{
	public class MemoryStream : ioStream
	{
		private unsafe byte* a = null;
		public override void Close()
		{
		}
		public unsafe override void Write(byte i)
		{
			if (this.a == null)
			{
				base.Write(i);
			}
			else
			{
				this.a[(IntPtr)this.Position / 1] = i;
				this.Position++;
			}
		}
		public unsafe override byte Read()
		{
			byte result;
			if (this.a == null)
			{
				result = base.Read();
			}
			else
			{
				this.Position++;
				result = this.a[(IntPtr)(this.Position - 1) / 1];
			}
			return result;
		}
		public MemoryStream(int size)
		{
			this.a = null;
			base.init(size);
		}
		public MemoryStream(byte[] dat)
		{
			this.a = null;
			this.Data = dat;
		}
		public unsafe MemoryStream(byte* ptr)
		{
			this.a = ptr;
		}
	}
}

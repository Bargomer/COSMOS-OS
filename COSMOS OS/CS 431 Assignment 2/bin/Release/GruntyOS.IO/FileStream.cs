using GruntyOS.HAL;
using System;
namespace GruntyOS.IO
{
	public class FileStream : ioStream
	{
		private string a = "";
		private string b = "";
		public FileStream(string fname, string mode)
		{
			this.a = fname;
			base.init(7000);
			this.b = mode;
			if (mode == "r")
			{
				base.init(FileSystem.Root.readFile(fname).Length);
				this.Data = FileSystem.Root.readFile(fname);
			}
		}
		public override void Flush()
		{
			base.Flush();
		}
		public override void Write(byte i)
		{
			base.Write(i);
		}
		public override byte Read()
		{
			return base.Read();
		}
		public override void Close()
		{
			if (this.b == "w")
			{
				MemoryStream memoryStream = new MemoryStream(this.Position);
				for (int i = 0; i < this.Position; i++)
				{
					memoryStream.Write(this.Data[i]);
				}
				this.Data = memoryStream.Data;
				FileSystem.Root.saveFile(this.Data, this.a, CurrentUser.Username);
			}
		}
	}
}

using GruntyOS.String;
using System;
using System.Collections.Generic;
namespace GruntyOS.HAL
{
	public class DevFS : StorageDevice
	{
		public char Seperator = '/';
		public override void Chmod(string f, string perms)
		{
			throw new NotImplementedException();
		}
		public override void Chown(string f, string perms)
		{
			throw new NotImplementedException();
		}
		public override void Delete(string Path)
		{
			throw new NotImplementedException();
		}
		public override fsEntry[] getLongList(string dir)
		{
			string[] array = this.ListFiles(dir);
			List<fsEntry> list = new List<fsEntry>();
			for (int i = 0; i < array.Length; i++)
			{
				list.Add(new fsEntry
				{
					Name = array[i],
					Owner = "sys",
					Time = b.b(),
					Group = 6,
					User = 6,
					Attributes = 3,
					Global = 0
				});
			}
			return list.ToArray();
		}
		public override string[] ListDirectories(string dir)
		{
			return this.ListJustFiles(dir);
		}
		public override string[] ListJustFiles(string dir)
		{
			return this.ListFiles(dir);
		}
		public override void makeDir(string name, string owner)
		{
			throw new NotImplementedException();
		}
		public override byte[] readFile(string name)
		{
			Devices.device device = new Devices.device();
			for (int i = 0; i < Devices.dev.Count; i++)
			{
				int num = Util.LastIndexOf(Devices.dev[i].name, this.Seperator);
				if (Devices.dev[i].name.Substring(num + 1) == Util.cleanName(name))
				{
					byte[] array = new byte[Devices.dev[i].dev.BlockCount * Devices.dev[i].dev.BlockSize];
					byte[] array2 = new byte[Devices.dev[i].dev.BlockSize];
					for (int j = 0; j < (int)((uint)Devices.dev[i].dev.BlockCount); j++)
					{
						Devices.dev[i].dev.ReadBlock((ulong)((long)j), 1u, array2);
						for (int k = 0; k < (int)((uint)Devices.dev[i].dev.BlockSize); k++)
						{
							array[j * (int)((uint)Devices.dev[i].dev.BlockSize) + k] = array2[k];
						}
					}
					return array;
				}
			}
			throw new Exception("File not found!");
		}
		public override void saveFile(byte[] data, string name, string owner)
		{
			throw new NotImplementedException();
		}
		public override void Move(string s1, string s2)
		{
			throw new NotImplementedException();
		}
		public override string[] ListFiles(string dir)
		{
			List<string> list = new List<string>();
			for (int i = 0; i < Devices.dev.Count; i++)
			{
				int num = Util.LastIndexOf(Devices.dev[i].name, this.Seperator);
				list.Add(Devices.dev[i].name.Substring(num + 1));
			}
			return list.ToArray();
		}
	}
}

using Cosmos.Hardware.BlockDevice;
using GruntyOS.Crypto;
using GruntyOS.IO;
using GruntyOS.String;
using System;
using System.Collections.Generic;
namespace GruntyOS.HAL
{
	public class GLNFS : StorageDevice
	{
		private Partition a = null;
		public int ID = 0;
		private string b = null;
		private int c = 0;
		public int prevLoc = 0;
		private int d;
		public override void makeDir(string name, string owner)
		{
			name = this.a(name);
			if (!this.c(name, FileSystem.Root.Seperator))
			{
				if (CurrentUser.Privilages != 0 && this.DriveLabel == "GruntyOS")
				{
					throw new Exception("Can not make files here");
				}
				this.a(name, 2, owner);
			}
			else
			{
				if (!this.CanWrite(name.Substring(0, Util.LastIndexOf(name, FileSystem.Root.Seperator))))
				{
					throw new Exception("Access denied!");
				}
				this.a(name, 2, owner);
			}
		}
		public void Format(string VolumeLABEL)
		{
			byte[] array = new byte[512];
			MemoryStream memoryStream = new MemoryStream(512);
			for (int i = 0; i < 512; i++)
			{
				array[i] = 0;
			}
			for (int j = 0; j < 100; j++)
			{
				this.a.WriteBlock((ulong)((long)j), 1u, array);
			}
			memoryStream.Data = array;
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write("GFS SC");
			binaryWriter.Write(VolumeLABEL);
			binaryWriter.Write(4);
			binaryWriter.BaseStream.Close();
			this.a.WriteBlock(1uL, 1u, binaryWriter.BaseStream.Data);
		}
		public GLNFS(Partition p)
		{
			this.a = p;
			byte[] array = new byte[512];
			this.a.ReadBlock(1uL, 1u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(512));
			binaryReader.BaseStream.Data = array;
			if (!(binaryReader.ReadString() != "GFS SC"))
			{
				this.DriveLabel = binaryReader.ReadString();
			}
		}
		public override void Delete(string Path)
		{
			this.b(Path);
		}
		private bool c(string A_0, char A_1)
		{
			bool result;
			for (int i = 0; i < A_0.Length; i++)
			{
				char c = A_0[i];
				if (c == A_1)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public static bool isGFS(Partition part)
		{
			byte[] array = new byte[512];
			part.ReadBlock(1uL, 1u, array);
			return new BinaryReader(new MemoryStream(512))
			{
				BaseStream = 
				{
					Data = array
				}
			}.ReadString() == "GFS SC";
		}
		public byte[] GetFile(int index)
		{
			return null;
		}
		private byte[] a(int A_0, int A_1)
		{
			byte[] result;
			if (A_1 < 512)
			{
				byte[] array = new byte[512];
				this.a.ReadBlock((ulong)((long)A_0), 1u, array);
				byte[] array2 = new byte[A_1];
				for (int i = 4; i < A_1; i++)
				{
					array2[i] = array[i];
				}
				result = array2;
			}
			else
			{
				int j = 0;
				byte[] array2 = new byte[A_1];
				int num = 4;
				while (j < A_1)
				{
					byte[] array = new byte[512];
					this.a.ReadBlock((ulong)((long)A_0), 1u, array);
					for (int i = num; i < 512; i++)
					{
						array2[j] = array[i];
						if (j == A_1)
						{
							result = array2;
							return result;
						}
						j++;
					}
					num = 0;
					A_0++;
				}
				result = null;
			}
			return result;
		}
		private int b(string A_0, char A_1)
		{
			int num = 0;
			int result;
			for (int i = 0; i < A_0.Length; i++)
			{
				char c = A_0[i];
				if (c == A_1)
				{
					result = num;
					return result;
				}
				num++;
			}
			result = -1;
			return result;
		}
		private bool a(byte A_0, int A_1)
		{
			int num = (int)A_0 & 1 << A_1 - 1;
			return num != 0;
		}
		public bool CanWrite(string file)
		{
			bool result;
			if (CurrentUser.Privilages == 0)
			{
				result = true;
			}
			else
			{
				fsEntry fsEntry = this.readFromNode(file);
				result = ((this.a(fsEntry.User, 2) && CurrentUser.Username == fsEntry.Owner) || this.a(fsEntry.Group, 2) || this.a(fsEntry.Global, 2));
			}
			return result;
		}
		public bool CanRead(string file)
		{
			bool result;
			if (CurrentUser.Privilages == 0)
			{
				result = true;
			}
			else
			{
				fsEntry fsEntry = this.readFromNode(file);
				result = ((this.a(fsEntry.User, 3) && CurrentUser.Username == fsEntry.Owner) || this.a(fsEntry.Group, 3) || this.a(fsEntry.Global, 3));
			}
			return result;
		}
		public bool CanExecute(string file)
		{
			fsEntry fsEntry = this.readFromNode(file);
			return (this.a(fsEntry.User, 1) && CurrentUser.Username == fsEntry.Owner) || this.a(fsEntry.Group, 1) || this.a(fsEntry.Global, 1);
		}
		private void a(fsEntry A_0, int A_1)
		{
			byte[] array = new byte[1024];
			this.a.ReadBlock((ulong)((long)A_1), 2u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024));
			binaryReader.BaseStream.Data = array;
			int num = binaryReader.ReadInt32() + 1;
			for (int i = 0; i < num - 1; i++)
			{
				string text = binaryReader.ReadString();
				int num2 = binaryReader.ReadInt32();
				int num3 = binaryReader.ReadInt32();
				byte b = binaryReader.BaseStream.Read();
				byte b2 = binaryReader.BaseStream.Read();
				byte b3 = binaryReader.BaseStream.Read();
				byte b4 = binaryReader.BaseStream.Read();
				string text2 = binaryReader.ReadString();
				string text3 = binaryReader.ReadString();
				int num4 = binaryReader.ReadInt32();
			}
			int position = binaryReader.BaseStream.Position;
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream(1024));
			binaryWriter.BaseStream.Data = array;
			binaryWriter.Write(num);
			binaryWriter.BaseStream.Position = position;
			binaryWriter.Write(A_0.Name);
			binaryWriter.Write(A_0.Pointer);
			binaryWriter.Write(A_0.Length);
			binaryWriter.BaseStream.Write(A_0.Attributes);
			binaryWriter.Write(A_0.User);
			binaryWriter.Write(A_0.Group);
			binaryWriter.Write(A_0.Global);
			binaryWriter.Write(global::b.b());
			binaryWriter.Write(A_0.Owner);
			binaryWriter.Write(A_0.Checksum);
			binaryWriter.BaseStream.Close();
			this.a.WriteBlock((ulong)((long)A_1), 2u, binaryWriter.BaseStream.Data);
		}
		public int getWriteAddress()
		{
			byte[] array = new byte[512];
			this.a.ReadBlock(1uL, 1u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(512));
			binaryReader.BaseStream.Data = array;
			binaryReader.ReadString();
			binaryReader.ReadString();
			return binaryReader.ReadInt32();
		}
		private void a(int A_0)
		{
			byte[] array = new byte[512];
			this.a.ReadBlock(1uL, 1u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(512));
			binaryReader.BaseStream.Data = array;
			binaryReader.ReadString();
			binaryReader.ReadString();
			BinaryWriter binaryWriter = new BinaryWriter(binaryReader.BaseStream);
			binaryWriter.BaseStream.Position = binaryReader.BaseStream.Position;
			binaryWriter.Write(this.getWriteAddress() + A_0);
			binaryWriter.BaseStream.Close();
			this.a.WriteBlock(1uL, 1u, binaryWriter.BaseStream.Data);
		}
		public void setWriteAddress(int amount)
		{
			byte[] array = new byte[512];
			this.a.ReadBlock(1uL, 1u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(512));
			binaryReader.BaseStream.Data = array;
			binaryReader.ReadString();
			binaryReader.ReadString();
			BinaryWriter binaryWriter = new BinaryWriter(binaryReader.BaseStream);
			binaryWriter.BaseStream.Position = binaryReader.BaseStream.Position;
			binaryWriter.Write(amount);
			binaryWriter.BaseStream.Close();
			this.a.WriteBlock(1uL, 1u, binaryWriter.BaseStream.Data);
		}
		private void a(string A_0, int A_1, string A_2)
		{
			A_0 = this.a(A_0);
			byte[] array = new byte[1024];
			this.a.ReadBlock((ulong)((long)A_1), 2u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024));
			binaryReader.BaseStream.Data = array;
			this.prevLoc = A_1;
			int num = binaryReader.ReadInt32();
			if (this.c(A_0, FileSystem.Root.Seperator))
			{
				string text = A_0.Substring(0, this.b(A_0, FileSystem.Root.Seperator));
				for (int i = 0; i < num; i++)
				{
					string text2 = binaryReader.ReadString();
					if (text2 == A_0 && !this.c(A_0, FileSystem.Root.Seperator))
					{
						break;
					}
					int num2 = binaryReader.ReadInt32();
					int num3 = binaryReader.ReadInt32();
					if (text == text2)
					{
						A_1 = num2;
						this.prevLoc = num2;
						this.a(A_0.Substring(this.b(A_0, FileSystem.Root.Seperator) + 1), A_1, A_2);
						break;
					}
					byte b = binaryReader.BaseStream.Read();
					byte b2 = binaryReader.BaseStream.Read();
					byte b3 = binaryReader.BaseStream.Read();
					byte b4 = binaryReader.BaseStream.Read();
					string text3 = binaryReader.ReadString();
					string text4 = binaryReader.ReadString();
					int num4 = binaryReader.ReadInt32();
				}
			}
			else
			{
				fsEntry fsEntry = new fsEntry();
				fsEntry.Name = A_0;
				fsEntry.Attributes = 2;
				fsEntry.Length = 2;
				fsEntry.Pointer = this.getWriteAddress();
				byte[] array2 = new byte[1024];
				for (int i = 0; i < 1024; i++)
				{
					array2[i] = 0;
				}
				this.a.WriteBlock((ulong)((long)this.getWriteAddress()), 2u, array2);
				fsEntry.Owner = A_2;
				this.a(2);
				this.a(fsEntry, this.prevLoc);
			}
		}
		private int a(string A_0, char A_1)
		{
			int result = -1;
			int num = 0;
			for (int i = 0; i < A_0.Length; i++)
			{
				char c = A_0[i];
				if (c == A_1)
				{
					result = num;
				}
				num++;
			}
			return result;
		}
		private void b(string A_0)
		{
			if (!this.CanWrite(A_0))
			{
				throw new Exception("Access denied!");
			}
			string text = A_0.Substring(Util.LastIndexOf(A_0, FileSystem.Root.Seperator) + 1);
			A_0 = A_0.Substring(0, Util.LastIndexOf(A_0, FileSystem.Root.Seperator));
			this.a(A_0);
			A_0 = FileSystem.Root.Seperator.ToString() + A_0;
			int num = 2;
			if (Util.Contains(A_0, FileSystem.Root.Seperator))
			{
				num = this.getNodeAddress(A_0, 2);
			}
			byte[] array = new byte[1024];
			this.a.ReadBlock((ulong)num, 2u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024));
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream(1024));
			binaryReader.BaseStream.Data = array;
			int num2 = binaryReader.ReadInt32();
			bool flag = false;
			binaryWriter.Write(num2 - 1);
			for (int i = 0; i < num2; i++)
			{
				string data = binaryReader.ReadString();
				int data2 = binaryReader.ReadInt32();
				int data3 = binaryReader.ReadInt32();
				byte data4 = binaryReader.BaseStream.Read();
				byte data5 = binaryReader.BaseStream.Read();
				byte data6 = binaryReader.BaseStream.Read();
				byte data7 = binaryReader.BaseStream.Read();
				string data8 = binaryReader.ReadString();
				string data9 = binaryReader.ReadString();
				int data10 = binaryReader.ReadInt32();
				if (data == text)
				{
					flag = true;
				}
				else
				{
					binaryWriter.Write(data);
					binaryWriter.Write(data2);
					binaryWriter.Write(data3);
					binaryWriter.Write(data4);
					binaryWriter.Write(data5);
					binaryWriter.Write(data6);
					binaryWriter.Write(data7);
					binaryWriter.Write(data8);
					binaryWriter.Write(data9);
					binaryWriter.Write(data10);
				}
			}
			if (flag)
			{
				binaryWriter.BaseStream.Close();
				this.a.WriteBlock((ulong)((long)num), 2u, binaryWriter.BaseStream.Data);
			}
		}
		public override void Chown(string item, string mod)
		{
			if (!this.CanWrite(item))
			{
				throw new Exception("Error: Access Denied!");
			}
			this.a(item);
			string text = item.Substring(Util.LastIndexOf(item, FileSystem.Root.Seperator) + 1);
			item = item.Substring(0, Util.LastIndexOf(item, FileSystem.Root.Seperator));
			int nodeAddress = this.getNodeAddress(item, 2);
			byte[] array = new byte[1024];
			this.a.ReadBlock((ulong)((long)nodeAddress), 2u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024));
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream(1024));
			binaryReader.BaseStream.Data = array;
			int num = binaryReader.ReadInt32();
			binaryWriter.Write(num);
			for (int i = 0; i < num; i++)
			{
				string data = binaryReader.ReadString();
				int data2 = binaryReader.ReadInt32();
				int data3 = binaryReader.ReadInt32();
				byte data4 = binaryReader.BaseStream.Read();
				byte data5 = binaryReader.BaseStream.Read();
				byte data6 = binaryReader.BaseStream.Read();
				byte data7 = binaryReader.BaseStream.Read();
				string data8 = binaryReader.ReadString();
				string data9 = binaryReader.ReadString();
				int data10 = binaryReader.ReadInt32();
				if (data == text)
				{
					binaryWriter.Write(data);
					binaryWriter.Write(data2);
					binaryWriter.Write(data3);
					binaryWriter.Write(data4);
					binaryWriter.Write(data5);
					binaryWriter.Write(data6);
					binaryWriter.Write(data7);
					binaryWriter.Write(data8);
					binaryWriter.Write(mod);
					binaryWriter.Write(data10);
				}
				else
				{
					binaryWriter.Write(data);
					binaryWriter.Write(data2);
					binaryWriter.Write(data3);
					binaryWriter.Write(data4);
					binaryWriter.Write(data5);
					binaryWriter.Write(data6);
					binaryWriter.Write(data7);
					binaryWriter.Write(data8);
					binaryWriter.Write(data9);
					binaryWriter.Write(data10);
				}
			}
			binaryWriter.BaseStream.Close();
			this.a.WriteBlock((ulong)((long)nodeAddress), 2u, binaryWriter.BaseStream.Data);
		}
		public override void Chmod(string item, string mod)
		{
			if (!this.CanWrite(item))
			{
				throw new Exception("Error: Access Denied!");
			}
			byte data = (byte)Conversions.StringToInt(mod.Substring(0, 1));
			byte data2 = (byte)Conversions.StringToInt(mod.Substring(1, 1));
			byte data3 = (byte)Conversions.StringToInt(mod.Substring(2, 1));
			this.a(item);
			string text = item.Substring(Util.LastIndexOf(item, FileSystem.Root.Seperator) + 1);
			item = item.Substring(0, Util.LastIndexOf(item, FileSystem.Root.Seperator));
			int nodeAddress = this.getNodeAddress(item, 2);
			byte[] array = new byte[1024];
			this.a.ReadBlock((ulong)((long)nodeAddress), 2u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024));
			BinaryWriter binaryWriter = new BinaryWriter(new MemoryStream(1024));
			binaryReader.BaseStream.Data = array;
			int num = binaryReader.ReadInt32();
			binaryWriter.Write(num);
			for (int i = 0; i < num; i++)
			{
				string data4 = binaryReader.ReadString();
				int data5 = binaryReader.ReadInt32();
				int data6 = binaryReader.ReadInt32();
				byte data7 = binaryReader.BaseStream.Read();
				byte data8 = binaryReader.BaseStream.Read();
				byte data9 = binaryReader.BaseStream.Read();
				byte data10 = binaryReader.BaseStream.Read();
				string data11 = binaryReader.ReadString();
				string data12 = binaryReader.ReadString();
				int data13 = binaryReader.ReadInt32();
				if (data4 == text)
				{
					binaryWriter.Write(data4);
					binaryWriter.Write(data5);
					binaryWriter.Write(data6);
					binaryWriter.Write(data7);
					binaryWriter.Write(data);
					binaryWriter.Write(data2);
					binaryWriter.Write(data3);
					binaryWriter.Write(data11);
					binaryWriter.Write(data12);
					binaryWriter.Write(data13);
				}
				else
				{
					binaryWriter.Write(data4);
					binaryWriter.Write(data5);
					binaryWriter.Write(data6);
					binaryWriter.Write(data7);
					binaryWriter.Write(data8);
					binaryWriter.Write(data9);
					binaryWriter.Write(data10);
					binaryWriter.Write(data11);
					binaryWriter.Write(data12);
					binaryWriter.Write(data13);
				}
			}
			binaryWriter.BaseStream.Close();
			this.a.WriteBlock((ulong)((long)nodeAddress), 2u, binaryWriter.BaseStream.Data);
		}
		public override fsEntry[] getLongList(string dir)
		{
			if (dir != "" && dir != FileSystem.Root.Seperator.ToString() && !this.CanRead(dir))
			{
				throw new Exception("Access Denied!");
			}
			if (dir == FileSystem.Root.Seperator.ToString())
			{
				dir = "";
			}
			string[] array = this.ListFiles(dir);
			dir = this.a(dir);
			fsEntry[] array2 = new fsEntry[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array2[i] = this.readFromNode(Util.cleanName(dir + FileSystem.Root.Seperator.ToString() + array[i]));
			}
			return array2;
		}
		public override void Move(string f, string dest)
		{
			fsEntry fsEntry = this.readFromNode(f);
			this.b(f);
			fsEntry.Name = dest.Substring(Util.LastIndexOf(dest, FileSystem.Root.Seperator) + 1);
			int nodeAddress;
			if (Util.Contains(dest, FileSystem.Root.Seperator))
			{
				nodeAddress = this.getNodeAddress(dest.Substring(0, this.a(dest, FileSystem.Root.Seperator)), 2);
			}
			else
			{
				nodeAddress = this.getNodeAddress(f.Substring(0, this.a(f, FileSystem.Root.Seperator)), 2);
			}
			this.a(fsEntry, nodeAddress);
		}
		public fsEntry readFromNode(string name)
		{
			name = this.a(name);
			int num;
			if (this.c(name, FileSystem.Root.Seperator))
			{
				num = this.getNodeAddress(name.Substring(0, this.a(name, FileSystem.Root.Seperator)), 2);
			}
			else
			{
				num = 2;
			}
			byte[] array = new byte[1024];
			this.a.ReadBlock((ulong)((long)num), 2u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024));
			binaryReader.BaseStream.Data = array;
			int num2 = binaryReader.ReadInt32();
			for (int i = 0; i < num2; i++)
			{
				string name2 = binaryReader.ReadString();
				int pointer = binaryReader.ReadInt32();
				int length = binaryReader.ReadInt32();
				byte attributes = binaryReader.BaseStream.Read();
				byte user = binaryReader.BaseStream.Read();
				byte group = binaryReader.BaseStream.Read();
				byte global = binaryReader.BaseStream.Read();
				string time = binaryReader.ReadString();
				string owner = binaryReader.ReadString();
				int checksum = binaryReader.ReadInt32();
				if (name2 == name.Substring(this.a(name, FileSystem.Root.Seperator) + 1) || name2 == name)
				{
					return new fsEntry
					{
						Name = name2,
						Attributes = attributes,
						Length = length,
						Pointer = pointer,
						User = user,
						Group = group,
						Global = global,
						Owner = owner,
						Time = time,
						Checksum = checksum
					};
				}
			}
			throw new Exception("File not found!!!");
		}
		public int getNodeAddress(string name, int Node_block = 2)
		{
			name = this.a(name);
			int result;
			if (!(name == "") && name != null)
			{
				if (Node_block == 0)
				{
					Node_block = 2;
				}
				string text = "";
				byte[] array = new byte[1024];
				this.a.ReadBlock((ulong)((long)Node_block), 2u, array);
				BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024));
				binaryReader.BaseStream.Data = array;
				binaryReader.BaseStream.Position = 0;
				int num = binaryReader.ReadInt32();
				if (this.c(name, FileSystem.Root.Seperator))
				{
					text = name.Substring(0, this.b(name, FileSystem.Root.Seperator));
				}
				for (int i = 0; i < num; i++)
				{
					string text2 = binaryReader.ReadString();
					int num2 = binaryReader.ReadInt32();
					int num3 = binaryReader.ReadInt32();
					if (name.Substring(this.b(name, FileSystem.Root.Seperator) + 1) == text2 && !this.c(name, FileSystem.Root.Seperator))
					{
						result = num2;
						return result;
					}
					if (text == text2 && this.c(name, FileSystem.Root.Seperator))
					{
						Node_block = num2;
						this.prevLoc = num2;
						name = name.Substring(this.b(name, FileSystem.Root.Seperator) + 1);
						result = this.getNodeAddress(name, num2);
						return result;
					}
					byte b = binaryReader.BaseStream.Read();
					byte b2 = binaryReader.BaseStream.Read();
					byte b3 = binaryReader.BaseStream.Read();
					byte b4 = binaryReader.BaseStream.Read();
					string text3 = binaryReader.ReadString();
					string text4 = binaryReader.ReadString();
					int num4 = binaryReader.ReadInt32();
				}
				throw new Exception("File not found :(");
			}
			result = 2;
			return result;
		}
		public override byte[] readFile(string name)
		{
			if (!this.CanRead(name))
			{
				throw new Exception("Access Denied!");
			}
			name = this.a(name);
			fsEntry fsEntry = this.readFromNode(name);
			byte[] array = new byte[fsEntry.Length * 512];
			this.a.ReadBlock((ulong)((long)fsEntry.Pointer), (uint)fsEntry.Length, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(array.Length)
			{
				Data = array
			});
			int num = binaryReader.ReadInt32();
			byte[] array2 = new byte[num];
			for (int i = 0; i < num; i++)
			{
				array2[i] = binaryReader.BaseStream.Read();
			}
			int cRC = Hash.getCRC(array2);
			byte[] result;
			if (cRC != fsEntry.Checksum)
			{
				result = this.readFile(name);
			}
			else
			{
				result = array2;
			}
			return result;
		}
		public override string[] ListJustFiles(string dir)
		{
			if (dir != "" && dir != FileSystem.Root.Seperator.ToString() && !this.CanRead(dir))
			{
				throw new Exception("Access Denied!");
			}
			dir = this.a(dir);
			List<string> list = new List<string>();
			byte[] array = new byte[1024];
			this.a.ReadBlock((ulong)((long)this.getNodeAddress(dir, 2)), 2u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024)
			{
				Data = array
			});
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string item = binaryReader.ReadString();
				int num2 = binaryReader.ReadInt32();
				int num3 = binaryReader.ReadInt32();
				byte b = binaryReader.BaseStream.Read();
				byte b2 = binaryReader.BaseStream.Read();
				byte b3 = binaryReader.BaseStream.Read();
				byte b4 = binaryReader.BaseStream.Read();
				string text = binaryReader.ReadString();
				string text2 = binaryReader.ReadString();
				int num4 = binaryReader.ReadInt32();
				if (b != 2)
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}
		public override string[] ListDirectories(string dir)
		{
			if (dir != "" && dir != FileSystem.Root.Seperator.ToString() && !this.CanRead(dir))
			{
				throw new Exception("Access Denied!");
			}
			dir = this.a(dir);
			List<string> list = new List<string>();
			byte[] array = new byte[1024];
			this.a.ReadBlock((ulong)((long)this.getNodeAddress(dir, 2)), 2u, array);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024)
			{
				Data = array
			});
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string item = binaryReader.ReadString();
				int num2 = binaryReader.ReadInt32();
				int num3 = binaryReader.ReadInt32();
				byte b = binaryReader.BaseStream.Read();
				byte b2 = binaryReader.BaseStream.Read();
				byte b3 = binaryReader.BaseStream.Read();
				byte b4 = binaryReader.BaseStream.Read();
				string text = binaryReader.ReadString();
				string text2 = binaryReader.ReadString();
				int num4 = binaryReader.ReadInt32();
				if (b == 2)
				{
					list.Add(item);
				}
			}
			return list.ToArray();
		}
		public override string[] ListFiles(string dir)
		{
			if (dir != "" && dir != FileSystem.Root.Seperator.ToString() && !this.CanRead(dir))
			{
				throw new Exception("Access Denied!");
			}
			List<string> list = new List<string>();
			byte[] array = new byte[1024];
			this.a.ReadBlock((ulong)this.getNodeAddress(dir, 2), 2u, array);
			dir = this.a(dir);
			BinaryReader binaryReader = new BinaryReader(new MemoryStream(1024)
			{
				Data = array
			});
			int num = binaryReader.ReadInt32();
			for (int i = 0; i < num; i++)
			{
				string item = binaryReader.ReadString();
				int num2 = binaryReader.ReadInt32();
				int num3 = binaryReader.ReadInt32();
				byte b = binaryReader.BaseStream.Read();
				byte b2 = binaryReader.BaseStream.Read();
				byte b3 = binaryReader.BaseStream.Read();
				byte b4 = binaryReader.BaseStream.Read();
				string text = binaryReader.ReadString();
				string text2 = binaryReader.ReadString();
				int num4 = binaryReader.ReadInt32();
				list.Add(item);
			}
			return list.ToArray();
		}
		private string a(string A_0)
		{
			if (A_0.Substring(0, 1) == FileSystem.Root.Seperator.ToString())
			{
				A_0 = A_0.Substring(1, A_0.Length - 1);
			}
			if (A_0.Substring(A_0.Length - 1, 1) == FileSystem.Root.Seperator.ToString())
			{
				A_0 = A_0.Substring(0, A_0.Length - 1);
			}
			return A_0;
		}
		public override void saveFile(byte[] data, string name, string owner)
		{
			name = this.a(name);
			if (Util.Contains(name, FileSystem.Root.Seperator))
			{
				if (!this.CanWrite(name.Substring(0, Util.LastIndexOf(name, FileSystem.Root.Seperator))))
				{
					throw new Exception("Access denied");
				}
			}
			fsEntry fsEntry = new fsEntry();
			fsEntry.Checksum = Hash.getCRC(data);
			fsEntry.Attributes = 1;
			byte[] array = new byte[data.Length + 4];
			MemoryStream memoryStream = new MemoryStream(array.Length);
			BinaryWriter binaryWriter = new BinaryWriter(memoryStream);
			binaryWriter.Write(data.Length);
			int i;
			for (i = 0; i < data.Length; i++)
			{
				binaryWriter.BaseStream.Write(data[i]);
			}
			binaryWriter.BaseStream.Close();
			data = binaryWriter.BaseStream.Data;
			fsEntry.Name = name;
			int j;
			for (j = 0; j < data.Length; j++)
			{
				byte[] array2 = new byte[512];
				for (i = 0; i < 512; i++)
				{
				}
			}
			fsEntry.Length = j;
			fsEntry.Pointer = this.getWriteAddress();
			fsEntry.Attributes = 1;
			fsEntry.Owner = owner;
			int a_ = 2;
			if (Util.Contains(fsEntry.Name, FileSystem.Root.Seperator))
			{
				a_ = this.getNodeAddress(fsEntry.Name.Substring(0, this.a(fsEntry.Name, FileSystem.Root.Seperator)), 2);
			}
			if (this.c(name, FileSystem.Root.Seperator))
			{
				fsEntry.Name = name.Substring(this.a(name, FileSystem.Root.Seperator) + 1);
			}
			string[] array3 = this.ListFiles(name.Substring(0, this.a(name, FileSystem.Root.Seperator)));
			i = 0;
			while (i < array3.Length)
			{
				if (array3[i] == name.Substring(Util.LastIndexOf(name, FileSystem.Root.Seperator) + 1))
				{
					if (fsEntry.Length * 512 <= j)
					{
						fsEntry = this.readFromNode(name);
						this.b(name);
						if (data.Length < 512)
						{
							memoryStream = new MemoryStream(512);
							for (int k = 0; k < data.Length; k++)
							{
								memoryStream.Write(data[k]);
							}
							this.a.WriteBlock((ulong)((long)fsEntry.Pointer), 1u, memoryStream.Data);
							this.a(fsEntry, a_);
						}
						else
						{
							int l = 0;
							int num = fsEntry.Pointer;
							while (l < data.Length)
							{
								byte[] array2 = new byte[512];
								for (int k = 0; k < 512; k++)
								{
									array2[k] = data[l];
									l++;
								}
								this.a.WriteBlock((ulong)((long)num), 1u, array2);
								num++;
							}
							this.a(fsEntry, a_);
						}
						return;
					}
					fsEntry.Owner = this.readFromNode(name).Owner;
					fsEntry.Group = this.readFromNode(name).Group;
					fsEntry.Global = this.readFromNode(name).Global;
					fsEntry.User = this.readFromNode(name).User;
					this.b(name);
					break;
				}
				else
				{
					i++;
				}
			}
			if (data.Length < 512)
			{
				memoryStream = new MemoryStream(512);
				for (i = 0; i < data.Length; i++)
				{
					memoryStream.Write(data[i]);
				}
				this.a.WriteBlock((ulong)((long)fsEntry.Pointer), 1u, memoryStream.Data);
				this.a(fsEntry, a_);
				this.a(j);
				return;
			}
			int m = 0;
			int num2 = this.getWriteAddress();
			this.a(j);
			while (m < data.Length)
			{
				byte[] array2 = new byte[512];
				for (i = 0; i < 512; i++)
				{
					array2[i] = data[m];
					m++;
				}
				this.a.WriteBlock((ulong)((long)num2), 1u, array2);
				num2++;
			}
			this.a(fsEntry, a_);
		}
	}
}

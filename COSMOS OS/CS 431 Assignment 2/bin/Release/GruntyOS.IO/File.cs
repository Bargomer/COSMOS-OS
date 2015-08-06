using GruntyOS.HAL;
using GruntyOS.String;
using System;
namespace GruntyOS.IO
{
	public class File
	{
		public static string Open(string fname)
		{
			string text = "";
			byte[] array = FileSystem.Root.readFile(fname);
			for (int i = 0; i < array.Length; i++)
			{
				byte b = array[i];
				string arg_2A_0 = text;
				char c = (char)b;
				text = arg_2A_0 + c.ToString();
			}
			return text;
		}
		private static bool a(byte A_0, int A_1)
		{
			int num = (int)A_0 & 1 << A_1 - 1;
			return num != 0;
		}
		public static bool CanExecute(fsEntry ent)
		{
			return (File.a(ent.User, 1) && CurrentUser.Username == ent.Owner) || File.a(ent.Group, 1) || File.a(ent.Global, 1);
		}
		public static bool isExecutable(string file)
		{
			fsEntry[] longList = FileSystem.Root.getLongList(file.Substring(0, Util.LastIndexOf(file, '/')));
			bool result;
			for (int i = 0; i < longList.Length; i++)
			{
				if (longList[i].Name == file.Substring(Util.LastIndexOf(file, FileSystem.Root.Seperator) + 1))
				{
					if (File.CanExecute(longList[i]))
					{
						result = true;
						return result;
					}
				}
			}
			result = false;
			return result;
		}
		public static void Delete(string File)
		{
			FileSystem.Root.Delete(File);
		}
		public static void Chmod(string name, string chmod)
		{
			FileSystem.Root.Chmod(name, chmod);
		}
		public static void Save(string name, string text)
		{
			BinaryWriter binaryWriter = new BinaryWriter(new FileStream(name, "w"));
			for (int i = 0; i < text.Length; i++)
			{
				byte i2 = (byte)text[i];
				binaryWriter.BaseStream.Write(i2);
			}
			binaryWriter.BaseStream.Close();
		}
	}
}

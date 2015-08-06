using GruntyOS.HAL;
using System;
namespace GruntyOS.String
{
	public class Util
	{
		public static string RemoveNonprintableChars(string str)
		{
			string text = "";
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if ((byte)c >= 32 && (byte)c <= 126)
				{
					text += c.ToString();
				}
			}
			return text;
		}
		public static string Remove(string str, char with)
		{
			string text = "";
			for (int i = 0; i < str.Length; i++)
			{
				char c = str[i];
				if (c.ToString() != with.ToString())
				{
					text += c.ToString();
				}
			}
			return text;
		}
		public static bool isWhiteSpace(char c)
		{
			return c == ' ' || c == '\r' || c == '\n';
		}
		public static bool isLetter(char c)
		{
			byte b = (byte)c;
			return (b >= 65 && b <= 90) || (b >= 97 && b <= 122);
		}
		public static bool isLetterOrDigit(char c)
		{
			bool result;
			if (Util.isLetter(c))
			{
				result = true;
			}
			else
			{
				byte b = (byte)c;
				result = (b >= 48 && b <= 58);
			}
			return result;
		}
		public static bool isDigit(char c)
		{
			byte b = (byte)c;
			return b >= 48 && b <= 58;
		}
		public static bool Contains(string Str, char c)
		{
			bool result;
			for (int i = 0; i < Str.Length; i++)
			{
				char c2 = Str[i];
				if (c2 == c)
				{
					result = true;
					return result;
				}
			}
			result = false;
			return result;
		}
		public static int IndexOf(string str, char c)
		{
			int num = 0;
			int result;
			for (int i = 0; i < str.Length; i++)
			{
				char c2 = str[i];
				if (c2 == c)
				{
					result = num;
					return result;
				}
				num++;
			}
			result = -1;
			return result;
		}
		public static string cleanName(string name)
		{
			if (name.Substring(0, 1) == FileSystem.Root.Seperator.ToString())
			{
				name = name.Substring(1, name.Length - 1);
			}
			if (name.Substring(name.Length - 1, 1) == FileSystem.Root.Seperator.ToString())
			{
				name = name.Substring(0, name.Length - 1);
			}
			return name;
		}
		public static int LastIndexOf(string This, char ch)
		{
			int result = -1;
			int num = 0;
			for (int i = 0; i < This.Length; i++)
			{
				char c = This[i];
				if (c == ch)
				{
					result = num;
				}
				num++;
			}
			return result;
		}
	}
}

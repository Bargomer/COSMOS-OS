using System;
namespace GruntyOS.Crypto
{
	public class Hash
	{
		public static int GHash(string s)
		{
			int num = 23;
			int num2 = 0;
			char[] array = s.ToCharArray();
			for (int i = 0; i < array.Length; i++)
			{
				char c = array[i];
				num = (num ^ num2 ^ (int)c);
			}
			return num ^ s.Length;
		}
		public static string PasswordHASH(string str)
		{
			int num = 4;
			int[] array = new int[num];
			int num2 = 0;
			char[] array2 = str.ToCharArray();
			int num3 = Hash.GHash(str);
			int num4 = 1;
			int num5 = 2;
			for (int i = 0; i < str.Length; i++)
			{
				byte b = (byte)str[i];
				num5 = ((int)b ^ num5);
				array2[num2] = (char)b;
				num2++;
			}
			num3 ^= num5;
			num2 = 0;
			for (int j = 0; j < num; j++)
			{
				for (int k = 0; k < array2.Length; k++)
				{
					if (num2 > num - 1)
					{
						num2 = 0;
					}
					int num6 = (int)((byte)array2[k]);
					num6 ^= num5;
					array[num2] = (str.Length ^ k);
					if (num6 != 0)
					{
						array[num2] ^= num3;
						array[num2] <<= 1;
						array[num2] ^= num4;
						array[num2] <<= 4;
						array[num2] ^= num5;
						array[num2] <<= 2;
						array[num2] += num6;
					}
					num2++;
					num4++;
				}
			}
			num2 = 0;
			num4 = 0;
			for (int k = 0; k < array2.Length; k++)
			{
				if (num2 > num - 1)
				{
					num2 = 0;
				}
				int num6 = (int)((byte)((int)array2[k] ^ num3 ^ num5));
				byte[] bytes = BitConverter.GetBytes(num6);
				bytes[0] = (byte)((int)bytes[0] ^ (num6 & num4));
				bytes[1] = (bytes[1] & (byte)num4);
				bytes[2] = (byte)((int)bytes[2] | ((int)bytes[1] ^ num6));
				bytes[3] = (bytes[3] ^ (bytes[1] ^ bytes[2]));
				array[num2] = (num3 ^ BitConverter.ToInt32(bytes, 0));
				array[num2] ^= (int)(bytes[0] * bytes[1] ^ bytes[2]);
				for (int j = 0; j < bytes.Length; j++)
				{
					if (bytes[j] == 0)
					{
						bytes[j] = (byte)(num6 ^ num4);
					}
				}
				num2++;
				num4++;
			}
			string text = "";
			for (int k = 0; k < num; k++)
			{
				int value = array[k];
				byte[] bytes2 = BitConverter.GetBytes(value);
				for (int i = 0; i < bytes2.Length; i++)
				{
					byte dat = bytes2[i];
					text += Conversions.ByteToHex((int)dat);
				}
			}
			return text;
		}
		public static int GHash(byte[] s)
		{
			int num = 23;
			int num2 = 0;
			for (int i = 0; i < s.Length; i++)
			{
				byte b = s[i];
				num2++;
				num += (num2 | (int)b);
			}
			return num * s.Length;
		}
		public static int getCRC(byte[] s)
		{
			return Hash.GHash(s);
		}
	}
}

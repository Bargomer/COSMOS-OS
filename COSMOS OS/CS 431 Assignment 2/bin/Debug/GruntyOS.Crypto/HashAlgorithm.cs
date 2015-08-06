using System;
namespace GruntyOS.Crypto
{
	public abstract class HashAlgorithm
	{
		public byte[] HashValue;
		public string getString(byte[] data)
		{
			string text = "";
			byte[] array = this.Calculate(data);
			byte[] array2 = array;
			for (int i = 0; i < array2.Length; i++)
			{
				byte dat = array2[i];
				text += Conversions.ByteToHex((int)dat);
			}
			return text;
		}
		public byte[] Calculate(string str)
		{
			byte[] array = new byte[str.Length];
			int num = 0;
			for (int i = 0; i < str.Length; i++)
			{
				byte b = (byte)str[i];
				array[num] = b;
				num++;
			}
			return array;
		}
		public virtual byte[] Calculate(byte[] data)
		{
			return null;
		}
		public virtual void Calculate(byte[] data, ref uint val)
		{
		}
	}
}

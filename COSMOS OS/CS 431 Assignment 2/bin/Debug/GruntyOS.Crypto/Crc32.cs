using System;
namespace GruntyOS.Crypto
{
	public class Crc32 : HashAlgorithm
	{
		public const uint DefaultPolynomial = 3988292384u;
		public const uint DefaultSeed = 4294967295u;
		private uint a;
		private uint b;
		private uint[] c;
		private static uint[] d;
		public Crc32()
		{
			this.c = Crc32.b(3988292384u);
			this.b = 4294967295u;
			this.a = this.b;
		}
		public Crc32(uint polynomial, uint seed)
		{
			this.c = Crc32.b(polynomial);
			this.b = seed;
			this.a = seed;
		}
		public override void Calculate(byte[] data, ref uint val)
		{
			val = Crc32.Compute(data);
		}
		private void a(byte[] A_0, int A_1, int A_2)
		{
			this.a = Crc32.a(this.c, this.a, A_0, A_1, A_2);
		}
		private byte[] a()
		{
			byte[] array = this.a(~this.a);
			this.HashValue = array;
			return array;
		}
		public static uint Compute(byte[] buffer)
		{
			return ~Crc32.a(Crc32.b(3988292384u), 4294967295u, buffer, 0, buffer.Length);
		}
		public static uint Compute(uint seed, byte[] buffer)
		{
			return ~Crc32.a(Crc32.b(3988292384u), seed, buffer, 0, buffer.Length);
		}
		public static uint Compute(uint polynomial, uint seed, byte[] buffer)
		{
			return ~Crc32.a(Crc32.b(polynomial), seed, buffer, 0, buffer.Length);
		}
		private static uint[] b(uint A_0)
		{
			uint[] result;
			if (A_0 == 3988292384u && Crc32.d != null)
			{
				result = Crc32.d;
			}
			else
			{
				uint[] array = new uint[256];
				for (int i = 0; i < 256; i++)
				{
					uint num = (uint)i;
					for (int j = 0; j < 8; j++)
					{
						if ((num & 1u) == 1u)
						{
							num = (num >> 1 ^ A_0);
						}
						else
						{
							num >>= 1;
						}
					}
					array[i] = num;
				}
				if (A_0 == 3988292384u)
				{
					Crc32.d = array;
				}
				result = array;
			}
			return result;
		}
		private static uint a(uint[] A_0, uint A_1, byte[] A_2, int A_3, int A_4)
		{
			uint num = A_1;
			for (int i = A_3; i < A_4; i++)
			{
				num = (num >> 8 ^ A_0[(int)((UIntPtr)((uint)A_2[i] ^ (num & 255u)))]);
			}
			return num;
		}
		private byte[] a(uint A_0)
		{
			return new byte[]
			{
				(byte)(A_0 >> 24 & 255u),
				(byte)(A_0 >> 16 & 255u),
				(byte)(A_0 >> 8 & 255u),
				(byte)(A_0 & 255u)
			};
		}
	}
}

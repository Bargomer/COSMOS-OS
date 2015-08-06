using System;
namespace GruntyOS.IO
{
	public class TextReader
	{
		public int pos = 0;
		private char[] a;
		public int Length;
		public char Read()
		{
			this.pos++;
			return this.a[this.pos - 1];
		}
		public char Peek()
		{
			char result;
			if (this.pos < this.a.Length)
			{
				result = this.a[this.pos];
			}
			else
			{
				result = 'ÿ';
			}
			return result;
		}
		public char Peek(int depth)
		{
			char result;
			if (this.pos < this.a.Length)
			{
				result = this.a[this.pos + depth];
			}
			else
			{
				result = 'ÿ';
			}
			return result;
		}
		public TextReader(string str)
		{
			this.a = str.ToCharArray();
			this.Length = this.a.Length;
		}
	}
}

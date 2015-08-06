using Cosmos.Hardware;
using GruntyOS.IO;
using System;
internal class a : ioStream
{
	private Keyboard a;
	private bool b = false;
	private char c;
	private void a(byte A_0, bool A_1)
	{
		if (this.b)
		{
			if (!A_1)
			{
				this.a.GetChar(ref this.c);
				this.b = false;
			}
		}
	}
	public a()
	{
		HandleKeyboardDelegate handleKeyboardDelegate = new HandleKeyboardDelegate(this.a);
		this.a.Initialize(handleKeyboardDelegate);
	}
	public override byte Read()
	{
		return 0;
	}
}

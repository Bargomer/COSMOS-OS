using GruntyOS.IO;
using System;
using System.Collections.Generic;
namespace GruntyOS.Lang
{
	public class Scanner
	{
		private class a
		{
			public string a;
		}
		private object a = new object();
		private bool d(char A_0)
		{
			return A_0 == ' ' || A_0 == '\r' || A_0 == '\n';
		}
		private bool c(char A_0)
		{
			byte b = (byte)A_0;
			return (b >= 65 && b <= 90) || (b >= 97 && b <= 122);
		}
		private bool b(char A_0)
		{
			bool result;
			if (this.c(A_0))
			{
				result = true;
			}
			else
			{
				byte b = (byte)A_0;
				result = (b >= 48 && b <= 58);
			}
			return result;
		}
		private bool a(char A_0)
		{
			byte b = (byte)A_0;
			return b >= 48 && b <= 58;
		}
		public List<object> getTokens(string s)
		{
			List<object> list = new List<object>();
			TextReader textReader = new TextReader(s);
			while (textReader.Length > textReader.pos)
			{
				while (this.d(textReader.Peek()))
				{
					textReader.Read();
				}
				char c = textReader.Peek();
				if (c == '"')
				{
					textReader.Read();
					string text = "";
					while (textReader.Peek() != '"')
					{
						text += textReader.Read().ToString();
					}
					textReader.Read();
					list.Add(text);
				}
				else
				{
					if (c == '\'')
					{
						textReader.Read();
						string text = "";
						while (textReader.Peek() != '\'')
						{
							text += textReader.Read().ToString();
						}
						textReader.Read();
						list.Add(new Scanner.a
						{
							a = text
						});
					}
					else
					{
						if (this.a(c))
						{
							string text = "";
							while (this.a(textReader.Peek()))
							{
								text += textReader.Read().ToString();
							}
							list.Add(text);
						}
						else
						{
							if (c == '=')
							{
								textReader.Read();
								if (textReader.Peek() != '=')
								{
									list.Add("=");
								}
								else
								{
									list.Add("==");
									textReader.Read();
								}
							}
							else
							{
								if (c == '[')
								{
									textReader.Read();
									list.Add("[");
								}
								else
								{
									if (c == ']')
									{
										textReader.Read();
										list.Add("]");
									}
									else
									{
										if (c == '{')
										{
											textReader.Read();
											list.Add("{");
										}
										else
										{
											if (c == '}')
											{
												textReader.Read();
												list.Add("}");
											}
											else
											{
												if (this.c(c) || c == '/' || c == '-' || c == '.' || c == '$')
												{
													string text = "";
													while (this.b(textReader.Peek()) || textReader.Peek() == '_' || textReader.Peek() == '.' || textReader.Peek() == '/' || textReader.Peek() == '-' || textReader.Peek() == '$')
													{
														text += textReader.Read().ToString();
													}
													list.Add(text);
												}
											}
										}
									}
								}
							}
						}
					}
				}
			}
			return list;
		}
	}
}

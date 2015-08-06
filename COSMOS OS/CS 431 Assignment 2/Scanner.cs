using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CS_431_Assignment_2
{
    public class Scanner
    {
        public class StringLiteral
        {
            public string Value;
        }
        bool isWhiteSpace(char c)
        {
            if (c == ' ')
            {
                return true;
            }
            else if (c == '\r')
            {
                return true;
            }
            else if (c == '\n')
            {
                return true;
            }
            return false;
        }
        bool isLetter(char c)
        {
            byte code = (byte)c;
            if (code >= 65 && code <= 90)
            {
                return true;
            }
            if (code >= 97 && code <= 122)
            {
                return true;
            }
            return false;
        }
        bool isLetterOrDigit(char c)
        {
            if (isLetter(c))
            {
                return true;
            }
            byte code = (byte)c;
            if (code >= 48 && code <= 58)
            {
                return true;
            }
            return false;
        }
        bool isDigit(char c)
        {

            byte code = (byte)c;
            if (code >= 48 && code <= 58)
            {
                return true;
            }
            return false;
        }
        object Dot = new object();

        public List<object> getTokens(string s)
        {
            List<Object> fin = new List<object>();
            TextReader tr = new TextReader(s);
            while (tr.Length > tr.pos)
            {
                while (isWhiteSpace(tr.Peek()))
                {
                    tr.Read();
                }
                char ch = tr.Peek();

                if (ch == '"')
                {
                    tr.Read();
                    string accum = "";
                    while (tr.Peek() != '"')
                    {
                        accum += tr.Read().ToString();
                    }
                    tr.Read();

                    fin.Add(accum);
                }
                else if (ch == '\'')
                {
                    tr.Read();
                    string accum = "";
                    while (tr.Peek() != '\'')
                    {
                        accum += tr.Read().ToString();
                    }
                    tr.Read();

                    StringLiteral sl = new StringLiteral();
                    sl.Value = accum;
                    fin.Add(sl.Value);
                }

                else if (isDigit(ch))
                {
                    string accum = "";
                    while (isDigit(tr.Peek()))
                    {
                        accum += tr.Read().ToString();
                    }
                    fin.Add(accum);
                }
                else if (ch == '=')
                {
                    tr.Read();
                    if (tr.Peek() != '=')
                        fin.Add("=");
                    else
                    {
                        fin.Add("==");
                        tr.Read();
                    }

                }
                else if (ch == '[')
                {
                    tr.Read();
                    fin.Add("[");
                }
                else if (ch == ']')
                {
                    tr.Read();
                    fin.Add("]");
                }
                else if (ch == '{')
                {
                    tr.Read();
                    fin.Add("{");
                }
                else if (ch == '~')
                {
                    tr.Read();
                    fin.Add("~");
                }

                else if (ch == '>')
                {
                    tr.Read();
                    fin.Add(">");
                }
                else if (ch == '<')
                {
                    tr.Read();
                    fin.Add("<");
                }
                else if (ch == '}')
                {
                    tr.Read();
                    fin.Add("}");
                }
                else if (isLetter(ch) || ch == '/' || ch == '-' || ch == '.' || ch == '$' || ch == ':' || ch == '\\')
                {
                    string accum = "";
                    while (isLetterOrDigit(tr.Peek()) || tr.Peek() == '_' || tr.Peek() == '.' || tr.Peek() == '/' || tr.Peek() == '-' || tr.Peek() == '$' || tr.Peek() == ':' || tr.Peek() == '\\')
                    {
                        accum += tr.Read().ToString();
                    }
                    fin.Add(accum);
                }

            }
            return fin;
        }
    }
}

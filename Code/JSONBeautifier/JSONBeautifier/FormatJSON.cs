using System;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using Microsoft.SqlServer.Server;

using System.Text;
using System.Linq;


public partial class JSONBeautifier
{
    [Microsoft.SqlServer.Server.SqlFunction]
    public static SqlString IndentJSON(string JSON)
    {
        // Put your code here
        string FormatedJson = FormatJson(JSON);
        return new SqlString (FormatedJson);
    }

    private const string INDENT_STRING = "    ";
    public static string FormatJson(string str)
    {
        var indent = 0;
        var quoted = false;
        var sb = new StringBuilder();
        for (var i = 0; i < str.Length; i++)
        {
            var ch = str[i];
            switch (ch)
            {
                case '{':
                case '[':
                    sb.Append(ch);
                    if (!quoted)
                    {
                        sb.AppendLine();

                        foreach (int item in Enumerable.Range(0, ++indent))
                        {
                            sb.Append(INDENT_STRING);
                        }

                    }
                    break;
                case '}':
                case ']':
                    if (!quoted)
                    {
                        sb.AppendLine();

                        foreach (int item in Enumerable.Range(0, --indent))
                        {
                            sb.Append(INDENT_STRING);
                        }
                    }
                    sb.Append(ch);
                    break;
                case '"':
                    sb.Append(ch);
                    bool escaped = false;
                    var index = i;
                    while (index > 0 && str[--index] == '\\')
                        escaped = !escaped;
                    if (!escaped)
                        quoted = !quoted;
                    break;
                case ',':
                    sb.Append(ch);
                    if (!quoted)
                    {
                        sb.AppendLine();

                        foreach (int item in Enumerable.Range(0, indent))
                        {
                            sb.Append(INDENT_STRING);
                        }
                    }
                    break;
                case ':':
                    sb.Append(ch);
                    if (!quoted)
                        sb.Append(" ");
                    break;
                default:
                    sb.Append(ch);
                    break;
            }
        }
        return sb.ToString();
    }


}

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace Gico.Common
{
    public static class Extensions
    {
        // transform object into Identity data type (integer).

        public static int AsId(this object item, int defaultId = -1)
        {
            if (item == null)
                return defaultId;

            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultId;

            return result;
        }

        // transform object into integer data type.

        public static int AsInt(this object item, int defaultInt = default(int))
        {
            if (item == null)
                return defaultInt;

            int result;
            if (!int.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }
        public static int AsEnumToInt(this object item, int defaultInt = default(int))
        {
            if (item == null)
                return defaultInt;
            return (int)item;
        }
        public static long AsLong(this object item, long defaultInt = default(long))
        {
            if (item == null)
                return defaultInt;

            long result;
            if (!long.TryParse(item.ToString(), out result))
                return defaultInt;

            return result;
        }

        // transform object into double data type

        public static double AsDouble(this object item, double defaultDouble = default(double))
        {
            if (item == null)
                return defaultDouble;

            double result;
            if (!double.TryParse(item.ToString(), out result))
                return defaultDouble;

            return result;
        }
        public static decimal AsDecimal(this object item, decimal defaultDecimal = default(decimal))
        {
            if (item == null)
                return defaultDecimal;

            decimal result;
            if (!decimal.TryParse(item.ToString(), out result))
                return defaultDecimal;

            return result;
        }
        public static short AsShort(this object item, short defaultShort = default(short))
        {
            if (item == null)
                return defaultShort;

            short result;
            if (!short.TryParse(item.ToString(), out result))
                return defaultShort;

            return result;
        }
        public static byte AsByte(this object item, byte defaultByte = default(byte))
        {
            if (item == null)
                return defaultByte;

            byte result;
            if (!byte.TryParse(item.ToString(), out result))
                return defaultByte;

            return result;
        }
        // transform object into string data type

        public static string AsString(this object item, string defaultString = default(string))
        {
            if (item == null || item.Equals(DBNull.Value))
                return defaultString;

            return item.ToString().Trim();
        }
        public static string AsMoney(this decimal item, bool haveCurrency = true)
        {
            if (item > 0)
            {
                if (haveCurrency)
                {
                    return item.ToString("#,##.## VND", new CultureInfo("vi-VN"));
                }
                else
                {
                    return item.ToString("#,##.##", new CultureInfo("vi-VN"));
                }
            }
            if (haveCurrency)
            {
                return string.Format("0 VND");
            }
            else
            {
                return string.Format("0");
            }

        }

        public static string AsMoneyBackEnd(this decimal item, bool haveCurrency = true)
        {
            if (item > 0)
            {
                if (haveCurrency)
                {
                    return item.ToString("#,##.## VNĐ", new CultureInfo("en-US"));
                }
                else
                {
                    return item.ToString("#,##.##", new CultureInfo("en-US"));
                }
            }
            if (haveCurrency)
            {
                return string.Format("0 VND");
            }
            else
            {
                return string.Format("0");
            }

        }

        public static string AsDateView(this DateTime item)
        {
            return item.ToString("dd/MM/yyyy");
        }
        public static string AsDateTimeView(this DateTime item)
        {
            return item.ToString("dd/MM/yyyy HH:mm");
        }

        public static DateTime GetCurrentDateUtc()
        {
            return DateTime.UtcNow;
        }
        // transform object into DateTime data type.

        public static DateTime AsDateTime(this object item, DateTime defaultDateTime = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item.ToString()))
                return defaultDateTime;

            DateTime result;
            if (!DateTime.TryParse(string.Format("{0:yyyy-MM-dd HH:mm:ss.fff}", item), out result))
                return defaultDateTime;

            return result;
        }

        public static DateTime AsDateTime(this string item, string fomat, DateTime defaultDateTime = default(DateTime))
        {
            if (item == null || string.IsNullOrEmpty(item))
                return defaultDateTime;
            try
            {
                var result = DateTime.ParseExact(item, fomat, CultureInfo.InvariantCulture);
                return result;
            }
            catch (Exception)
            {
                return defaultDateTime;
            }

        }
        public static DateTime? AsDateTimeNullable(this string item, string fomat, DateTime? defaultDateTime = null)
        {
            if (item == null || string.IsNullOrEmpty(item))
                return defaultDateTime;
            try
            {
                var result = DateTime.ParseExact(item, fomat, CultureInfo.InvariantCulture);
                return result;
            }
            catch (Exception)
            {
                return defaultDateTime;
            }

        }

        public static DateTime? AsToUniversalTime(this DateTime? dateTime)
        {
            return dateTime?.ToUniversalTime();
        }
        public static DateTime? AsToLocalTime(this DateTime? dateTime)
        {
            return dateTime?.ToLocalTime();
        }
        // transform object into bool data type

        public static bool AsBool(this object item, bool defaultBool = default(bool))
        {
            if (item == null)
                return defaultBool;

            return new List<string>() { "yes", "y", "true" }.Contains(item.ToString().ToLower());
        }

        // transform string into byte array

        public static byte[] AsByteArray(this string s)
        {
            if (string.IsNullOrEmpty(s))
                return null;

            return Convert.FromBase64String(s);
        }

        // transform object into base64 string.

        public static string AsBase64String(this object item)
        {
            if (item == null)
                return null;
            ;

            return Convert.ToBase64String((byte[])item);
        }

        // transform object into Guid data type

        public static Guid AsGuid(this object item)
        {
            try { return new Guid(item.ToString()); }
            catch { return Guid.Empty; }
        }

        // concatenates SQL and ORDER BY clauses into a single string

        public static string OrderBy(this string sql, string sortExpression)
        {
            if (string.IsNullOrEmpty(sortExpression))
                return sql;

            return sql + " ORDER BY " + sortExpression;
        }


        // takes an enumerable source and returns a comma separate string.
        // handy for building SQL Statements (for example with IN () statements) from object collections

        public static string CommaSeparate<T, U>(this IEnumerable<T> source, Func<T, U> func)
        {
            return string.Join(",", source.Select(s => func(s).ToString()).ToArray());
        }

        public static string ByteArrayToString(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }

        public static DateTime? StringVniPaseDateTime(string strDateTime, string format)
        {
            try
            {
                DateTime dt;
                DateTime.TryParseExact(strDateTime, format, new CultureInfo("en-US"), DateTimeStyles.None, out dt);
                return dt == DateTime.MinValue ? DateTime.MaxValue : dt;
            }
            catch (Exception)
            {
                return DateTime.MaxValue;
            }

        }
        private static readonly Regex StripHTMLExpression = new Regex("<\\S[^><]*>", RegexOptions.IgnoreCase | RegexOptions.Singleline | RegexOptions.Multiline | RegexOptions.CultureInvariant | RegexOptions.Compiled);
        public static string StripHtml(this string target)
        {
            if (!String.IsNullOrEmpty(target))
            {
                return StripHTMLExpression.Replace(target, string.Empty);
            }
            return target;
        }


        public static string Sha256Encrypt(string password, string salt)
        {
            string saltAndPwd = String.Concat(password, salt);
            UTF8Encoding encoder = new UTF8Encoding();
            SHA256Managed sha256hasher = new SHA256Managed();
            byte[] hashedDataBytes = sha256hasher.ComputeHash(encoder.GetBytes(saltAndPwd));
            string hashedPwd = String.Concat(byteArrayToString(hashedDataBytes), salt);
            return hashedPwd;
        }


        public static string byteArrayToString(byte[] inputArray)
        {
            StringBuilder output = new StringBuilder("");
            for (int i = 0; i < inputArray.Length; i++)
            {
                output.Append(inputArray[i].ToString("X2"));
            }
            return output.ToString();
        }

        public static string CreateSalt(string userName)
        {
            var random = Guid.NewGuid().ToString();
            string username = String.Concat(userName, random);
            byte[] userBytes;
            string salt;
            userBytes = ASCIIEncoding.ASCII.GetBytes(username);
            long XORED = 0x00; foreach (int x in userBytes)
                XORED = XORED ^ x;
            Random rand = new Random(Convert.ToInt32(XORED));
            salt = rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            salt += rand.Next().ToString();
            return salt;
        }

        public const string UniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯqwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM1234567890_-";
        public static HashSet<char> UnicodeCharter;
        public const string DefaultSqlSearchText = "DefaultSqlSearchText";
        public const string DefaultSqlSearchTextParameter = "@DefaultSqlSearchText";
        public static string AsSqlSearchText(this string item, string defaultValue = DefaultSqlSearchText)
        {
            if (string.IsNullOrEmpty(item))
            {
                return defaultValue;
            }
            if (UnicodeCharter == null)
            {
                UnicodeCharter = new HashSet<char>(UniChars.Select(p => p));
            }
            StringBuilder stringBuilder = new StringBuilder();
            bool lastCharterValid = true;
            int i = 0;
            foreach (var s in item)
            {
                if (UnicodeCharter.Contains(s))
                {
                    stringBuilder.Append(s);
                    lastCharterValid = true;
                    i++;
                }
                else
                {
                    if (!lastCharterValid || i == 0)
                    {
                        continue;
                    }
                    if (s == ' ')
                    {
                        stringBuilder.Append(" NEAR ");
                    }
                    else
                    {
                        stringBuilder.Append(" OR ");
                    }
                    lastCharterValid = false;
                }

            }
            string text = stringBuilder.ToString().Trim();
            if (text.EndsWith("NEAR"))
            {
                text = text.Remove(text.Length - 4, 4);
            }
            else if (text.EndsWith("OR"))
            {
                text = text.Remove(text.Length - 2, 2);
            }
            return text;
        }
        public static bool IsValidEmail(string strIn)
        {
            try
            {
                MailAddress m = new MailAddress(strIn);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
            // Return true if strIn is in valid e-mail format.
            //return Regex.IsMatch(strIn, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
        }

        public static string GetMessage(this Exception exception)
        {
            if (exception != null)
                return
                    $"Message: {exception.Message},Data: {Serialize.JsonSerializeObject(exception.Data)},StackTrace: {exception.StackTrace}";
            return string.Empty;
        }

        public static string ToJson(this Exception exception)
        {
            if (exception != null)
                return Serialize.JsonSerializeObject(exception);
            return string.Empty;
        }

        public static void ExceptionAddParam(this Exception exception, string method, object input)
        {
            exception.Data["Method"] = method;
            exception.Data["Input"] = input;
        }

    }
}

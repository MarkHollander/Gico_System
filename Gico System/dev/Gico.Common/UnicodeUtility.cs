using System;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace Gico.Common
{
    public class UnicodeUtility
    {
        public const string UniChars = "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

        public const string KoDauChars =
            //"aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIOOOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyaaaaaaaaaaaaaaaaaeeeeeeeeeeediiiooooooooooooooooooouuuuuuuuuuuyyyyyaadoou";

        public const string KeyBoardChars = " `1234567890-=~!@#$%^&*()_+qwertyuiop[]{}|asdfghjkl;':zxcvbnm,./<>?*-+";

        public static string UnicodeToKoDau(string s)
        {
            string retVal = String.Empty;
            for (int i = 0; i < s.Length; i++)
            {
                var pos = UniChars.IndexOf(s[i].ToString(), StringComparison.Ordinal);
                if (pos >= 0)
                {
                    retVal += KoDauChars[pos];
                }
                else
                {
                    if (KeyBoardChars.IndexOf(s[i].ToString().ToLower(), StringComparison.Ordinal) >= 0)
                    {
                        retVal += s[i].ToString();
                    }
                }
            }

            return retVal;
        }
        public static string UnicodeToKoDauAndGach(string s)
        {
            if (string.IsNullOrEmpty(s))
                return "";
            string retVal = String.Empty;
            int pos;

            for (int i = 0; i < s.Length; i++)
            {
                pos = UniChars.IndexOf(s[i].ToString(), StringComparison.Ordinal);
                if (pos >= 0)
                    retVal += KoDauChars[pos];
                else
                    retVal += s[i];
            }
            String temp = retVal;
            for (int i = 0; i < retVal.Length; i++)
            {
                pos = Convert.ToInt32(retVal[i]);
                if (!((pos >= 97 && pos <= 122) || (pos >= 65 && pos <= 90) || (pos >= 48 && pos <= 57) || pos == 32))
                    temp = temp.Replace(retVal[i].ToString(), "");
            }
            temp = temp.Replace(" ", "-");
            while (temp.EndsWith("-"))
                temp = temp.Substring(0, temp.Length - 1);

            while (temp.IndexOf("--", StringComparison.Ordinal) >= 0)
                temp = temp.Replace("--", "-");

            retVal = temp;

            return retVal.ToLower();
        }
        public static string HtmlTabRemove(string input)
        {
            if (string.IsNullOrEmpty(input)) return input;

            input = Regex.Replace(input, "<style(.|\n)*?>(.|\n)*?</style>", string.Empty);
            input = Regex.Replace(input, @"<xml(.|\n)*?>(.|\n)*?</xml>", string.Empty); // remove all <xml></xml> tags and anything inbetween.  
            input = Regex.Replace(input, @"<script(.|\n)*?>(.|\n)*?</script>", string.Empty);
            input = Regex.Replace(input, @"<(.|\n)*?>", string.Empty); // remove any tags but not there content "<p>bob<span> johnson</span></p>" becomes "bob johnson"
            input = WebUtility.HtmlDecode(input);
            input = Regex.Replace(input, @"\s{2,}", " ");
            input = input.Replace("\"", string.Empty);
            //input = WebUtility.JavaScriptStringEncode(input);
            return input;
        }
        public static string ToHexString(string str)
        {
            var sb = new StringBuilder();

            var bytes = Encoding.Unicode.GetBytes(str);
            foreach (var t in bytes)
            {
                sb.Append(t.ToString("X2"));
            }

            return sb.ToString(); // returns: "48656C6C6F20776F726C64" for "Hello world"
        }

        public static string FromHexString(string hexString)
        {
            var bytes = new byte[hexString.Length / 2];
            for (var i = 0; i < bytes.Length; i++)
            {
                bytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            }

            return Encoding.Unicode.GetString(bytes); // returns: "Hello world" for "48656C6C6F20776F726C64"
        }
    }
}
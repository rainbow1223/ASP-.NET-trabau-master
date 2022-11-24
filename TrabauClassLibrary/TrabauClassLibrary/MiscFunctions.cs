using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Serialization;

namespace TrabauClassLibrary
{
    public class MiscFunctions
    {
        public static string Base64DecodingMethod(string Data)
        {
            byte[] encodedDataAsBytes = Convert.FromBase64String(Data);
            string returnValue = Encoding.UTF8.GetString(encodedDataAsBytes);
            return returnValue;
        }

        public static string Base64EncodingMethod(string Data)
        {
            byte[] toEncodeAsBytes = Encoding.UTF8.GetBytes(Data);
            string sReturnValues = Convert.ToBase64String(toEncodeAsBytes);
            return sReturnValues;
        }

        public static string EncryptAndEncode(string Data, string key)
        {
            try
            {
                string encryptedString = EncyptSalt.EncryptText(Data, key);
                string text = Base64EncodingMethod(encryptedString);
                return text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static string DecodeAndDecrypt(string Data, string key)
        {
            try
            {
                string decodedString = Base64DecodingMethod(Data);
                string text = EncyptSalt.DecryptText(decodedString, key);
                return text;
            }
            catch
            {
                return string.Empty;
            }
        }

        public static bool IsBase64String(string base64)
        {
            base64 = base64.Trim();
            return (base64.Length % 4 == 0) && Regex.IsMatch(base64, @"^[a-zA-Z0-9\+/]*={0,3}$", RegexOptions.None);
        }

        public static string ParseXpathString(string input)
        {

            if (input.Contains("'"))
            {
                int myindex = input.IndexOf("'");
                input = input.Insert(myindex, "'");
            }
            return input;
        }

        public static string GetXMLString(DataTable dt, string name)
        {
            string XMLData = "";
            try
            {
                dt.TableName = name;
                using (StringWriter sw = new StringWriter())
                {
                    dt.WriteXml(sw, false);
                    XMLData = ParseXpathString(sw.ToString());
                }
            }
            catch (Exception)
            {
            }
            return XMLData;
        }

        public static string RandomString(int length)
        {
            Random random = new Random();
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }



        public static string GetFileExtension(string base64data)
        {
            try
            {
                string filetype = base64data.Substring(base64data.IndexOf("data:", 0) + 5, base64data.IndexOf("base64,", 0) - 6);
                switch (filetype)
                {
                    case "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet":
                        filetype = "spreadsheet";
                        break;
                }
                return filetype;
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static string GetValue(List<dynamic> data, string element)
        {
            try
            {
                if (data == null)
                {
                    return string.Empty;
                }

                if (data.Count == 0)
                {
                    return string.Empty;
                }

                return data.SingleOrDefault(fi => fi.ItemKey == element).ItemValue;
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public static bool asBoolean(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return false;
            }

            if (bool.TryParse(value, out bool result))
            {
                return result;
            }
            else
            {
                return false;
            }
        }

        public static T FromXml<T>(string xml)
        {
            if (string.IsNullOrEmpty(xml))
            {
                return default(T);
            }

            try
            {
                var xmlserializer = new XmlSerializer(typeof(T));
                var stringReader = new StringReader(xml);
                using (var reader = XmlReader.Create(stringReader))
                {
                    return (T)xmlserializer.Deserialize(reader);
                }
            }
            catch
            {
                return default(T);
            }
        }

    }
}

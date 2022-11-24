using MimeDetective;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace TrabauClassLibrary
{
    public class MimeType_Validation
    {
        /// <summary>
        /// Check Mime Types, Valid Mime Types are application/pdf,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/excel,image/jpeg,image/png,application/vnd.openxmlformats-officedocument.presentationml.presentation,application/mspowerpoint
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="valid_mimetypes"></param>
        /// <returns></returns>
        public static Tuple<bool, string> CheckMimeType(byte[] bytes, string valid_mimetypes, string FilePath = "")
        {
            bool val = false;
            string _foundtype = string.Empty;
            try
            {
                bool extension_found = false;
                string all_valid_mimetypes = "video/x-m4v,application/pdf,application/vnd.openxmlformats-officedocument.spreadsheetml.sheet,application/excel,image/jpeg,image/jpg,image/gif,image/png,application/vnd.openxmlformats-officedocument.presentationml.presentation,application/mspowerpoint,application/vnd.openxmlformats-officedocument.wordprocessingml.document,application/msword,image/tiff";
                if (valid_mimetypes == string.Empty)
                {
                    valid_mimetypes = all_valid_mimetypes;
                    extension_found = true;
                }
                else
                {
                    for (int i = 0; i < valid_mimetypes.Split(',').Length; i++)
                    {
                        string _mime = valid_mimetypes.Split(',')[i];

                        if (all_valid_mimetypes.Contains(_mime))
                        {
                            extension_found = true;
                        }
                        else
                        {
                            extension_found = false;
                            break;
                        }
                    }
                }

                if (extension_found)
                {

                    FileType fileType = bytes.GetFileType();
                    for (int i = 0; i < valid_mimetypes.Split(',').Length; i++)
                    {
                        string _mime = valid_mimetypes.Split(',')[i];
                        if (_mime == fileType.Mime)
                        {
                            _foundtype = _mime;
                            val = true;
                            break;
                        }
                    }
                }
            }
            catch (Exception)
            {
                val = false;
            }
            return new Tuple<bool, string>(val, _foundtype);
        }

    }
}

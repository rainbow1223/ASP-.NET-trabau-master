using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using TrabauClassLibrary.DLL.profile.settings;

namespace TrabauClassLibrary
{
    public static class ImageProcessing
    {
        public static string GetUserPicture(Int64 UserId, int Height, int Width)
        {
            string pic = "";
            try
            {
                settings_changes obj = new settings_changes();
                DataSet ds = obj.LoadProfilePicture(UserId);

                if (ds != null)
                {
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            byte[] pic_bytes = (byte[])ds.Tables[0].Rows[0]["ProfilePicture"];
                            if (Width > 0 && Height > 0)
                            {
                                pic_bytes = ScaleFreeHeight(pic_bytes, Width, Height);
                            }
                            pic = "data:image;base64," + Convert.ToBase64String(pic_bytes);
                        }
                        else
                        {
                            string path = HttpContext.Current.Server.MapPath("~/assets/uploads/avtar.jpg");
                            pic = "data:image;base64," + Convert.ToBase64String(File.ReadAllBytes(path));
                        }
                    }
                    else
                    {
                        string path = HttpContext.Current.Server.MapPath("~/assets/uploads/avtar.jpg");
                        pic = "data:image;base64," + Convert.ToBase64String(File.ReadAllBytes(path));
                    }
                }
            }
            catch (Exception ex)
            {
                string path = HttpContext.Current.Server.MapPath("~/assets/uploads/avtar.jpg");
                pic = "data:image;base64," + Convert.ToBase64String(File.ReadAllBytes(path));
            }
            return pic;
        }

        public static string GetCompanyLogo(Int64 UserId, int Height, int Width)
        {
            string pic = "";
            try
            {
                settings_changes obj = new settings_changes();
                DataSet ds = obj.LoadCompanyLogo(UserId);

                if (ds != null)
                {
                    if (ds.Tables.Count != 0)
                    {
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            byte[] pic_bytes = (byte[])ds.Tables[0].Rows[0]["CompanyLogo"];
                            if (Width > 0 && Height > 0)
                            {
                                pic_bytes = ScaleFreeHeight(pic_bytes, Width, Height);
                            }
                            pic = "data:image;base64," + Convert.ToBase64String(pic_bytes);
                        }
                        else
                        {
                            string path = HttpContext.Current.Server.MapPath("~/assets/images/building.png");
                            pic = "data:image;base64," + Convert.ToBase64String(File.ReadAllBytes(path));
                        }
                    }
                    else
                    {
                        string path = HttpContext.Current.Server.MapPath("~/assets/images/building.png");
                        pic = "data:image;base64," + Convert.ToBase64String(File.ReadAllBytes(path));
                    }
                }
            }
            catch (Exception ex)
            {
                string path = HttpContext.Current.Server.MapPath("~/assets/images/building.png");
                pic = "data:image;base64," + Convert.ToBase64String(File.ReadAllBytes(path));
            }
            return pic;
        }

        public static byte[] ScaleFreeHeight(byte[] bytes, int newWidth, int newHeight)
        {
            var newHeight2 = 0;
            System.IO.MemoryStream ms = new System.IO.MemoryStream(bytes);
            System.Drawing.Image returnImage = System.Drawing.Image.FromStream(ms);

            var image = returnImage;
            if (newHeight == 0)
            {
                newHeight2 = Convert.ToInt32(newWidth * (1.0000000 * image.Height / image.Width));
            }
            else
            {
                newHeight2 = newHeight;
            }
            newWidth = Convert.ToInt32(newHeight * (1.0000000 * image.Width / image.Height));
            var thumbnail = new Bitmap(newWidth, newHeight2);
            var graphic = Graphics.FromImage(thumbnail);
            graphic.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphic.SmoothingMode = SmoothingMode.HighQuality;
            graphic.PixelOffsetMode = PixelOffsetMode.HighQuality;
            graphic.CompositingQuality = CompositingQuality.HighQuality;

            graphic.DrawImage(image, 0, 0, newWidth, newHeight2);


            System.IO.MemoryStream objMemoryStream = new System.IO.MemoryStream();
            thumbnail.Save(objMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);

            byte[] imageContent = new byte[objMemoryStream.Length];
            objMemoryStream.Position = 0;
            objMemoryStream.Read(imageContent, 0, (int)objMemoryStream.Length);
            return imageContent;
        }
    }
}

using SSMCPushNotificationCenter.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace SSMCPushNotificationCenter.Repositories
{
    public class ContentRepository
    {
        public int UploadImageInDataBase(HttpPostedFileBase file, tb_notification notif)
        {
            notif.Image = ConvertToBytes(file);
            var Content = new tb_notification
            {
                Title = notif.Title,
                Message = notif.Message,
                Image = notif.Image,
            };

            if (notif.Image != null)
            {
                return 1;
            }

            return 0;

        }

        public byte[] ConvertToBytes(HttpPostedFileBase image)
        {
            byte[] imageBytes = null;
            BinaryReader reader = new BinaryReader(image.InputStream);
            imageBytes = reader.ReadBytes((int)image.ContentLength);
            return imageBytes;
        }
    }
}
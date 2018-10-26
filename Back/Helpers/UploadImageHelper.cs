using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Net.Http;

namespace Back.Helpers
{
    public class UploadImageHelper
    {      

        public static Boolean p_AEPSAD_UploadImage(HttpPostedFileBase IconoFile, String PrefixFileName, ref String FileName, ref String Error)
        {           

            var validImageTypes = new string[]
             {
                "image/gif",
                "image/jpeg",
                "image/pjpeg",
                "image/png"
             };

            if (IconoFile == null || IconoFile.ContentLength == 0)
            {
                Error = "This field is required";
                return false;
            }
            else if (!validImageTypes.Contains(IconoFile.ContentType))
            {
                Error = "Please choose either a GIF, JPG or PNG image.";
                return false;
            }

            System.IO.Stream stream = IconoFile.InputStream;
            System.Drawing.Image image = System.Drawing.Image.FromStream(stream);

            int height = image.Height;
            int width = image.Width;

            if( image.Width > 48 || image.Height > 48)
            {
                Error = "Máximo 48";
                return false;
            }

            try
            {
                //Use Namespace called :  System.IO  
                string _fileName = Path.GetFileNameWithoutExtension(IconoFile.FileName);

                //To Get File Extension  
                string _fileExtension = Path.GetExtension(IconoFile.FileName);

                //Add Current Date To Attached File Name  
                _fileName = PrefixFileName + _fileExtension;

                //Get Upload path from Web.Config file AppSettings.  
                string _uploadPath = Path.Combine(HttpContext.Current.Server.MapPath("~" + ConfigHelper.PATH_UPLOADS_ICONS), FileName);

                //Its Create complete path to store in server.  
                FileName = _fileName;

                //To copy and save file into server.  
                IconoFile.SaveAs(_uploadPath + _fileName);

                return true;
            }
            catch (Exception ex) {

                Error = "Hubo un error al subir el fichero";
                return false;
            }

        }
    }
}
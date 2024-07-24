using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VentaMusical.BusinessLogic.Extensions
{
    public class Base64Converter
    {
        public string ConvertIFormFileToBase64String(IFormFile file)
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                file.CopyTo(memoryStream);
                byte[] bytes = memoryStream.ToArray();

                // Convert byte array to Base64 string
                string base64String = Convert.ToBase64String(bytes);

                return base64String;
            }
        }

        public string ConvertBase64StringToString(string base64String)
        {
            // Convert Base64 string to regular string
            byte[] bytes = Convert.FromBase64String(base64String);
            string regularString = System.Text.Encoding.UTF8.GetString(bytes);

            return regularString;
        }
    }
}

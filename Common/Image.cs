using System;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using System.IO;

namespace SugarFreeHealthyDiet.Common
{
    public class Image
    {
        protected IFormFile file;
        protected byte[] image;
        public Image(IFormFile file)
        {
            this.file = file;
        }

        public virtual byte[] GetBytes()
        {
            using (var ms = new MemoryStream())
            {
                file.CopyTo(ms);
                image = ms.ToArray();
            }

            return image;
        }

        public virtual byte[] GetThumbnailBytes()
        {
            EnsureImageExists();

            int imgHeight = 200;
            int imgWidth = 300;

            using (SixLabors.ImageSharp.Image thumbnail = SixLabors.ImageSharp.Image.Load(image))
            {
                if (thumbnail.Width < thumbnail.Height)
                {
                    //portrait image                  
                    var imgRatio = (float)imgHeight / (float)thumbnail.Height;
                    imgWidth = Convert.ToInt32(thumbnail.Height * imgRatio);
                }
                else if (thumbnail.Height < thumbnail.Width)
                {
                    //landscape image
                    var imgRatio = (float)imgWidth / (float)thumbnail.Width;
                    imgHeight = Convert.ToInt32(thumbnail.Height * imgRatio);
                }

                thumbnail.Mutate(x => x.Resize(imgWidth, imgWidth));
                return thumbnail;
            }
        }

        private void EnsureImageExists()
        {
            if (image == null)
            {
                GetBytes();
            }
        }
    }
}

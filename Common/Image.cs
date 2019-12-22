using System;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
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

            using (SixLabors.ImageSharp.Image thumbnail = SixLabors.ImageSharp.Image.Load(image))
            {
                var resizedImage = ResizeImage(thumbnail);

                using (var ms = new MemoryStream())
                {
                    resizedImage.SaveAsJpeg(ms);
                    return ms.ToArray();
                }
            }
        }

        private SixLabors.ImageSharp.Image ResizeImage(SixLabors.ImageSharp.Image image)
        {
            int imgHeight = 200;
            int imgWidth = 300;

            if (image.Width < image.Height)
            {
                //portrait image                  
                var imgRatio = (float)imgHeight / (float)image.Height;
                imgWidth = Convert.ToInt32(image.Height * imgRatio);
            }
            else if (image.Height < image.Width)
            {
                //landscape image
                var imgRatio = (float)imgWidth / (float)image.Width;
                imgHeight = Convert.ToInt32(image.Height * imgRatio);
            }

            image.Mutate(x => x.Resize(imgWidth, imgWidth));

            return image;
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

using System;
using System.IO;

using System.Drawing;
using System.Linq;
using ImageHelper.Standard.Interface;

namespace ImageHelper.Standard.Services
{
    public class ImageManager : IImageManager
    {
        public Image ProcessExif(Image image)
        {
            //Image originalImage = Image.FromStream(image);

            if (image.PropertyIdList.Contains(0x0112))
            {
                int rotationValue = image.GetPropertyItem(0x0112).Value[0];
                switch (rotationValue)
                {
                    case 1: // landscape, do nothing
                        break;

                    case 8: // rotated 90 right
                            // de-rotate:
                        image.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                        break;

                    case 3: // bottoms up
                        image.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                        break;

                    case 6: // rotated 90 left
                        image.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                        break;

                    default: //no need to cater for other scenarios
                        break;
                }
            }


            return image;
        }

        public bool IsMinimumSizeRequirementsMet(Stream stream, int width, int height)
        {
            using (var image = Image.FromStream(stream))
            {
                if (image.Width >= width && image.Height >= height)
                {
                    image.Dispose();
                    return true;
                }
            }

            return false;
        }
    }
}

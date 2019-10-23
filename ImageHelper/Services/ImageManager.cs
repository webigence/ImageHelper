using System;
using System.IO;

using System.Drawing;
using System.Linq;
using ImageHelper.Interface;

namespace ImageHelper.Services
{
    public class ImageManager : IImageManager
    {
        public Stream ProcessExif(Stream image)
        {
            Image originalImage = Image.FromStream(image);

            if (originalImage.PropertyIdList.Contains(0x0112))
            {
                int rotationValue = originalImage.GetPropertyItem(0x0112).Value[0];
                switch (rotationValue)
                {
                    case 1: // landscape, do nothing
                        break;

                    case 8: // rotated 90 right
                            // de-rotate:
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate270FlipNone);
                        break;

                    case 3: // bottoms up
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate180FlipNone);
                        break;

                    case 6: // rotated 90 left
                        originalImage.RotateFlip(rotateFlipType: RotateFlipType.Rotate90FlipNone);
                        break;

                    default: //no need to cater for other scenarios
                        break;
                }
            }

            var ms = new MemoryStream();
            originalImage.Save(ms, originalImage.RawFormat);

            // Reset the position to the start in case user wants to read it from the stream
            ms.Position = 0;

            return ms;
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

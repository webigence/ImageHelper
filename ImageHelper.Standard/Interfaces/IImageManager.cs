using System.Drawing;
using System.IO;

namespace ImageHelper.Standard.Interface
{
    public interface IImageManager
    {
        Image ProcessExif(Image image);
        bool IsMinimumSizeRequirementsMet(Stream stream, int width, int height);
    }
}

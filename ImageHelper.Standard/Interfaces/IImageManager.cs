using System.IO;

namespace ImageHelper.Standard.Interface
{
    public interface IImageManager
    {
        Stream ProcessExif(Stream image);
        bool IsMinimumSizeRequirementsMet(Stream stream, int width, int height);
    }
}

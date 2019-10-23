using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Text;

namespace ImageHelper.Interface
{
    public interface IImageManager
    {
        Stream ProcessExif(Stream image);
        bool IsMinimumSizeRequirementsMet(Stream stream, int width, int height);
    }
}

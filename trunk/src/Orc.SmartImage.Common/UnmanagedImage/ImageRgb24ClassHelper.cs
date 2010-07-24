﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    using TPixel = Rgb24;
    using TCache = System.Int32;
    using TKernel = System.Int32;

    public static partial class ImageRgb24ClassHelper
    {
        #region include "ImageClassHelper_Template.cs"
        #endregion
    }

    public partial class ImageRgb24
    {
        #region include "Image_Template.cs"
        #endregion
    }

    public partial struct Rgb24
    {
        #region include "TPixel_Template.cs"
        #endregion
    }
}

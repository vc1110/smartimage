﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Orc.SmartImage
{
    using TPixel = System.Int32;
    using TCache = System.Int32;
    using TKernel = System.Int32;
    using TImage = Orc.SmartImage.ImageInt32;

    public static partial class ImageInt32ClassHelper
    {
        #region include "ImageClassHelper_Template.cs"
        #endregion
    }

    public partial class ImageInt32
    {
        #region include "Image_Template.cs"
        #endregion

        #region include "ImageFilter_Template.cs"
        #endregion
    }
}




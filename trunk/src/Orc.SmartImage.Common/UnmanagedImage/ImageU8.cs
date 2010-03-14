﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Orc.SmartImage
{
    public struct ByteConverter : IColorConverter
    {
        #region IColorConvert Members

        public unsafe void Copy(byte* from, Argb32* to)
        {
            Byte data = *from;
            to->Alpha = 255;
            to->Red = data;
            to->Green = data;
            to->Blue = data;
        }

        public unsafe void Copy(Argb32* from, byte* to)
        {
            *to = (Byte)(from->Red * 0.299 + from->Green * 0.587 + from->Blue * 0.114);
        }

        public unsafe void Copy(Rgb24* from, byte* to)
        {
            *to = (Byte)(from->Red * 0.299 + from->Green * 0.587 + from->Blue * 0.114);
        }

        #endregion
    }

    public class ImageU8 : UnmanagedImage<Byte>
    {
        public unsafe Byte* Start { get { return (Byte*)this.StartIntPtr; } }

        public unsafe ImageU8(Int32 width, Int32 height)
            : base(width, height)
        {
        }

        public ImageU8(Bitmap map)
            : base(map)
        {
        }

        public unsafe Byte this[int index]
        {
            get { return *(Start + index); }
            set {
                *(Start + index) = value; 
            }
        }

        protected override IColorConverter GetColorConvert()
        {
            return new ByteConverter();
        }

    }
}

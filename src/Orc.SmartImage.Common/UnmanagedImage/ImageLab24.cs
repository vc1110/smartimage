﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Orc.SmartImage
{
    using Orc.Util;

    public partial struct Lab24 : IMetriable<Lab24>
    {
        public byte L;
        public byte A;
        public byte B;

        public Lab24(byte l,byte a,byte b)
        {
            L = l;
            A = a;
            B = b;
        }

        public Lab24(int l,int a,int b)
        {
            L = (byte)l;
            A = (byte)a;
            B = (byte)b;
        }

        public int GetDistanceSquare(Lab24 other)
        {
            int deltaL = this.L - other.L;
            int deltaA = this.A - other.A;
            int deltaB = this.B - other.B;
            return (int)(0.16 * deltaL * deltaL) + deltaA * deltaA + deltaB * deltaB;
        }

        public double GetDistance(Lab24 other)
        {
            return Math.Sqrt(GetDistanceSquare(other));
        }

        #region RGB 与 Lab 互相转换 ，从 OpenCV 项目中借鉴得到

        
        public static unsafe Lab24 CreateFrom(Rgb24 color)
        {
            Lab24 dst = new Lab24();
            UnmanagedImageConverter.ToLab24(&color, &dst, 1);
            return dst;
        }

        public static unsafe Lab24 CreateFrom(Byte red,Byte green,Byte blue)
        {
            return CreateFrom(new Rgb24(red,green,blue));
        }

        public unsafe Rgb24 ToRgb24()
        {
            Rgb24 dst = new Rgb24();
            fixed (Lab24* l = &this)
            {
                UnmanagedImageConverter.ToRgb24(l, &dst, 1);
            }
            return dst;
        }

        #endregion

        #region Test

        public static void ConvertTest()
        {
            Boolean error = false;

            for (int r = 0; r < 256; r += 2)
            {
                for (int g = 0; g < 256; g += 2)
                {
                    for (int b = 0; b < 256; b += 2)
                    {
                        Rgb24 src = new Rgb24(r, g, b);
                        Lab24 dst = Lab24.CreateFrom(src);
                        Rgb24 rvt = dst.ToRgb24();
                        if (Math.Abs(src.Red - rvt.Red) + Math.Abs(src.Green - rvt.Green) + Math.Abs(src.Blue - rvt.Blue) > 16)
                        {
                            error = true;
                        }

                    }
                }
            }

            Console.WriteLine(error);
        }

        #endregion
    }

    public struct Lab24Converter : IColorConverter
    {
        public unsafe void Copy(Rgb24* from, void* to, int length)
        {
            UnmanagedImageConverter.ToLab24(from, (Lab24*)to, length);
        }

        public unsafe void Copy(Argb32* from, void* to, int length)
        {
            UnmanagedImageConverter.ToLab24(from, (Lab24*)to, length);
        }

        public unsafe void Copy(byte* from, void* to, int length)
        {
            UnmanagedImageConverter.ToLab24(from, (Lab24*)to, length);
        }
    }

    public partial class ImageLab24 : UnmanagedImage<Lab24>
    {
        public unsafe ImageLab24(Int32 width,Int32 height)
            : base(width,height)
        {
        }

        public unsafe ImageLab24(ImageRgb24 img)
            : base(img.Width, img.Height)
        {
            int length = img.Length;
            UnmanagedImageConverter.ToLab24(img.Start, (Lab24*)this.StartIntPtr, length);
        }

        public unsafe ImageRgb24 ToImageRgb24()
        {
            ImageRgb24 img = new ImageRgb24(this.Width, this.Height);
            UnmanagedImageConverter.ToRgb24((Lab24*)this.StartIntPtr, img.Start, img.Length);
            return img;
        }

        protected override IColorConverter CreateByteConverter()
        {
            return new Lab24Converter();
        }

        public override IImage Clone()
        {
            ImageLab24 img = new ImageLab24(this.Width,this.Height);
            img.CloneFrom(this);
            return img;
        }

        protected override System.Drawing.Imaging.PixelFormat GetOutputBitmapPixelFormat()
        {
            return System.Drawing.Imaging.PixelFormat.Format24bppRgb;
        }

        protected override unsafe void ToBitmapCore(byte* src, byte* dst, int width)
        {
            UnmanagedImageConverter.ToRgb24((Lab24*)src, (Rgb24*)dst, width);
        }
    }
}

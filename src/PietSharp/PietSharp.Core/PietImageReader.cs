using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;

namespace PietSharp.Core
{
    public class PietImageReader
    {
        public uint[,] ReadImage(string path)
        {
            using var image = Image.Load(path);
            var rgb = image.CloneAs<Rgb24>();

            uint[,] pixels = new uint[image.Height, image.Width];

            for (var y = 0; y < image.Height; y++)
            {
                Span<Rgb24> pixelRowSpan = rgb.GetPixelRowSpan(y);
                for (var x = 0; x < image.Width; x++)
                {
                    var pix = pixelRowSpan[x];
                    pixels[y, x] = (uint) ((pix.R << 16) | (pix.G << 8) | pix.B);
                }
            }

            return pixels;
        }
    }
}

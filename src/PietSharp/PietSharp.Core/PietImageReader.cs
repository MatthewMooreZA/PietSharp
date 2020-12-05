using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.PixelFormats;

namespace PietSharp.Core
{
    public class PietImageReader
    {
        public uint[,] ReadImage(string path, int? codelSize = null)
        {
            using var image = Image.Load(path);
            var rgb = image.CloneAs<Rgb24>();

            var step = codelSize ?? EstimateCodelSize(rgb);

            uint[,] pixels = new uint[image.Height / step, image.Width / step];
            int outY = 0;
            for (var y = 0; y < image.Height; y += step, outY++)
            {
                Span<Rgb24> pixelRowSpan = rgb.GetPixelRowSpan(y);
                var outX = 0;
                for (var x = 0; x < image.Width; x += step, outX++)
                {
                    var pix = pixelRowSpan[x];
                    pixels[outY, outX] = ToRgb(pix);
                }
            }

            return pixels;
        }


        private uint ToRgb(Rgb24 rgb24)
        {
            return (uint)((rgb24.R << 16) | (rgb24.G << 8) | rgb24.B);
        }

        private int EstimateCodelSize(Image<Rgb24> rgb)
        {
            // test the first row

            int count = 1;
            int minCount = int.MaxValue;

            int[] rowsToTest = { 0, rgb.Height - 1 };

            foreach (var rowIndex in rowsToTest)
            {
                Span<Rgb24> firstRow = rgb.GetPixelRowSpan(rowIndex);

                var prevColour = ToRgb(firstRow[0]);

                for (var i = 1; i < firstRow.Length; i++)
                {
                    var currentColour = ToRgb(firstRow[i]);
                    if (currentColour == prevColour)
                    {
                        count++;
                    }
                    else
                    {
                        if (count < minCount)
                        {
                            minCount = count;
                        }
                        prevColour = currentColour;
                        count = 1;
                    }
                }

                if (count < minCount)
                {
                    minCount = count;
                }
            }
            return minCount;
        }

        public int EstimateCodelSize(string path)
        {
            using var image = Image.Load(path);
            var rgb = image.CloneAs<Rgb24>();

            return EstimateCodelSize(rgb);
        }
    }
}

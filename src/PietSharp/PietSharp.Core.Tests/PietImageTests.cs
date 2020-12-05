using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using PietSharp.Core.Tests.Utils;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Advanced;
using SixLabors.ImageSharp.ColorSpaces;
using SixLabors.ImageSharp.PixelFormats;
using Xunit;

namespace PietSharp.Core.Tests
{
    public class PietImageTests
    {
        [Theory]
        [MemberData(nameof(GetPietPrograms), ".\\Images")]
        public void TestImage(string pietImage, string expectedOutput)
        {
            var imageReader = new PietImageReader();
            uint[,] pixels = imageReader.ReadImage(pietImage);
            var io = new PietTestIO();
            var session = new PietSession(pixels, io);

            session.Run();

            Assert.Equal(expectedOutput, io.OutputString());
            
        }

        public static IEnumerable<object[]> GetPietPrograms(string directory)
        {

            foreach (var outputFile in Directory.EnumerateFiles(directory, "*.out.txt"))
            {
                var imageFile = outputFile.Replace(".out.txt", "");

                if (File.Exists(outputFile))
                {
                    string expectedOutput = File.ReadAllText(outputFile)
                        .Replace("\\n","\n");


                    yield return new object[] { imageFile, expectedOutput };
                }
            }
        }
    }
}

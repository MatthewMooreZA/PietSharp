using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using PietSharp.Core.Tests.Utils;
using Xunit;

namespace PietSharp.Core.Tests
{
    public class PietImageTests
    {
        [Theory]
        [MemberData(nameof(GetPietPrograms), ".\\Images")]
        public void TestImage(string pietImage, string inputs, string expectedOutput)
        {
            var input = string.IsNullOrWhiteSpace(inputs) 
                ? new List<string>() 
                : inputs.Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            var imageReader = new PietImageReader();
            uint[,] pixels = imageReader.ReadImage(pietImage);
            var io = new PietTestIO(input, maxOutput: 100);
            var session = new PietSession(pixels, io);

            session.Run();

            Assert.Equal(expectedOutput, io.OutputString());
            
        }

        public static IEnumerable<object[]> GetPietPrograms(string directory)
        {

            foreach (var testFile in Directory.EnumerateFiles(directory, "*.test.txt"))
            {
                var imageFile = testFile.Replace(".test.txt", "");

                if (File.Exists(testFile))
                {
                    string data = File.ReadAllText(testFile);
                    foreach (var scenario in data.Split("\r\n"))
                    {
                        var splits = scenario.Split('|', 2);

                        string expectedOutput;
                        var inputs = string.Empty;
                        if (splits.Length == 1)
                        {
                            // output only
                            expectedOutput = splits[0].TrimStart();
                        }
                        else
                        {
                            inputs = splits[0];
                            expectedOutput = splits[1].TrimStart();
                        }

                        expectedOutput = expectedOutput.Replace("\\n", "\n").Replace("\\r", "\r");

                        yield return new object[] { imageFile, inputs, expectedOutput };
                    }


                }
            }
        }
    }
}

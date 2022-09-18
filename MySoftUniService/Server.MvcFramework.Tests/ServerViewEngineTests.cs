using Server.MvcFramework.ViewEngine;
using System;
using System.Collections.Generic;
using System.IO;
using Xunit;

namespace Server.MvcFramework.Tests
{
    public class ServerViewEngineTests
    {
        [Theory]
        [InlineData("Clean")]
        [InlineData("IfElseFor")]
        [InlineData("ViewModel")]
        public void TestGetHtml(string fileName)
        {
            var model = new TestViewModel
            {
                Name = "Ceci",
                Year = 2022,
                Numbers = new List<int> { 1, 10, 100, 1000, 10000 },
            };
            IViewEngine viewEngine = new SurverViewEngine();
            var view = File.ReadAllText($"ViewTests/{fileName}.html");
            var result = viewEngine.GetHtml(view, model);
            var expectedResult = File.ReadAllText($"ViewTests/{fileName}.Result.html");
            Assert.Equal(expectedResult, result);
        }

        public class TestViewModel
        {
            public int Year { get; set; }

            public string Name { get; set; }

            public IEnumerable<int> Numbers { get; set; }
        }
    }
}

using Microsoft.VisualStudio.TestTools.UnitTesting;
using ASEProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ASEProject.Tests
{
    [TestClass()]
    public class ParameterParserTests
    {
        [TestMethod()]
        public void moveToTest()
        {
            ParameterParser parser = new ParameterParser();

            Assert.IsFalse(parser.validateInput("move", "moveTo(100, 100, 200"));
        }

        [TestMethod()]
        public void TriangleTest()
        {
            ParameterParser parser = new ParameterParser();

            Assert.IsFalse(parser.validateInput("triangle", "triangle(20, 20)"));
        }

        [TestMethod()]
        public void CircleTest()
        {
            ParameterParser parser = new ParameterParser();


            Assert.IsFalse(parser.validateInput("circle", "circle(20, 20)"));
        }

        [TestMethod()]
        public void RectangleTest()
        {
            ParameterParser parser = new ParameterParser();

            Assert.IsFalse(parser.validateInput("rectangle", "rectangle(0)"));
        }

        [TestMethod()]
        public void UnknownCommandTest()
        {
            ParameterParser parser = new ParameterParser();

            Assert.IsNull(parser.parseParams("Unknown", "Test case"));
        }
    }
}
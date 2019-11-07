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

            try
            {
                parser.parseParams("move", "moveTo(100, 100, 200)");
            } catch(Exception e)
            {
                StringAssert.Contains(e.Message, "Invalid parameters");
                return;
            }
            Assert.Fail("No exception was thrown!");
        }

        [TestMethod()]
        public void TriangleTest()
        {
            ParameterParser parser = new ParameterParser();

            try
            {
                parser.parseParams("triangle", "triangle(100, 100)");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Invalid parameters");
                return;
            }
            Assert.Fail("No exception was thrown!");
        }

        [TestMethod()]
        public void CircleTest()
        {
            ParameterParser parser = new ParameterParser();

            try
            {
                parser.parseParams("circle", "circle(100, 100, 200)");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Invalid parameters");
                return;
            }
            Assert.Fail("No exception was thrown!");
        }

        [TestMethod()]
        public void RectangleTest()
        {
            ParameterParser parser = new ParameterParser();

            try
            {
                parser.parseParams("rectangle", "rectangle(100, 100, 200)");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Invalid parameters");
                return;
            }
            Assert.Fail("No exception was thrown!");
        }

        [TestMethod()]
        public void UnknownCommandTest()
        {
            ParameterParser parser = new ParameterParser();

            try
            {
                parser.parseParams("", "Test");
            }
            catch (Exception e)
            {
                StringAssert.Contains(e.Message, "Unknown command");
                return;
            }
            Assert.Fail("No exception was thrown!");
        }
    }
}
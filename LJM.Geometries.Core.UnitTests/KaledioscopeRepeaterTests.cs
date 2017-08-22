using Microsoft.VisualStudio.TestTools.UnitTesting;
using LJM.Geometries.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJM.Geometries.Core.Tests
{
	[TestClass()]
	public class KaledioscopeRepeaterTests
	{
		[TestMethod()]
		public void AlphaToIntConverterTest_Success()
		{
			int result = KaledioscopeRepeater.AlphaToIntConverter("B");
			Assert.AreEqual(2, result);
		}

		[TestMethod()]
		public void AlphaToIntConverterTest_Double_Success()
		{
			int result = KaledioscopeRepeater.AlphaToIntConverter("AB");
			Assert.AreEqual(28, result);
		}

		[TestMethod()]
		public void AlphaToIntConverterTest_DoubleB_Success()
		{
			int result = KaledioscopeRepeater.AlphaToIntConverter("BB");
			Assert.AreEqual(54, result);
		}

		[TestMethod()]
		public void IntToAlphaConverterTest_Success()
		{
			string result = KaledioscopeRepeater.IntToAlphaConverter(2);
			Assert.AreEqual("B", result);
		}

		[TestMethod()]
		public void IntToAlphaConverterTest_Double_Success()
		{
			string result = KaledioscopeRepeater.IntToAlphaConverter(28);
			Assert.AreEqual("AB", result);
		}

		[TestMethod()]
		public void IntToAlphaConverterTest_DoubleB_Success()
		{
			string result = KaledioscopeRepeater.IntToAlphaConverter(54);
			Assert.AreEqual("BB", result);
		}

		[TestMethod()]
		public void GetRowAndColumn_Success()
		{
			KaledioscopeRepeater repeater = testSetup();
			Tuple<string, int> value = repeater.GetRowAndColumn(new Vertex(2.0, 1.0), new Vertex(2.0, 0.0), new Vertex(1.0, 1.0));
			Assert.AreEqual(new Tuple<string, int>("B", 4), value);
		}

		[TestMethod()]
		public void GetRowAndColumn_Top_Success()
		{
			KaledioscopeRepeater repeater = testSetup();
			Tuple<string, int> value = repeater.GetRowAndColumn(new Vertex(2.0, 1.0), new Vertex(1.0, 2.0), new Vertex(1.0, 1.0));
			Assert.AreEqual(new Tuple<string, int>("A", 3), value);
		}

		[TestMethod()]
		public void GetXSubIndex_Success()
		{
			KaledioscopeRepeater repeater = testSetup();
			Triangle tri = new Triangle(new Vertex(1.0, 1.0), new Vertex(0.0, 1.0), new Vertex(1.0, 0.0));
			int result = repeater.GetXSubIndex(tri.GetCentroid());
			Assert.AreEqual(2, result);
		}

		[TestMethod()]
		public void GetXSubIndexCount_Success()
		{
			KaledioscopeRepeater repeater = testSetup();
			int result = repeater.GetXSubIndexCount();
			Assert.AreEqual(2, result);
		}

		[TestMethod()]
		public void GetAllLines_Success()
		{
			KaledioscopeRepeater repeater = testSetup();
			List<Line> lines = repeater.GetAllLines();

			// We should have 24 lines for 8 triangles
			Assert.AreEqual(24, lines.Count);

		}

		[TestMethod()]
		public void GetCompositeHeight_Success()
		{
			KaledioscopeRepeater repeater = testSetup();
			int height = repeater.GetCompositeHeight();
			Assert.AreEqual(2, height);
		}

		[TestMethod()]
		public void GetCompositeWidth_Success()
		{
			KaledioscopeRepeater repeater = testSetup();
			int width = repeater.GetCompositeWidth();
			Assert.AreEqual(2, width);
		}

		private KaledioscopeRepeater testSetup()
		{
			// Create two triangle polygons
			Triangle tri1 = new Triangle(
				new Vertex(0.0, 0.0),
				new Vertex(0.0, 1.0),
				new Vertex(1.0, 0.0));
			Triangle tri2 = new Triangle(
				new Vertex(1.0, 1.0),
				new Vertex(0.0, 1.0),
				new Vertex(1.0, 0.0));
			List<IPolygon> triList = new List<IPolygon>() { tri1, tri2 };

			KaleidoscopeImage image = new KaleidoscopeImage(triList, 1, 1);

			return new KaledioscopeRepeater(image, "B", 2);

		}

	}
}
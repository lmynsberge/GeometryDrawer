using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LJM.Geometries.Core.Tests
{
	[TestClass()]
	public class LineTests
	{
		[TestMethod()]
		public void GetLengthTest_Success()
		{
			Assert.AreEqual(4.0, Line.GetLength(0, 4, 0, 0));
		}

		[TestMethod()]
		public void SameLines_AreEqual()
		{
			// Setup/Test
			Vertex v1 = new Vertex(1.0, 2.0);
			Vertex v2 = new Vertex(0.0, 1.0);
			Line l1 = new Line(v1, v2);
			Line l2 = new Line(v1, v2);

			// Assert
			Assert.AreEqual(l1, l2);
		}

		[TestMethod()]
		public void DifferentLines_AreNotEqual()
		{
			// Setup/Test
			Vertex v1 = new Vertex(1.0, 2.0);
			Vertex v2 = new Vertex(0.0, 1.0);
			Vertex v3 = new Vertex(0.0, 2.0);
			Line l1 = new Line(v1, v2);
			Line l2 = new Line(v1, v3);

			// Assert
			Assert.AreNotEqual(l1, l2);
		}

		[TestMethod()]
		public void SameLines_AltCtor_AreEqual()
		{
			// Setup/Test
			Line l1 = new Line(1.0, 2.0, 0.0, 1.0);
			Line l2 = new Line(1.0, 2.0, 0.0, 1.0);

			// Assert
			Assert.AreEqual(l1, l2);
		}

		[TestMethod()]
		[ExpectedException(typeof(IllDefinedGemoetryException))]
		public void SameVertex_ThrowsException()
		{
			// Setup/Test
			Line l1 = new Line(1.0, 2.0, 1.0, 2.0);
		}
	}
}
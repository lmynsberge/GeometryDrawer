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
	public class TriangleTests
	{
		[TestMethod()]
		public void Triangle_Ctor1_Success()
		{
			Triangle tri = new Triangle(new List<Vertex>() { new Vertex(0.0, 0.0), new Vertex(1.0, 0.0), new Vertex(0.0, 2.0) });
			Assert.IsTrue(tri != null);

		}

		[TestMethod()]
		public void Triangle_Ctor2_Success()
		{
			Triangle tri = new Triangle(new Vertex(0.0, 0.0), new Vertex(1.0, 0.0), new Vertex(0.0, 2.0));
			Assert.IsTrue(tri != null);
		}

		[TestMethod()]
		public void Triangle_Ctor3_Success()
		{
			Triangle tri = new Triangle(new Line(new Vertex(0.0, 0.0), new Vertex(1.0, 0.0)),
				new Line(new Vertex(0.0, 0.0), new Vertex(0.0, 2.0)),
				new Line(new Vertex(0.0, 2.0), new Vertex(1.0, 0.0)));
			Assert.IsTrue(tri != null);

		}

		[TestMethod()]
		[ExpectedException(typeof(IllDefinedGemoetryException))]
		public void InvalidTriagle_ctor_Failure()
		{
			// Triangles have to have any 2 sides longer than the other
			Triangle tri = new Triangle(new Vertex(0.0, 1.0), new Vertex(0.0, 0.0), new Vertex(0.0, -1.0));
		}

		[TestMethod()]
		public void GetCentroid_Success()
		{
			Vertex v1 = new Vertex(0.0, 1.0);
			Vertex v2 = new Vertex(0.0, 0.0);
			Vertex v3 = new Vertex(1.0, 0.0);
			Triangle tri = new Triangle(v1, v2, v3);
			Assert.AreEqual(new Vertex(1.0 / 3.0, 1.0 / 3.0), tri.GetCentroid());
		}

		[TestMethod()]
		public void GetLines_Success()
		{
			// Setup
			
			Line l1 = new Line(new Vertex(0.0, 1.0), new Vertex(0.0, 0.0));
			Line l2 = new Line(new Vertex(0.0, 1.0), new Vertex(1.0, 0.0));
			Line l3 = new Line(new Vertex(0.0, 0.0), new Vertex(1.0, 0.0));
			Triangle tri = new Triangle(l1, l2, l3);
			bool l1True = false, l2True = false, l3True = false;

			foreach (Line line in tri.GetLines())
			{
				if(line == l1)
				{
					l1True = true;
				}
				else if (line == l2)
				{
					l2True = true;
				}
				else if (line == l3)
				{
					l3True = true;
				}
			}

			Assert.IsTrue(l1True && l2True && l3True);
		}
	}
}
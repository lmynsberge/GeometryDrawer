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
	public class TransformationFactoryTests
	{
		[TestMethod()]
		public void TranslateTest_Success()
		{
			// Setup: Create simple triangle poly (right triangle)
			Triangle tri = new Triangle(new List<Vertex>()
			{
				new Vertex(0.0, 0.0),
				new Vertex(1.0, 0.0),
				new Vertex(0.0, 1.0)
			});

			// Test
			Triangle translatedTri = (Triangle)TransformationFactory.Translate(tri, 1.0, 1.0);

			// Assert: Make sure all vertices are what we would expect
			bool pointC = false;
			bool pointB = false;
			bool pointA = false;
			foreach (Line line in translatedTri.GetLines())
			{
				// This will make sure all three points are the same
				if ((line.V1.X == 1.0 && line.V1.Y == 1.0) || (line.V2.X == 1.0 && line.V2.Y == 1.0))
				{
					pointC = true;
				}

				if (line.V1.X == 2.0 && line.V1.Y == 1.0 || (line.V2.X == 2.0 && line.V2.Y == 1.0))
				{
					pointB = true;
				}

				if (line.V1.X == 1.0 && line.V1.Y == 2.0 || (line.V2.X == 1.0 && line.V2.Y == 2.0))
				{
					pointA = true;
				}
			}
			Assert.IsTrue(pointA && pointB && pointC, "Point A: " + pointA.ToString() + " Point B: " + pointB.ToString() + " Point C: " + pointC.ToString());
		}

		[TestMethod()]
		public void RotateTest_Success()
		{
			// Setup: Create simple triangle poly (right triangle)
			Triangle tri = new Triangle(new List<Vertex>()
			{
				new Vertex(0.0, 0.0),
				new Vertex(1.0, 0.0),
				new Vertex(0.0, 1.0)
			});

			// Test 180deg rotation
			Triangle rotatedTri = (Triangle)TransformationFactory.RotateOnCenter(tri, Math.PI);

			// Assert: Make sure all vertices are what we would expect
			bool pointC = false;
			bool pointB = false;
			bool pointA = false;
			foreach (Line line in rotatedTri.GetLines())
			{
				// This will make sure all three points are the same
				if ((line.V1.X == 0.0 && line.V1.Y == 0.0) || (line.V2.X == 0.0 && line.V2.Y == 0.0))
				{
					pointC = true;
				}

				if (line.V1.X == -1.0 && line.V1.Y == 0.0 || (line.V2.X == -1.0 && line.V2.Y == 0.0))
				{
					pointB = true;
				}

				if (line.V1.X == 0.0 && line.V1.Y == -1.0 || (line.V2.X == 0.0 && line.V2.Y == -1.0))
				{
					pointA = true;
				}
			}
			Assert.IsTrue(pointA && pointB && pointC, "Point A: " + pointA.ToString() + " Point B: " + pointB.ToString() + " Point C: " + pointC.ToString());
		}
	}
}
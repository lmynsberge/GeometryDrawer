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
	public class VertexTests
	{
		[TestMethod()]
		public void SameVertices_AreEqual()
		{
			// Setup/Test
			Vertex v1 = new Vertex(1.0, 2.0);
			Vertex v2 = new Vertex(1.0, 2.0);

			// Assert
			Assert.AreEqual(v1, v2);
		}

		[TestMethod()]
		public void DifferentVertices_AreNotEqual()
		{
			// Setup/Test
			Vertex v1 = new Vertex(2.0, 1.0);
			Vertex v2 = new Vertex(1.0, 2.0);

			// Assert
			Assert.AreNotEqual(v1, v2);
		}
	}
}
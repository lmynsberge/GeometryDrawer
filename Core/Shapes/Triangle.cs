using System.Collections.Generic;

namespace LJM.Geometries.Core
{
	public class Triangle : IPolygon
	{
		#region Private fields
		private List<Line> lines;
		private List<Vertex> vertices;
		#endregion

		#region Constructors
		/// <summary>
		/// Create a triangle from a list of vertices. Errors if not three defined.
		/// </summary>
		/// <param name="vertexList">The list of vertices</param>
		public Triangle(List<Vertex> vertexList)
		{
			if(vertexList == null || vertexList.Count != 3)
			{
				throw new IllDefinedGemoetryException("Vertices must be passed and there can only be 3.");
			}
			Line l1 = new Line(vertexList[0], vertexList[1]);
			Line l2 = new Line(vertexList[0], vertexList[2]);
			Line l3 = new Line(vertexList[1], vertexList[2]);
			ConstructorHelper(l1, l2, l3);
		}
		/// <summary>
		/// Create a triangle from three vertices.
		/// </summary>
		/// <param name="v1">Vertex 1</param>
		/// <param name="v2">Vertex 2</param>
		/// <param name="v3">Vertex 3</param>
		public Triangle(Vertex v1, Vertex v2, Vertex v3)
		{
			// Check for bad input
			if ((v1 == v2) || (v2 == v3) || (v1 == v3))
			{
				throw new IllDefinedGemoetryException("Vertices can't be the same");
			}

			// Create lines
			Line l1 = new Line(v1, v2);
			Line l2 = new Line(v1, v3);
			Line l3 = new Line(v2, v3);

			ConstructorHelper(l1, l2, l3);
		}
		/// <summary>
		/// Creates a triangle from three lines.
		/// </summary>
		/// <param name="l1">Line 1</param>
		/// <param name="l2">Line 2</param>
		/// <param name="l3">Line 3</param>
		public Triangle(Line l1, Line l2, Line l3)
		{
			Line newL1 = new Line(l1.V1, l1.V2);
			Line newL2 = new Line(l2.V1, l2.V2);
			Line newL3 = new Line(l3.V1, l3.V2);
			ConstructorHelper(newL1, newL2, newL3);
		}
		#endregion

		#region ~Fields from the 
		/// <summary>
		/// Defines the lines that construct the triangle. If setting, reevaluates that the lines are valid
		/// </summary>
		public List<Line> GetLines()
		{
			return this.lines;
		}
		/// <summary>
		/// Sets lines into a polygon after validatin
		/// </summary>
		/// <param name="lineList">List of lines</param>
		public void SetLines(List<Line> lineList)
		{
			if(lineList == null || lineList.Count != 3)
			{
				throw new IllDefinedGemoetryException("Must have 3 lines to create a triangle.");
			}
			Line l1 = new Line(lineList[0].V1, lineList[0].V2);
			Line l2 = new Line(lineList[1].V1, lineList[1].V2);
			Line l3 = new Line(lineList[2].V1, lineList[2].V2);
			ConstructorHelper(l1, l2, l3);
		}
		/// <summary>
		/// Makes a copy of the triangle object
		/// </summary>
		/// <param name="poly">The input Triangle object</param>
		/// <returns>The output triangle object</returns>
		public IPolygon GetCopy(IPolygon poly)
		{
			return new Triangle(poly.GetLines()[0], poly.GetLines()[1], poly.GetLines()[2]);
		}
		/// <summary>
		/// Gets all the vertices
		/// </summary>
		/// <returns>List of vertices</returns>
		public List<Vertex> GetVertices()
		{
			return this.vertices;
		}
		/// <summary>
		/// Defines the centroid of a triangle
		/// </summary>
		/// <returns>The centroid of the triangle</returns>
		public Vertex GetCentroid()
		{
			if (this.vertices == null)
			{
				return null;
			}
			// X & Y coordinates are just the average of all vertices
			double centroidX = (this.vertices[0].X + this.vertices[1].X + this.vertices[2].X) / 3.0;
			double centroidY = (this.vertices[0].Y + this.vertices[1].Y + this.vertices[2].Y) / 3.0;

			return new Vertex(centroidX, centroidY);
		}
		#endregion

		#region methods
		private void ConstructorHelper(Line l1, Line l2, Line l3)
		{
			if (!IsValidTriangle(l1, l2, l3))
			{
				throw new IllDefinedGemoetryException("Values do not constitue a valid triangle.");
			}
			this.lines = new List<Line>() { l1, l2, l3 };

			// Store vertices
			Vertex v1 = l1.V1;
			Vertex v2 = l1.V2;
			Vertex v3 = null;
			if (l2.V1 != l1.V1 && l2.V1 != l1.V2)
			{
				v3 = l2.V1;
			}
			else if (l2.V2 != l1.V1 && l2.V2 != l1.V2)
			{
				v3 = l2.V2;
			}
			this.vertices = new List<Vertex>() { v1, v2, v3 };
		}

		/// <summary>
		/// Ensures that the triangle is valid while it is being constructed
		/// </summary>
		/// <param name="l1">First line</param>
		/// <param name="l2">Second line</param>
		/// <param name="l3">Third line</param>
		/// <returns>True if a triangle is value, otherwise false</returns>
		public static bool IsValidTriangle(Line l1, Line l2, Line l3)
		{
			if( (l1.Length + l2.Length) <= l3.Length || (l2.Length + l3.Length) <= l1.Length || (l1.Length + l3.Length) <= l2.Length)
			{
				return false;
			}
			return true;
		}
		#endregion
	}
}

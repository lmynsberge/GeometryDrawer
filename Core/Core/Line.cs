using System;

namespace LJM.Geometries.Core
{
	/// <summary>
	/// A class defining a line
	/// </summary>
	public class Line
	{
		#region Constructors
		/// <summary>
		/// Constructor to construct a line from four coordinates, the first two are for one point
		/// and the second two for the next. Must not be the same point.
		/// </summary>
		/// <param name="x1">Point 1 x-coordinate</param>
		/// <param name="y1">Point 1 y-coordinate</param>
		/// <param name="x2">Point 2 x-coordinate</param>
		/// <param name="y2">Point 2 y-coordinate</param>
		public Line(double x1, double y1, double x2, double y2)
		{
			Vertex v1 = new Vertex(x1, y1);
			Vertex v2 = new Vertex(x2, y2);
			ConstructorHelper(v1, v2);
		}
		/// <summary>
		/// Constructor to construct a line from two vertices. Must not be the same vertex
		/// </summary>
		/// <param name="v1">The first vertex</param>
		/// <param name="v2">Tje second vertex</param>
		public Line(Vertex v1, Vertex v2)
		{
			Vertex newV1 = new Vertex(v1.X, v1.Y);
			Vertex newV2 = new Vertex(v2.X, v2.Y);
			ConstructorHelper(newV1, newV2);
		}
		#endregion

		#region Fields
		public Vertex V1 { get; private set; }
		public Vertex V2 { get; private set; }
		public double Length { get; private set; }
		#endregion

		#region Available Methods
		/// <summary>
		/// Returns the length of a line.
		/// </summary>
		/// <param name="x1">Point 1 x-coordinate</param>
		/// <param name="y1">Point 1 y-coordinate</param>
		/// <param name="x2">Point 2 x-coordinate</param>
		/// <param name="y2">Point 2 y-coordinate</param>
		/// <returns>Legnth with same "units" as the coordinates</returns>
		public static double GetLength(double x1, double x2, double y1, double y2)
		{
			return Math.Sqrt(Math.Pow(y2 - y1, 2.0) + Math.Pow(x2 - x1, 2.0));
		}
		private void ConstructorHelper(Vertex v1, Vertex v2)
		{
			if ((v1.X == v2.X) && (v1.Y == v2.Y))
			{
				throw new IllDefinedGemoetryException("Vertices can't be the same");
			}

			this.V1 = v1;
			this.V2 = v2;
			this.Length = Line.GetLength(v1.X, v2.X, v1.Y, v2.Y);
		}
		#endregion

		#region Operator Overrides
		// It's an immutable object so these operators should be set
		public override bool Equals(object obj)
		{
			// Equals has to return false for null
			if (obj == null)
			{
				return false;
			}
			// Try to cast
			return this.Equals((Line)obj);
		}
		public bool Equals(Line l)
		{
			// Equals has to return false for null
			if ((object)l == null)
			{
				return false;
			}
			// Otherwise points have to be the same
			return (V1 == l.V1) && (V2 == l.V2);
		}
		public override int GetHashCode()
		{
			return V1.GetHashCode() * V2.GetHashCode();
		}
		public static bool operator ==(Line l1, Line l2)
		{
			if (ReferenceEquals(l1, l2))
			{
				return true;
			}

			// check if one is null
			if (((object)l1 == null) || ((object)l2 == null))
			{
				return false;
			}

			// Otherwise call previous definition
			return l1.Equals(l2);
		}
		public static bool operator !=(Line l1, Line l2)
		{
			return !(l1 == l2);
		}
		#endregion
	}
}

using System;

namespace LJM.Geometries.Core
{
	/// <summary>
	/// A class defining a vertex
	/// </summary>
    public class Vertex
    {
		#region Private fields
		#endregion

		#region Constructors
		public Vertex(double x, double y)
		{
			this.X = x;
			this.Y = y;
		}
		#endregion

		#region Fields
		public double X { get; private set; }
		public double Y { get; private set; }
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
			return this.Equals((Vertex)obj);
		}
		public bool Equals(Vertex v)
		{
			// Equals has to return false for null
			if ((object)v == null)
			{
				return false;
			}
			// Otherwise points have to be the same
			return (X == v.X) && (Y == v.Y);
		}
		public override int GetHashCode()
		{
			return (int)Math.Pow(X, Y);
		}
		public static bool operator ==(Vertex v1, Vertex v2)
		{
			if (ReferenceEquals(v1, v2))
			{
				return true;
			}

			// check if one is null
			if (((object)v1 == null) || ((object)v2 == null))
			{
				return false;
			}

			// Otherwise call previous definition
			return v1.Equals(v2);
		}
		public static bool operator !=(Vertex v1, Vertex v2)
		{
			return !(v1 == v2);
		}
		#endregion
	}
}

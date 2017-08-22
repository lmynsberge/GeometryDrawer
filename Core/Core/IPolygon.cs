using System.Collections.Generic;

namespace LJM.Geometries.Core
{
	/// <summary>
	/// Basic interface to define a polygon
	/// </summary>
	public interface IPolygon
	{
		/// <summary>
		/// Centroid is a required definition of a polygon and polygons can vary their definition of center 
		/// </summary>
		/// <returns>A vertex defining the center point of a polygon</returns>
		Vertex GetCentroid();

		/// <summary>
		/// A field that defines the list of lines composing the polygon
		/// </summary>
		List<Line> GetLines();

		/// <summary>
		/// A field that defines the list of lines composing the polygon
		/// </summary>
		void SetLines(List<Line> lines);

		/// <summary>
		/// A field that defines the list of vertices composing the polygon
		/// </summary>
		List<Vertex> GetVertices();

		/// <summary>
		/// Used to create a deep copy
		/// </summary>
		/// <param name="polygon">Polygon to copy</param>
		/// <returns></returns>
		IPolygon GetCopy(IPolygon polygon);
	}
}
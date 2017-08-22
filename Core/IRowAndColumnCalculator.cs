using System;

namespace LJM.Geometries.Core
{
	/// <summary>
	/// Defines an interface to retrieve a row and column from 
	/// </summary>
	public interface IRowAndColumnCalculator
	{
		Tuple<string, int> GetRowAndColumn(Vertex v1, Vertex v2, Vertex v3);
	}
}
using System.Collections.Generic;

namespace LJM.Geometries.Core
{
	public interface IRepeater
	{
		List<Line> GetAllLines();
		int GetCompositeHeight();
		int GetCompositeWidth();
		int GetXSubIndexCount();
		int GetXSubIndex(Vertex centroid);
		int GetYSubIndexCount();
		int GetYSubIndex(Vertex centroid);
	}
}
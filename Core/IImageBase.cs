using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJM.Geometries.Core
{
	/// <summary>
	/// Defines the requirements to be able to draw an image of polygons
	/// </summary>
	public interface IImageBase
	{
		int GetPixelWidth();
		int GetPixelHeight();
		void SetPixelWidth(int pixels);
		void SetPixelHeight(int pixels);
		List<IPolygon> GetPolygonList();
	}
}

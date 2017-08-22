using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LJM.Geometries.Core
{
	/// <summary>
	/// A class to create an image from a list of polygons
	/// </summary>
	public class KaleidoscopeImage : IImageBase
	{
		private int _pixelHeight;
		private int _pixelWidth;
		private List<IPolygon> _polyList;
		private List<Vertex> _centroidList;

		/// <summary>
		/// Constructor that expects polygons to be within the confines of the width/height specified
		/// </summary>
		/// <param name="polygons">List of polygons</param>
		/// <param name="width">The width of the image</param>
		/// <param name="height">The height of the image</param>
		/// <remarks>
		///		Developer's note, normally would add checking to ensure bounding box, but seemed overkill for the time being
		/// </remarks>
		public KaleidoscopeImage(List<IPolygon> polygons, int width, int height)
		{
			this._polyList = polygons;
			this._pixelHeight = height;
			this._pixelWidth = width;
			this._centroidList = OrderPolygons();
		}

		#region implementing abstract fields
		public int GetPixelHeight()
		{
			return this._pixelHeight;
		}

		public int GetPixelWidth()
		{
			return this._pixelWidth;
		}

		public void SetPixelHeight(int pixels)
		{
			this._pixelHeight = pixels;
		}

		public void SetPixelWidth(int pixels)
		{
			this._pixelWidth = pixels;
		}

		public List<IPolygon> GetPolygonList()
		{
			return this._polyList;
		}
		#endregion

		#region methods
		private List<Vertex> OrderPolygons()
		{
			List<Vertex> centroid = new List<Vertex>();

			foreach(IPolygon poly in this._polyList)
			{
				centroid.Add(poly.GetCentroid());
			}
			centroid.Sort(comparer);

			return centroid;
		}

		private int comparer(Vertex v1, Vertex v2)
		{
			if (v2 == null)
			{
				return 1;
			}
			if (v1 == null)
			{
				return -1;
			}

			// If v1 is greater, it should come first (<0)
			return (int)(v2.X - v1.X);
		}
		#endregion

	}
}

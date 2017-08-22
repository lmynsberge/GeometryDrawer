using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LJM.Geometries.Core
{
	/// <summary>
	/// Repeats a Kaledioscope image a set amount of times and tracks/calculates locations of a specific repeated image
	/// </summary>
	public class KaledioscopeRepeater : IRowAndColumnCalculator, IRepeater
	{
		private KaleidoscopeImage _baseImage;
		private int _rowNumbers;
		private int _colNumbers;

		public KaleidoscopeImage BaseImage
		{
			get { return _baseImage; }
			private set { _baseImage = value; }
		}

		/// <summary>
		/// Constructs a repeated image
		/// </summary>
		/// <param name="image">image to repeat</param>
		/// <param name="rowsToCreate"># of rows of images</param>
		/// <param name="colsToCreate"># of columns of images</param>
		/// <remarks>
		/// Note there's not actually a need to construct the image, but rather just a way to track the properties of the image
		/// </remarks>
		public KaledioscopeRepeater(KaleidoscopeImage image, string rowsToCreate, int colsToCreate)
		{
			this.BaseImage = image;
			this._rowNumbers = AlphaToIntConverter(rowsToCreate);
			this._colNumbers = colsToCreate / this.GetXSubIndexCount();
		}



		/// <summary>
		/// Converts an "Excel-style" alpha index to an integer index
		/// </summary>
		/// <param name="alpha">lowercase or uppercase letters</param>
		/// <returns>integer-0 if invalid content</returns>
		public static int AlphaToIntConverter(string alpha)
		{
			Regex r = new Regex("^[a-zA-Z0-9]*$");
			if (!r.IsMatch(alpha))
			{
				return 0;
			}
			int finalValue = 0;

			// Look at each character to get its ASCII value
			for(int character = 0; character < alpha.Length; character++)
			{
				// Convert to upper to get a uniform value
				char upperChar = char.ToUpper(alpha[character]);
				finalValue *= 26;
				// "A" is 65, so set that as 0 and then treat us as base-26
				finalValue += ((int)upperChar - 65) + 1;
			}

			return finalValue;
		}

		/// <summary>
		/// Converts an integer to an "Excel-style" alpha character string
		/// </summary>
		/// <param name="number">integer</param>
		/// <returns>Uppercased string of letters</returns>
		public static string IntToAlphaConverter(int number)
		{
			string finalValue = "";

			int piece;

			while(number > 0)
			{
				piece = (number - 1) % 26;
				finalValue = Convert.ToChar(65 + piece).ToString() + finalValue;
				number = (int)((number - piece) / 26);
			}

			return finalValue;
		}

		/// <summary>
		/// Returns the row and column based on three vertices in the image. This assumes the lower-left of the image
		/// starts at (0, 0).
		/// </summary>
		/// <param name="v1">Vertex 1</param>
		/// <param name="v2">Vertex 2</param>
		/// <param name="v3">Vertex 3</param>
		/// <returns></returns>
		public Tuple<string, int> GetRowAndColumn(Vertex v1, Vertex v2, Vertex v3)
		{
			int column;
			string row;

			// Create triangle - takes care of throwing an exception too if the vertices are garbage
			Triangle triangle = new Triangle(v1, v2, v3);

			// Find out how many repeated images we are across and the leftovers
			int baseY = (int)triangle.GetCentroid().Y / this.BaseImage.GetPixelHeight(); // Y should just be gotten from this + 1
			int baseX = (int)triangle.GetCentroid().X / this.BaseImage.GetPixelWidth(); // X can be more so look at the remainder

			// Within the "leftovers" find out where we are at
			Vertex newCentroid = new Vertex(triangle.GetCentroid().X - baseX, triangle.GetCentroid().Y - baseY);
			int remX = this.GetXSubIndex(newCentroid);
			// Need to reverse "Y" since the input is from lower-left, but this looks from upper-left
			row = IntToAlphaConverter(this.GetCompositeHeight() - (baseY));
			column = baseX * this.GetXSubIndexCount() + remX;

			Tuple<string, int> rowColumn = new Tuple<string, int>(row, column);
			return rowColumn;
		}

		/// <summary>
		/// From a centroid returns the sub index on the X point. Rounds to find it.
		/// </summary>
		/// <param name="centroid">The centroid to find within the image</param>
		/// <returns>0 if not contained, otherwise the integer where it's contained</returns>
		public int GetXSubIndex(Vertex centroid)
		{
			// Guarantee order and find out where we are
			for (int i = 0; i < this.BaseImage.GetPolygonList().Count; i++)
			{
				if (Math.Round(this.BaseImage.GetPolygonList()[i].GetCentroid().X,5) == Math.Round(centroid.X,5))
				{
					return i + 1;
				}
			}
			return 0;
		}
		/// <summary>
		/// Tells how many further X-pieces this image can be split into.
		/// </summary>
		/// <returns>How many sub indices are in one image</returns>
		public int GetXSubIndexCount()
		{
			return this.BaseImage.GetPolygonList().Count;
		}
		/// <summary>
		/// From a centroid returns the sub index on the Y point 
		/// </summary>
		/// <param name="centroid">The centroid to find within the image</param>
		/// <returns>0 if null, otherwise 1</returns>
		public int GetYSubIndex(Vertex centroid)
		{
			if (centroid == null)
			{
				return 0;
			}
			return 1;
		}
		/// <summary>
		/// Tells how many further Y-pieces this image can be split into.
		/// </summary>
		/// <returns>Always 1 for Kaledioscope images</returns>
		public int GetYSubIndexCount()
		{
			return 1;
		}

		public List<Line> GetAllLines()
		{
			List<Line> allLines = new List<Line>();
			for(int row = 0; row < this._rowNumbers; row++)
			{
				for (int column = 0; column < this._colNumbers; column++)
				{
					foreach(IPolygon poly in this._baseImage.GetPolygonList())
					{
						IPolygon newPoly = TransformationFactory.Translate(poly, 
							Convert.ToDouble(column * this._baseImage.GetPixelWidth()), 
							Convert.ToDouble(row * this._baseImage.GetPixelHeight()));
						allLines.AddRange(newPoly.GetLines());
					}
				}
			}
			return allLines;
		}

		public int GetCompositeHeight()
		{
			return this._rowNumbers * this._baseImage.GetPixelHeight();
		}

		public int GetCompositeWidth()
		{
			return this._colNumbers * this._baseImage.GetPixelWidth();
		}
	}
}

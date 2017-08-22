using System;
using System.Collections.Generic;

namespace LJM.Geometries.Core
{
	public static class TransformationFactory
	{
		/// <summary>
		/// Translates a point by a specified by amount
		/// </summary>
		/// <param name="poly">Any polygon</param>
		/// <param name="moveX">How many units to move the polygon in the x-direction</param>
		/// <param name="moveY">How many units to move the polygon in the y-direction</param>
		/// <returns>A translated polygon</returns>
		public static IPolygon Translate(IPolygon poly, double moveX, double moveY)
		{
			IPolygon newPoly = poly.GetCopy(poly);
			List<Line> newLines = new List<Line>();
			foreach(Line line in poly.GetLines())
			{
				newLines.Add(new Line(line.V1.X + moveX, line.V1.Y + moveY, line.V2.X + moveX, line.V2.Y + moveY));
			}

			newPoly.SetLines(newLines);

			return newPoly;
		}

		/// <summary>
		/// Reflects a point about an axis
		/// </summary>
		/// <param name="poly">Any polygon</param>
		/// <param name="axis">An enumerated axis definition</param>
		/// <returns>A reflected polygon</returns>
		public static IPolygon Reflect(IPolygon poly, ReflectionAxes axis)
		{
			IPolygon newPoly = poly.GetCopy(poly); 
			List<Line> newLines = new List<Line>();

			foreach (Line line in poly.GetLines())
			{
				if (axis == ReflectionAxes.Xaxis)
				{
					newLines.Add(new Line(line.V1.X, -(line.V1.Y), line.V2.X, -(line.V2.Y)));
				}
				else if (axis == ReflectionAxes.Yaxis)
				{
					newLines.Add(new Line(-(line.V1.X), line.V1.Y, -(line.V2.X), line.V2.Y));
				}
			}

			newPoly.SetLines(newLines);

			return newPoly;
		}

		/// <summary>
		/// Rotates a polygon based on an amount of radians
		/// </summary>
		/// <param name="poly">Any polygon</param>
		/// <param name="moveRads">Rotation angle in radians</param>
		/// <returns>A rotated polygon</returns>
		public static IPolygon RotateOnCenter(IPolygon poly, double moveRads)
		{
			return RotateOffCenter(poly, moveRads, new Vertex(0.0, 0.0));
		}

		/// <summary>
		/// Rotates a polygon based on an amount of radians and a point of rotation. Rounds to 10 places offset the rotation
		/// </summary>
		/// <param name="poly">Any polygon</param>
		/// <param name="moveRads">Rotation angle in radians</param>
		/// <param name="rotatePoint">Vertex to rotate around</param>
		/// <returns>A rotated polygon</returns>
		public static IPolygon RotateOffCenter(IPolygon poly, double moveRads, Vertex rotatePoint)
		{
			IPolygon newPoly = poly.GetCopy(poly); 
			List<Line> newLines = new List<Line>();

			// Rotate around the 0 axis regardless
			foreach (Line line in poly.GetLines())
			{
				newLines.Add(new Line(
					Math.Round(line.V1.X * Math.Cos(moveRads) - line.V1.Y * Math.Sin(moveRads)),
					Math.Round(line.V1.X * Math.Sin(moveRads) + line.V1.Y * Math.Cos(moveRads)),
					Math.Round(line.V2.X * Math.Cos(moveRads) - line.V2.Y * Math.Sin(moveRads)),
					Math.Round(line.V2.X * Math.Sin(moveRads) + line.V2.Y * Math.Cos(moveRads))
					));
			}

			newPoly.SetLines(newLines);

			// Move the points based on the input rotation point
			return Translate(newPoly, rotatePoint.X, rotatePoint.Y);

		}
	}

	/// <summary>
	/// Defines what axis a 2D reflection should be made off of
	/// </summary>
	public enum ReflectionAxes
	{
		Xaxis,
		Yaxis
	};
}

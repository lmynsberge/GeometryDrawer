using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using LJM.Geometries.Core;
using System.IO;

namespace LJM.Geometries.Drawing
{
	/// <summary>
	/// Quick kaleidoscope repeater. Could create the drawer into an abstract class and have this be an implementation
	/// </summary>
    public class KaleidoscopeDrawer
    {
		private KaledioscopeRepeater _repeater;

		public KaleidoscopeDrawer(KaledioscopeRepeater repeater)
		{
			this._repeater = repeater;
		}
		public Bitmap GetImage()
		{
			Bitmap bmp = new Bitmap(this._repeater.GetCompositeWidth()+1, this._repeater.GetCompositeHeight()+1);
			using (Graphics graphic = Graphics.FromImage(bmp))
			{
				DrawImage(graphic);
			}

			return bmp;
		}
		private void DrawImage(Graphics graphic)
		{
			foreach (Line line in this._repeater.GetAllLines())
			{
				graphic.DrawLine(Pens.Black, 
					new Point(Convert.ToInt32(line.V1.X), Convert.ToInt32(line.V1.Y)), 
					new Point(Convert.ToInt32(line.V2.X), Convert.ToInt32(line.V2.Y)));
			}
		}
		public void SaveImage(Bitmap bmp, string fileName)
		{
			if (string.IsNullOrWhiteSpace(fileName))
			{
				return;
			}
			bmp.Save(fileName, System.Drawing.Imaging.ImageFormat.Png);
			bmp.Dispose();
		}
	}
}

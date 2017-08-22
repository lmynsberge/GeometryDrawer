using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using LJM.Geometries.Drawing;
using LJM.Geometries.Core;
using System.Drawing;
using System.IO;
using System.Web.Routing;

namespace LJM.Geometries.Services.Controllers
{
    public class ImageController : ApiController
    {
		/// <summary>
		/// Quick controller for fun image display. Better to split it out if more is needed in the future.
		/// </summary>
		/// <param name="rows">Number of rows to create</param>
		/// <param name="columns">Number of columns to create</param>
		/// <returns>An image and 200 response</returns>
		/// <remarks>
		///		The call is http://yourserverhere/api/Image?row=r&column=c. For example, from the sample:
		///		http://localhost:64876/api/Image?row=F&column=12 
		/// </remarks>
		public HttpResponseMessage Get(string row, int column)
		{

			Triangle tri1 = new Triangle(new Vertex(0.0, 0.0), new Vertex(10.0, 0.0), new Vertex(0.0, 10.0));
			Triangle tri2 = (Triangle)TransformationFactory.Translate(TransformationFactory.RotateOnCenter(tri1, Math.PI), 10.0, 10.0);
			KaleidoscopeImage image = new KaleidoscopeImage(new List<IPolygon>() { tri1, tri2 }, 10, 10);
			KaledioscopeRepeater repeater = new KaledioscopeRepeater(image, row, column);
			KaleidoscopeDrawer drawer = new KaleidoscopeDrawer(repeater);
			Bitmap bmp = drawer.GetImage();

			using (MemoryStream ms = new MemoryStream())
			{
				bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
				HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
				response.Content = new ByteArrayContent(ms.ToArray());
				response.Content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
				return response;
			}
		}
    }
}

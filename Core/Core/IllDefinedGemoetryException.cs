using System;

namespace LJM.Geometries.Core
{
	[Serializable]
	public class IllDefinedGemoetryException : Exception
	{
		/// <summary>
		/// Basic exception to call out bad geometry requests
		/// </summary>
		public IllDefinedGemoetryException()
		{
		}

		/// <summary>
		/// Basic exception to call out bad geometry requests with a message
		/// </summary>
		/// <param name="message">Message with the exception</param>
		public IllDefinedGemoetryException(string message) : base(message)
		{
		}
	}
}
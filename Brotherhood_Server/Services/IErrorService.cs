using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Brotherhood_Server.Services
{
	public interface IErrorService
	{
		/// <summary>
		/// Really super overkill convenience method for sending pretty error responses.
		/// </summary>
		/// <param name="code">The status code.</param>
		/// <param name="message">The error message. Will be set to a Message property in the error object.</param>
		/// <returns></returns>
		public ObjectResult Response([ActionResultStatusCode] int code, string message);
	}
}

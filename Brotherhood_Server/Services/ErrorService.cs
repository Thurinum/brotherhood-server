using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Linq;
using System.Reflection;
using System;
using System.Text.RegularExpressions;

namespace Brotherhood_Server.Services
{
	public class ErrorService : IErrorService
	{
		public ObjectResult Response([ActionResultStatusCode] int statusCode, string message)
		{
			string statusCodeStr = statusCode.ToString();
			FieldInfo field = typeof(StatusCodes).GetFields(BindingFlags.Public | BindingFlags.Static).FirstOrDefault(f => f.Name.Contains(statusCodeStr));

			if (field == null)
				throw new ArgumentException("Invalid status code.", nameof(statusCode));

			string statusName = Regex.Replace(field.Name.Split(statusCodeStr)[1], "[A-Z]", " $0").Trim();

			return new ObjectResult(new
			{
				Message = message,
				StatusName = statusName
			})
			{
				StatusCode = statusCode
			}; ;
		}
	}
}

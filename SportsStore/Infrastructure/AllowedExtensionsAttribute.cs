using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Infrastructure {
	public class AllowedExtensionsAttribute : ValidationAttribute, IClientModelValidator {
		private readonly string _extensions;
		public AllowedExtensionsAttribute(string extensions)
		{
			_extensions = extensions;
		}

		protected override ValidationResult IsValid(
		object value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file != null)
			{
				var extension = Path.GetExtension(file.FileName);
				if (!_extensions.Contains(extension.ToLower()))
				{
					return new ValidationResult(GetErrorMessage());
				}
			}

			return ValidationResult.Success;
		}

		public string GetErrorMessage()
		{
			return "Sorry, but the file you attempted to upload isn't allowed.";
		}

		/// <summary>
		/// This funciton adds attributes to the front end in this case
		/// data-val, data-val-allowedextensions, and data-val-allowedextensions-extensions
		/// 
		/// </summary>
		/// <param name="context"></param>
		public void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-allowedextensions", GetErrorMessage());
			MergeAttribute(context.Attributes, "data-val-allowedextensions-extensions", _extensions);
		}
		/// <summary>
		/// Helper method to add attributes to the front-end
		/// </summary>
		/// <param name="attributes">the front end dictionary</param>
		/// <param name="key">key to add</param>
		/// <param name="value">value to add</param>
		/// <returns></returns>
		private bool MergeAttribute(IDictionary<string, string> attributes, string key, string value)
		{
			if (attributes.ContainsKey(key))
			{
				return false;
			}
			attributes.Add(key, value);
			return true;
		}
	}
}

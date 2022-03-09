using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Infrastructure {
	public class MaxFileSizeAttribute : ValidationAttribute, IClientModelValidator {

		/// <summary>
		/// Set to 5 mb 
		/// </summary>
		private readonly int _maxFileSize = 5120000;
		/// <summary>
		/// Uses default value
		/// </summary>
		public MaxFileSizeAttribute()
		{
		}

		/// <summary>
		/// Custom value for max file size
		/// </summary>
		/// <param name="maxFileSize"></param>
		public MaxFileSizeAttribute(int maxFileSize)
		{
			_maxFileSize = maxFileSize;
		}

		protected override ValidationResult IsValid(
		object value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file != null)
			{
				if (file.Length > _maxFileSize)
				{
					return new ValidationResult(GetErrorMessage());
				}
			}

			return ValidationResult.Success;
		}

		public string GetErrorMessage()
		{
			return $"Maximum allowed file size is { _maxFileSize} bytes.";
		}

		public void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-maxfilesize", GetErrorMessage());
			MergeAttribute(context.Attributes, "data-val-maxfilesize-maxfilesize", _maxFileSize.ToString());
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
	/// <summary>
	/// used on a IFormFile as a way to limit the max size
	/// </summary>
	public class MinFileSizeAttribute : ValidationAttribute, IClientModelValidator {

		/// <summary>
		/// Set to 5 mb 
		/// </summary>
		private readonly int _minFileSize = 1;
		/// <summary>
		/// Uses default value
		/// </summary>
		public MinFileSizeAttribute()
		{
		}

		/// <summary>
		/// Custom value for max file size
		/// </summary>
		/// <param name="maxFileSize"></param>
		public MinFileSizeAttribute(int minFileSize)
		{
			_minFileSize = minFileSize;
		}

		protected override ValidationResult IsValid(
		object value, ValidationContext validationContext)
		{
			var file = value as IFormFile;
			if (file != null)
			{
				if (file.Length < _minFileSize)
				{
					return new ValidationResult(GetErrorMessage());
				}
			}

			return ValidationResult.Success;
		}

		public string GetErrorMessage()
		{
			return $"Minimum allowed file size is { _minFileSize} byte(s).";
		}

		public void AddValidation(ClientModelValidationContext context)
		{
			MergeAttribute(context.Attributes, "data-val", "true");
			MergeAttribute(context.Attributes, "data-val-minfilesize", GetErrorMessage());
			MergeAttribute(context.Attributes, "data-val-minfilesize-minfilesize", _minFileSize.ToString());
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

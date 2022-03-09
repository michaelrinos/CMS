using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CMS.Infrastructure {
	[AttributeUsage(AttributeTargets.Property)]
	public class RequiredIfAttribute : ValidationAttribute, IClientModelValidator {
		private const string defaultErrorMessage = "The {0} field is required.";

		private string PropertyName { get; set; }
		private Object DesiredValue { get; set; }
		private readonly RequiredAttribute _innerAttribute;


		public RequiredIfAttribute(string propertyName, object DesiredValue, string ErrorMessage = defaultErrorMessage)
		{
			PropertyName = propertyName;
			this.DesiredValue = DesiredValue;
			ErrorMessage = ErrorMessage; //used if error message is not set on attribute itself
			_innerAttribute = new RequiredAttribute();

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="value">Value refers to the actual value from the field that your custom validator is validating.</param>
		/// <param name="context"></param>
		/// <returns></returns>
		protected override ValidationResult IsValid(object value, ValidationContext context)
		{
			var dependentValue = context.ObjectInstance.GetType().GetProperty(PropertyName).GetValue(context.ObjectInstance, null);

			if (dependentValue.ToString() == DesiredValue.ToString())
			{
				if (!_innerAttribute.IsValid(value))
				{
					return new ValidationResult(FormatErrorMessage(context.DisplayName), new[] { context.MemberName });
				}
			}
			return ValidationResult.Success;
		}

		/// <summary>
		/// This funciton adds attributes to the front end in this case
		/// data-val and data-val-requirediftrue
		/// 
		/// </summary>
		/// <param name="context"></param>
		public void AddValidation(ClientModelValidationContext context)
		{

			MergeAttribute(context.Attributes, "data-val", "true");
			var errorMessage = FormatErrorMessage(context.ModelMetadata.GetDisplayName());
			MergeAttribute(context.Attributes, "data-val-requiredif", errorMessage);
			MergeAttribute(context.Attributes, "data-val-requiredif-dependentproperty", PropertyName);
			MergeAttribute(context.Attributes, "data-val-requiredif-desiredvalue", DesiredValue is bool ? DesiredValue.ToString().ToLower() : DesiredValue.ToString());

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

using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SportsStore.Models {
    public class User : IdentityUser {
		/*
        public int? UserId { get; set; }

        public Guid UserGuid { get; set; }
		public string Username { get; set; }


		public bool Active { get; set; }

		[Required]
		[EmailAddress]
		public string Email { get; set; }

		string _displayName;

		[Required]
		[DisplayName("First Name")]
		[RegularExpression(@"^([^\x00-\x7F]|[A-Za-z-. ']){1,50}$", ErrorMessage = "Invalid first name")]
		public string DisplayName
		{
			get {
				return _displayName;
			}
			set {
				_displayName = _scrubExtraChars(value);
			}
		}
		string _scrubExtraChars(string value)
		{
			if (value == null)
				return null;

			return Regex.Replace(value.Trim(), " +", " ");
		}


		[DisplayName("Last Name")]
        [RegularExpression(@"^([^\x00-\x7F]|[A-Za-z-. ']){1,50}$", ErrorMessage = "Invalid last name")]
        public string LastName { get; set; }

		Dictionary<string, string> _details;

		Dictionary<string, string> _existingDetails;
		[JsonIgnore]
		internal Dictionary<string, string> ExistingDetails
		{
			get { return _existingDetails; }
		}


		[JsonIgnore]
		public Dictionary<string, string> Details
		{
			get {
				//lazy load
				if (_details == null)
				{
					_details = new Dictionary<string, string>();

					if (this.UserId.HasValue)
					{
						var details = LoginManager.GetDetails(this);

						foreach (var v in details)
						{
							/* TODO?
							if (v.BinaryValue != null)
								_details.Add(v.Key, v.BinaryValue);
							else
							 */
							/*
							if (_details.ContainsKey(v.Key))
							{
								_details[v.Key] = v.TextValue;
							}
							else
							{
								_details.Add(v.Key, v.TextValue);
							}


						}
					}

					//keep track of details before any mods
					_existingDetails = new Dictionary<string, string>();

					foreach (var d in _details)
						_existingDetails.Add(d.Key, d.Value);
				}

				return _details;
			}
		}

		public bool HasDetail(string key, string value = null)
		{
			if (Details.ContainsKey(key) == false)
				return false;

			if (value == null)
				return true;

			return (Details[key] == value);
		}

		public string GetDetail(string key)
		{
			if (Details.ContainsKey(key) == false)
				return null;

			return (Details[key]);
		}

		public Dictionary<string, string> GetDetailsByKeys(string[] keys)
		{
			if (keys == null || !keys.Any())
				return null;

			return Details.Where((i) => keys.Contains(i.Key)).ToDictionary(i => i.Key, i => i.Value);
		}

		public bool HasMultiDetail(string key, string value = null)
		{
			if (Details.ContainsKey(key) == false)
				return false;

			if (value == null)
				return true;

			return (Details[key].Split(',').Contains(value));
		}
		public bool SetMultiDetail(string key, string value, bool isRemove)
		{
			var ret = false;

			if (String.IsNullOrWhiteSpace(key))
				throw new ArgumentException("Key cannot be null");

			//escape delimiter
			if (value.Contains(","))
				value = value.Replace(",", ";");

			//if we have key in details - get it and update it
			if (this.Details.ContainsKey(key))
			{
				var sm = this.Details[key];

				//removing
				var items = sm.Split(',').ToList();
				if (items.Contains(value) && isRemove)
				{
					items.Remove(value);

					ret = true;
				}
				//add new one
				else if (!items.Contains(value) && !isRemove)
				{
					//fifo
					int count = 0;

					if (key.StartsWith("Last10"))
						count = 10;
					else if (key.StartsWith("Last5"))
						count = 5;

					if (count > 0)
						while (items.Count >= count)
							items.RemoveAt(items.Count - 1);

					items.Insert(0, value);

					ret = true;
				}

				this.Details[key] = String.Join(",", items);
			}
			//if not then create it
			else
			{
				this.Details.Add(key, value);

				ret = true;
			}

			return ret;
		}
		public override bool Equals(object obj)
		{
			if (obj == null || GetType() != obj.GetType())
				return false;

			var login = obj as User;
			return (this.UserId == login.UserId);
		}

		public override int GetHashCode()
		{
			return this.UserId.GetHashCode();
		}
		public override string ToString()
		{
			return LastName + ", " + DisplayName + " [" + UserId + "]";
		}
							*/


	}
}

using System;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.Models
{
	public class PersonalCode
	{
		[Key]
		[StringLength(11)]
		public string Code { get; set; }

	}
}


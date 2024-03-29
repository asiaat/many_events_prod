﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.Models
{
	public class MFeeType
	{
		public int Id { get; set; }
		[Required]
		[StringLength(250)]
		public string Name { get; set; }

        [StringLength(250)]
        public string? Remarks { get; set; }

		public MFeeType()
		{

		}

		public void SetName(string name)
		{
			Name = name;
		}

		public void SetRemarks(string remarks)
		{
			Remarks = remarks;
		}

	}
}


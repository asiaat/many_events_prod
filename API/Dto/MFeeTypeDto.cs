using System;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.API.Dto
{
	public class MFeeTypeDto
	{
        public int Id { get; set; }
        
        public string Name { get; set; }

        public string? Remarks { get; set; }
    }
}


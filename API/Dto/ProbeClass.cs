using System;
using System.ComponentModel.DataAnnotations;
using ManyEvents.Models;

namespace ManyEvents.API.Dto
{
	public class ProbeClass
	{
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCodeAsString { get; set; }
        //public string Remarks { get; set; }
    }
}


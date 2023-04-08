using System;
using System.ComponentModel.DataAnnotations;
using ManyEvents.Models;

namespace ManyEvents.API.Dto
{
	public class MPersonDto
	{
        public int? Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PersonalCodeAsString { get; set; }
        //public string Remarks { get; set; }

        public virtual IList<MEventDto> EventsList { get; set; }
    }
}


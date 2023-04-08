using System;
using ManyEvents.Models;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.API.Dto
{
	public class MPersonDto
	{
        public int Id { get; set; }
        public string FirstName { get; set; }     
        public string LastName { get; set; }
        //public PersonalCode PersonalCode { get; set; }        
        
    }
}


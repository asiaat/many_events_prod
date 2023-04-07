using System;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.Models
{
	public class MUser
	{
        public int Id { get; set; }
        public string Username { get; set; }       
        public string Password { get; set; }
        public int Status { get; set; }
    }
}


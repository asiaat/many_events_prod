using System;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.Models
{
	public class MPerson : MGuest
	{
        [StringLength(50)]
        public string FirstName { get; set; }
        [StringLength(50)]
        public string LastName { get; set; }
		public PersonalCode PersonalCode { get; set; }
        [StringLength(1500)]
        public string Remarks { get; set; }



	}
}


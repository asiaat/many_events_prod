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


        public MPerson()
        {
            Remarks = "";
        }

        public void SetFirstName(string firstName)
        {
            FirstName = firstName;
        }
        public void SetLastName(string lastName)
        {
            LastName = lastName;
        }

        public void SetPersonalCode(string personalCodeAsString)
        {
            PersonalCode = new PersonalCode(personalCodeAsString);
        }
        public void SetFeeType(MFeeType feeType)
        {
            FeeType = feeType;
        }


	}
}


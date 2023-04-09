using System;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.Models
{
	public class MCompany :MGuest
	{
        [StringLength(150)]
        public string JurName { get; set; }
        [StringLength(8)]
        public string RegCode { get; set; }
        public int GuestsCount { get; set; }
        [StringLength(5000)]
        public string Remarks { get; set; }

        public virtual ICollection<MEvent> EventsList { get; set; }

        public MCompany()
        {
            Remarks = "";
            EventsList = new HashSet<MEvent>();
        }
    }
}


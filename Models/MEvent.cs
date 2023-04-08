using System;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.Models
{
	public class MEvent
	{
        
        public int Id { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Place { get; set; }
        public decimal Price { get; set; }

        public MEvent()
        {
            MPersons = new HashSet<MPerson>();
        }

        public virtual ICollection<MPerson> MPersons { get; set; }



    }
}


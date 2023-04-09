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
        public MFeeType EventFeeType { get; set; }

        public MEvent()
        {
            MPersons = new HashSet<MPerson>();
        }

        public virtual ICollection<MPerson> MPersons { get; set; }

        public void SetFeeType(MFeeType feeType)
        {
            EventFeeType = feeType;
        }

        public void SetTitle(string title)
        {
            Title = title;
        }

        public void SetPlace(string place)
        {
            Place = place;
        }

        public void SetPrice(decimal price)
        {
            Price = price;
        }

        public void SetReleaseDate(DateTime dateTime)
        {
            ReleaseDate = dateTime;
        }


    }
}


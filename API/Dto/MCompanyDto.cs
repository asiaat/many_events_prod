using System;
namespace ManyEvents.API.Dto
{
	public class MCompanyDto
	{
        public int? Id { get; set; }
        public string JurName { get; set; }

        public string RegCode { get; set; }
        public int GuestsCount { get; set; }

        public string? Remarks { get; set; }
        public MFeeTypeDto? FeeType { get; set; }

        public virtual IList<MEventDto>? EventsList { get; set; }
    }
}


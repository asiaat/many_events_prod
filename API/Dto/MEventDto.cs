﻿using System;
using System.ComponentModel.DataAnnotations;

namespace ManyEvents.API.Dto
{
	public class MEventDto
	{
        public int Id { get; set; }
        public string? Title { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string? Place { get; set; }
        public decimal Price { get; set; }
    }
}


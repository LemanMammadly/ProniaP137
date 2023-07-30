﻿using System;
using System.ComponentModel.DataAnnotations;

namespace P137Pronia.Models
{
	public class Slider
	{
		public int Id { get; set; }
		[Required]
		public string ImageUrl { get; set; } 
		[Required,MaxLength(50)]
        public string Title { get; set; }
		[Required,MaxLength(200)]
        public string Desc { get; set; } 
		[MaxLength(50)]
		public string? Offer { get; set; } 
        [Required, MaxLength(50)]
        public string ButtonText { get; set; }
    }
}



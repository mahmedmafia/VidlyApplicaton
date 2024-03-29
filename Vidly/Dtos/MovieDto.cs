﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Dtos
{
    public class MovieDto
    {
        public int id { get; set; }

        [Required]
        [StringLength(255)]
        public string Name { get; set; }

        public DateTime ReleaseDate { get; set; }

       
        public DateTime DateAdded { get; set; }

        [Range(1, 20)]
        public int Count { get; set; }

       [Required]
        public GenreDto Genre { get; set; }
    }
}
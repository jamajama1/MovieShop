﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApplicationCore.Entities
{
    [Table("Genre")]
    public class Genre
    {
        public Genre(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; }

        // Navigation Property
        public ICollection<MovieGenre> Movies { get; set; }
    }
}
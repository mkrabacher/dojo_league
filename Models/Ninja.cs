using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using dojo_league.Models;

namespace dojo_league.Models
{
    public class Ninja : Base
    {
        [Required]
        [MinLength(2)]
        public string Name { get; set; }
        [Required]
        [Range(1,10)]
        public int Level { get; set; }
        public string Description { get; set; }

        public Dojo Dojo { get; set; }
        [ForeignKey("Dojo")]
        [Required]
        public int? DojoId { get; set; }
    }
}
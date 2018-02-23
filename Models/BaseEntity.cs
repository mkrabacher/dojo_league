using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace dojo_league.Models
{
    public class Base
    {
        public int Id { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime CreatedAt { get; set; }
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime UpdatedAt { get; set; } 
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using dojo_league.Models;

namespace dojo_league.Models
{
    public class Dojo : Base
    {
        public string Name { get; set; }
        public string Location { get; set; }
        public string Description { get; set; }

        [InverseProperty("Dojo")]
        public List<Ninja> Ninjas { get; set; }

        public Dojo()
        {
            Ninjas = new List<Ninja>();
        }
    }
}
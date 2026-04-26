using System;
using System.Collections.Generic;
using System.Text;
namespace HeroArena.Models
{
    public class Spell
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Damage { get; set; }
        public string Description { get; set; }
    }
}
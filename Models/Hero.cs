using System;
using System.Collections.Generic;
using System.Text;

namespace HeroArena.Models
{
    public class Hero
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Health { get; set; }
        public string ImageURL { get; set; }
    }
}
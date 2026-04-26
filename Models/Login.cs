using System;
using System.Collections.Generic;
using System.Text;

namespace HeroArena.Models
{
    public class Login
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
    }
}
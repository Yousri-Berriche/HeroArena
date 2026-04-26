using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace HeroArena.Models
{
    public class ApplicationDbContext : DbContext
    {
        //ICI on declare les db pour per;etre la connexion entre les classes et la base de donne sql
        public DbSet<Login> Logins { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Hero> Heroes { get; set; }
        public DbSet<Spell> Spells { get; set; }

        //Ici on declare le constructeur de la classe Application pour permettre la creation d'une instanc3e de classe ApplicationDbContext
        public ApplicationDbContext() : base()
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // permet de configurer la connexion a la base de donne sql en local 
            optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=ExerciceHero;Trusted_Connection=true;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

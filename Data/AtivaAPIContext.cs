using Microsoft.EntityFrameworkCore;
using AtivaAPI.Models;
using System;

namespace AtivaAPI.Data
{
    public class AtivaAPIContext : DbContext
    {

        public DbSet<Fundo> Fundos { get; set; }
        public DbSet<Operacao> Operacoes { get; set; }

        public AtivaAPIContext(DbContextOptions<AtivaAPIContext> options) : base(options) { }

    }

}

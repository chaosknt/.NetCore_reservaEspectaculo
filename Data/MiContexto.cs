using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ReservaEspectaculo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ReservaEspectaculo.Data
{
    public class MiContexto : IdentityDbContext<IdentityUser<int>, IdentityRole<int>, int>
    {
        public MiContexto(DbContextOptions<MiContexto> options) : base (options)
        {         
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)

        {

            base.OnModelCreating(modelBuilder);

            //Referencias para customización

            //https://docs.microsoft.com/en-us/aspnet/core/security/authentication/customize-identity-model?view=aspnetcore-3.1

            //Esto es para que les sea más amigable a Uds. La tabla por defecto se llama AspNetUsers

            modelBuilder.Entity<IdentityUser<int>>(builder =>

            {
                builder.ToTable("Usuarios");
            });
            //modelBuilder.Entity<IdentityUser<int>>(builder =>

            //{
            //    builder.ToTable("AspNetRoles");
            //});
        }
        public DbSet<Cliente> Clientes { get; set; }

        public DbSet<Direccion> Direcciones { get; set; }

        public DbSet<Empleado> Empleados { get; set; }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Funcion> Funciones { get; set; }

        public DbSet<Genero> Generos { get; set; }

        public DbSet<Pelicula> Peliculas { get; set; }

        public DbSet<Reserva> Reservas { get; set; }

        public DbSet<Sala> Salas { get; set; }

        public DbSet<TipoSala> TipoSala { get; set; }

        public DbSet<Rol> Rol { get; set; }





    }


}


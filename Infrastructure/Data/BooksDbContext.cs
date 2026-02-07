using MyBooks.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Infrastructure.Data
{
    public class BooksDbContext : DbContext
    {
        public BooksDbContext(DbContextOptions<BooksDbContext> options)
            : base(options)
        {
        }

        public DbSet<LibroRefactor> LibroRefactor { get; set; }
        public DbSet<LibroInput> LibroInput { get; set; }
        public DbSet<LibroAutor> LibroAutor { get; set; }
        public DbSet<Lectura> Lectura { get; set; }
        public DbSet<FuenteExterna> FuenteExterna { get; set; }
        public DbSet<Autor> Autor { get; set; }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            setupLibroRefactorEntity(modelBuilder);
            setupLibroInputEntity(modelBuilder);
            setupLibroAutorEntity(modelBuilder);
            setupAutorEntity(modelBuilder);
            setupLecturaEntity(modelBuilder);
            setupFuenteExternaEntity(modelBuilder);


        }

        private void setupLibroRefactorEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibroRefactor>(entity =>
            {
                entity.HasKey(libro => libro.Id); //PK LibroRefactor

                entity.Property(libro => libro.Titulo)
                .IsRequired()
                .HasMaxLength(200);
            }
            );
        }

        private void setupLibroAutorEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibroAutor>(entity =>
            {
                entity.HasKey(la => new { la.LibroId, la.AutorId }); //Clave primaria compuesta

                // Relación con Libro
                entity.HasOne(la => la.LibroRefactor)
                .WithMany(libro => libro.LibroAutores)
                .HasForeignKey(la => la.LibroId);

                // Relación con Autor
                entity.HasOne(la => la.Autor)
                .WithMany(autor => autor.LibroAutores)
                .HasForeignKey(la => la.AutorId);
            });
        }


        private void setupLecturaEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Lectura>(entity =>
            {
                entity.HasKey(lectura => lectura.Id); //PK Lectura
                entity.Property(lectura => lectura.LibroId)
                .IsRequired();
                entity.Property(lectura => lectura.FechaLectura)
                .IsRequired();

                entity.HasOne(lectura => lectura.LibroRefactor)
                .WithOne(libro => libro.Lectura)
                .HasForeignKey<Lectura>(lectura => lectura.LibroId);
            });
        }

        private void setupFuenteExternaEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<FuenteExterna>(entity =>
            {
                entity.HasKey(fuente => fuente.Id); //PK FuenteExterna
                entity.Property(fuente => fuente.LibroId)
                .IsRequired();
                entity.Property(fuente => fuente.TituloExterno)
                .HasMaxLength(200);
                entity.Property(fuente => fuente.ImagenId)
                .HasMaxLength(200);

                entity.HasOne(fuente => fuente.LibroRefactor)
                .WithOne(libro => libro.FuenteExterna)
                .HasForeignKey<FuenteExterna>(fuente => fuente.LibroId);
            });
        }

        private void setupAutorEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Autor>(entity =>
            {
                entity.HasKey(a => a.Id); //PK Autor
                entity.Property(a => a.Nombre)
                .IsRequired()
                .HasMaxLength(250);

                entity.HasMany(a => a.LibroAutores)
                .WithOne(la => la.Autor)
                .HasForeignKey(la => la.AutorId);
            });
        }

        private void setupLibroInputEntity(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LibroInput>(entity =>
            {
                entity.HasKey(libroIn => libroIn.Id);
                entity.Property(libroIn => libroIn.LibroId)
                .IsRequired();
                entity.Property(libroIn => libroIn.FechaEntrada)
                .IsRequired();
                entity.Property(libroIn => libroIn.NombreArchivo)
                .IsRequired()
                .HasMaxLength(200);

                entity.HasOne(libroIn => libroIn.LibroRefactor)
                .WithOne(libro => libro.LibroInput)
                .HasForeignKey<LibroInput>(libroIn => libroIn.LibroId);

            });

        }
    }
}

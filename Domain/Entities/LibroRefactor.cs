using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBooks.Domain.Entities
{
    public class LibroRefactor
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string Titulo { get; set; }
        public int? AnoPublicacion { get; set; }
        public int? Paginas { get; set; }
       

        // Relaciones
        public ICollection<LibroAutor> LibroAutores { get; set; } = new List<LibroAutor>();
        public FuenteExterna FuenteExterna { get; set; }
        public Lectura Lectura { get; set; }
        // Relación inversa 1-1 con LibroInput
        public LibroInput LibroInput { get; set; }
    }
}

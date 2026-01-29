using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nombre { get; set; }

        // Relación muchos a muchos con LibroRefactor a través de LibroAutor
        public ICollection<LibroAutor> LibroAutores { get; set; } = new List<LibroAutor>();
    }
}

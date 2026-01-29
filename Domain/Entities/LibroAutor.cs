using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class LibroAutor
    {
        public int LibroId { get; set; }
        public int AutorId { get; set; }

        // Relaciones de navegación
        public LibroRefactor LibroRefactor { get; set; }
        public Autor Autor { get; set; }
    }
}

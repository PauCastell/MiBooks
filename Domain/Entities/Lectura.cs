using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Xml;

namespace MyBooks.Domain.Entities
{
    public class Lectura
    {
        public int Id { get; set; }
        [Required]
        public int LibroId { get; set; }
        [Required]
        public DateTime FechaLectura { get; set; }
        public int? Nota { get; set; }

        // Relación 1-1 con LibroRefactor
        public LibroRefactor LibroRefactor { get; set; }
    }
}

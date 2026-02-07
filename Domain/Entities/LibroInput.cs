using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBooks.Domain.Entities
{
    public class LibroInput
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(200)]
        public string NombreArchivo { get; set; }
        [Required]
        public DateTime FechaEntrada { get; set; }
        [Required]
        public int LibroId { get; set; }

        // Realción 1-1 con LibroRefactor
        public LibroRefactor LibroRefactor { get; set; }
    }
}

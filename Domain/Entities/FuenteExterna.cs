using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Domain.Entities
{
    public class FuenteExterna
    {
        public int Id { get; set; }
        [Required]
        public int LibroId { get; set; }
        [MaxLength(200)]
        public string TituloExterno { get; set; }
        [MaxLength(200)]
        public string ImagenId { get; set; }

        // Relación 1-1 con LibroRefactor
        public LibroRefactor LibroRefactor { get; set; }
    }
}

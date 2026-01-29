using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class FuenteExterna
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public string TituloExterno { get; set; }
        public string ImagenId { get; set; }

        // Relación 1-1 con LibroRefactor
        public LibroRefactor LibroRefactor { get; set; }
    }
}

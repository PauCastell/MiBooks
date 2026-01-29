using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Entities
{
    public class LibroInput
    {
        public int Id { get; set; }
        public string NombreArchivo { get; set; }
        public DateTime FechaEntrada { get; set; }
        public int LibroId { get; set; }

        // Realción 1-1 con LibroRefactor
        public LibroRefactor LibroRefactor { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;

namespace Domain.Entities
{
    public class Lectura
    {
        public int Id { get; set; }
        public int LibroId { get; set; }
        public DateTime FechaLectura { get; set; }
        public int Nota { get; set; }

        // Relación 1-1 con LibroRefactor
        public LibroRefactor LibroRefactor { get; set; }
    }
}

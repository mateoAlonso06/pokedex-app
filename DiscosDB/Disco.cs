using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscosDB
{
    internal class Disco
    {
        public string Titulo { get; set; }
        public DateTime Fecha { get; set; }
        public int CantidadCanciones { get; set; }
        public string UrlImagenTapa { get; set; }
        public string Estilo {  get; set; }
        public string Formato { get; set; } 

        // propiedades a utilizar para traer desde mi DB
    }
}

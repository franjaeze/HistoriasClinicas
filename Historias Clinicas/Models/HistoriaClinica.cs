using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Historias_Clinicas.Models
{
    public class HistoriaClinica
    {
        public int Id { get; set; }

        public int PacienteId { get; set; }

        public List<Episodio> Episodios { get; set; }
    }
}

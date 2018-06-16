using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using RegistroConDetalle.Entidades;

namespace RegistroConDetalle.Entidades
{
    public class Grupos
    {
        [Key]
        public int GrupoId { get; set; }
        public String Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int Grupo { get; set; }
        public int Integrantes { get; set; }
        public DateTime Fecha { get; set; }

     //   [StringLength(100)]
     //   public string Comments { get; set; }

        public virtual ICollection<GruposDetalle> Detalle { get; set; }

        public Grupos()
        {
            //Inicializar la Lista
            this.Detalle = new List<GruposDetalle>();
        }

        public void AgregarDetalle(int id, int gruposId, int personasId, string cargo)
        {
            this.Detalle.Add(new GruposDetalle(id, gruposId, personasId, cargo));
        }
    }
}

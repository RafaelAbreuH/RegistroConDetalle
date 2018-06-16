using RegistroConDetalle.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RegistroConDetalle.Entidades
{
    public class GruposDetalle
    {
        public int Id { get; set; }
        public int GruposId { get; set; }
        public int PersonasId { get; set; }

        [ForeignKey("PersonasId")]
        //Permite indicar por cual campo se usara
        //la NAVIGATION PROPERTY
        public virtual Personas Persona { get; set; }
        public string Cargo { get; set; }

        public GruposDetalle()
        {
            this.Id = 0;
            this.GruposId = 0;
        }
        /// <summary>
        /// Este constructor es opcional solo lo agrego por comodidad
        /// </summary> 
        public GruposDetalle(int id, int grupoId, int personaId, string cargo)
        {
            this.Id = id;
            this.GruposId = grupoId;
            this.PersonasId = personaId;
            this.Cargo = cargo;
        }
    }
}

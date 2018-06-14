using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using RegistroConDetalle.DAL;
using RegistroConDetalle.Entidades;

namespace RegistroConDetalle.BLL
{
    public class GruposBLL
    {

        public static bool Guardar(Grupos grupos)
        {
            bool paso = false;

            Contexto contexto = new Contexto();
            try
            {
                if (contexto.Grupos.Add(grupos) != null)
                {
                    contexto.SaveChanges();
                    paso = true;
                }

                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;
        }

        public static bool Modificar(Grupos grupos)
        {

            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {
                //todo: buscar las entidades que no estan para removerlas

                //recorrer el detalle
                foreach (var item in grupos.Detalle)
                {
                    //Muy importante indicar que pasara con la entidad del detalle
                    var estado = item.Id > 0 ? EntityState.Modified : EntityState.Added;
                    contexto.Entry(item).State = estado;
                }

                //Idicar que se esta modificando el encabezado
                contexto.Entry(grupos).State = EntityState.Modified;

                if (contexto.SaveChanges() > 0)
                {
                    paso = true;
                }
                contexto.Dispose();
            }
            catch (Exception)
            {
                throw;
            }
            return paso;

        }

        public static bool Eliminar(int id)
        {

            bool paso = false;

            Contexto contexto = new Contexto();

            try
            {

                Grupos grupos = contexto.Grupos.Find(id);
                contexto.Grupos.Remove(grupos);
                if (contexto.SaveChanges() > 0)
                {

                    paso = true;

                }

                contexto.Dispose();

            }

            catch (Exception)
            {

                throw;

            }

            return paso;
        }

        public static Grupos Buscar(int id)
        {

            Grupos grupos = new Grupos();
            Contexto contexto = new Contexto();

            try
            {
                grupos = contexto.Grupos.Find(id);
                //Cargar la lista en este punto porque
                //luego de hacer Dispose() el Contexto 
                //no sera posible leer la lista
                grupos.Detalle.Count();

                //Cargar los nombres de las personas
                foreach (var item in grupos.Detalle)
                {
                    //forzando la persona a cargarse
                    string s = item.Persona.Nombres;
                }
                contexto.Dispose();
            }

            catch (Exception)
            {

                throw;

            }

            return grupos;

        }

        public static List<Grupos> GetList(Expression<Func<Grupos, bool>> expression)
        {

            List<Grupos> Grupos = new List<Grupos>();
            Contexto contexto = new Contexto();

            try
            {

                Grupos = contexto.Grupos.Where(expression).ToList();
                contexto.Dispose();
            }
            catch (Exception)
            {

                throw;
            }

            return Grupos;
        }


    }
}

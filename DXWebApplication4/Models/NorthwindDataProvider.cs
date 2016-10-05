using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace DXWebApplication4.Models
{
    // DXCOMMENT: Configure a data model (In this sample, we do this in file NorthwindDataProvider.cs. You would better create your custom file for a data model.)
    public static class NorthwindDataProvider
    {
        const string NorthwindDataContextKey = "DXNorthwindDataContext";

        public static CRMEntities3 DB
        {
            get
            {
                if (HttpContext.Current.Items[NorthwindDataContextKey] == null)
                    HttpContext.Current.Items[NorthwindDataContextKey] = new CRMEntities3();
                return (CRMEntities3)HttpContext.Current.Items[NorthwindDataContextKey];
            }
        }

        public static IEnumerable GetPaises()
        {

            var query = from Paises in DB.Pais
                        select new
                        {
                            idPais = Paises.idPais,
                            nombrePais = Paises.nombre,

                        };
            return query.ToList();
        }
        public static IEnumerable GetCiudades()
        {
            return  DB.Ciudad.ToList();
        }
        public static void UpdateCiudades(Ciudad user)
        {
            Ciudad ciudad = DB.Ciudad.Where(u => u.idCiudad == user.idCiudad).SingleOrDefault();
            ciudad.nombre = user.nombre;
            ciudad.idPais = user.idPais;
            DB.SaveChanges();
        }

        public static void InsertCiudades(Ciudad user)
        {
            var userData = new Ciudad()
            {
                nombre = user.nombre,
                idPais = user.idPais

            };
            DB.Ciudad.Add(userData);
            DB.SaveChanges();

        }
        public static void InsertPaisesCiudades(Ciudad user, int idPais)
        {
            var userData = new Ciudad()
            {
                nombre = user.nombre,
                idPais = idPais

            };
            DB.Ciudad.Add(userData);
            DB.SaveChanges();

        }
        public static IEnumerable GetCiudades(int idPais)
        {
            return DB.Ciudad.Where(i => i.idPais == idPais).ToList();
        }


        public static void deleteCiudadinPaises(int idCiudad)
        {

            Ciudad user = DB.Ciudad.Where(val => val.idCiudad == idCiudad).SingleOrDefault();
            DB.Ciudad.Remove(user);
            DB.SaveChanges();
        }
    }
}
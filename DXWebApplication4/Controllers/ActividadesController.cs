using DXWebApplication4.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace DXWebApplication4.Controllers
{
    public class ActividadesController : Controller
    {
        CRMEntities3 DB = new CRMEntities3();
        // GET: Actividades
        public ActionResult Actividades()
        {
            var a = DB.Actividad;
            return View(a);
        }

        // GET: Actividades/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Actividades/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Actividades/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Actividades/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Actividades/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Actividades/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Actividades/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
        public JsonResult GetEvents()
        {

            var query = from pro in DB.Actividad
                        select new { pro.idActividad, pro.asunto, pro.descripcion, pro.fechaInicio, pro.fechaFinal };
            var v = query.ToList();
                return new JsonResult { Data = v, JsonRequestBehavior = JsonRequestBehavior.AllowGet };
            
   
        }
        [HttpPost]
        public void FullCalendarEventDrop(int id, string start)
        {
            DateTime startDt = DateTime.ParseExact(start, "ddd MMM d hh:mm:ss UTC yyyy", null).ToUniversalTime();


            Actividad OBJECTNAME = DB.Actividad.Find(id);
            OBJECTNAME.fechaInicio = startDt;
            OBJECTNAME.fechaFinal = startDt;

            DB.Entry(OBJECTNAME).State = EntityState.Modified;
            DB.SaveChanges();

        }
        //Action for Save event
        [HttpPost]
        public JsonResult SaveEvent(Actividad evt)
        {
            bool status = false;

            if (evt.fechaFinal != null && evt.fechaInicio.Value.TimeOfDay == new TimeSpan(0, 0, 0) &&
                evt.fechaFinal.Value.TimeOfDay == new TimeSpan(0, 0, 0))
            {
                evt.IsFullDay = true;
            }
            else
            {
                evt.IsFullDay = false;
            }

            if (evt.idActividad > 0)
            {
                var v = DB.Actividad.Where(a => a.idActividad.Equals(evt.idActividad)).FirstOrDefault();
                if (v != null)
                {
                 
                    v.asunto = evt.asunto;
                    v.descripcion = evt.descripcion;
                    v.fechaInicio = evt.fechaInicio;
                    v.fechaFinal = evt.fechaFinal;
                    v.IsFullDay = evt.IsFullDay;
                }
            }
            else
            {
                DB.Actividad.Add(evt);
            }
            DB.SaveChanges();
            status = true;
        
            return new JsonResult { Data = new { status = status } };
        }
        [HttpPost]
        public JsonResult DeleteEvent(int eventID)
        {
            bool status = false;
           
                var v = DB.Actividad.Where(a => a.idActividad.Equals(eventID)).FirstOrDefault();
                if (v != null)
                {
                    DB.Actividad.Remove(v);
                    DB.SaveChanges();
                    status = true;
                }
            
            return new JsonResult { Data = new { status = status } };
        }
    }
}

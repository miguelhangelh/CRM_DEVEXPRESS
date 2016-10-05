using DXWebApplication4.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DXWebApplication4.Controllers
{
    public class PaisesController : Controller
    {
       
        // GET: Paises
        public ActionResult index()
        {
            
            return View(NorthwindDataProvider.GetPaises());
        }
        public ActionResult MasterDetailMasterPartial()
        {
            return PartialView("MasterDetailMasterPartial", NorthwindDataProvider.GetPaises());
        }
        public ActionResult MasterDetailDetailPartial(int idPais)
        {
            ViewData["idPais"] = idPais;
            return PartialView("MasterDetailDetailPartial", NorthwindDataProvider.GetCiudades(idPais));
        }
       
        // GET: Paises/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Paises/Create
        [HttpPost]
        public ActionResult Create(Ciudad ciudad, int idPais)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    NorthwindDataProvider.InsertPaisesCiudades(ciudad,idPais);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(ciudad);
        }

        // GET: Paises/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Paises/Edit/5
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


        
            // POST: Paises/Delete/5
            [HttpPost]
        public ActionResult Delete(int idCiudad)
        {
            
            try
            {
                if (ModelState.IsValid)
                {

                    NorthwindDataProvider.deleteCiudadinPaises(idCiudad);
                 
                    return RedirectToAction("Index");
              
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View();
        }
    }
}

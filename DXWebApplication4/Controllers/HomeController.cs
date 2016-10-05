using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DXWebApplication4.Models;
using System.Data;
using System.Collections;

namespace DXWebApplication4.Controllers
{
    public class HomeController : Controller
    {
        CRMEntities3 BD = new CRMEntities3();

        public ActionResult Index()
        {
            // DXCOMMENT: Pass a data model for GridView
            if (Request.IsAuthenticated)
            {
                return View(NorthwindDataProvider.GetCiudades());
            }
            else
            {
                Response.Redirect("~/Account/Login");
            }
            return null;
        }
        
        public ActionResult GridViewPartialView() 
        {
            // DXCOMMENT: Pass a data model for GridView in the PartialView method's second parameter
            return PartialView("GridViewPartialView", NorthwindDataProvider.GetCiudades());
        }

        [HttpPost]
        public ActionResult Edit(Ciudad ciudad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NorthwindDataProvider.UpdateCiudades(ciudad);
                    return RedirectToAction("Index");
                }
            }
            catch (DataException)
            {
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists see your system administrator.");
            }
            return View(ciudad);
        }
        [HttpPost]
        public ActionResult Create(Ciudad ciudad)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    NorthwindDataProvider.InsertCiudades(ciudad);
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

public enum HeaderViewRenderMode { Full, Title }
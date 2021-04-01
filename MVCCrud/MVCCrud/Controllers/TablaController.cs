using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCCrud.Models;
using MVCCrud.Models.ViewModels;


namespace MVCCrud.Controllers
{
    public class TablaController : Controller
    {
        // GET: Tabla
        public ActionResult Index()
        {
            List<ListTablaViewModel> list;
            using (CrudEntities db = new CrudEntities())
            {
               
                 list = (from d in db.Tabla
                            select new ListTablaViewModel
                            {
                                Id = d.id,
                                Nombre = d.nombre,
                                Correo = d.correo

                            }).ToList(); 
            }
               
                return View(list);
        }


        public ActionResult Nuevo()
        {
            return View(); 
        }

        [HttpPost]
        public ActionResult Nuevo(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = new Tabla();
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;
                        oTabla.nombre = model.Nombre;
                        db.Tabla.Add(oTabla);
                        db.SaveChanges();

                    }
                    return Redirect("~/Tabla/");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        ///////////////////////////////////////Editar/////////////////////


        public ActionResult Editar( int Id)
        {
            TablaViewModel model = new TablaViewModel(); 
            using (CrudEntities db = new CrudEntities())
            {
                var oTabla = db.Tabla.Find(Id);
                model.Nombre = oTabla.nombre;
                model.Correo = oTabla.correo;
                model.Fecha_Nacimiento = oTabla.fecha_nacimiento;
                model.Id = oTabla.id;
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Editar(TablaViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    using (CrudEntities db = new CrudEntities())
                    {
                        var oTabla = db.Tabla.Find(model.Id);
                        oTabla.correo = model.Correo;
                        oTabla.fecha_nacimiento = model.Fecha_Nacimiento;
                        oTabla.nombre = model.Nombre;
                        db.Entry(oTabla).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();

                    }
                    return Redirect("~/Tabla/");
                }
                return View(model);
            }
            catch (Exception ex)
            {

                throw new Exception(ex.Message);
            }
        }

        ////////////Dele
        [HttpGet]
        public ActionResult Eliminar(int Id)
        {
            
            using (CrudEntities db = new CrudEntities())
            {
                var oTabla = db.Tabla.Find(Id);
                db.Tabla.Remove(oTabla);
                db.SaveChanges();
            }
            return Redirect("~/Tabla/");
        }



    }
}
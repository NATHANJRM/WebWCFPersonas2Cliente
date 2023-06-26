using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebWCFPersonas2Cliente.ServiceReferencePersonas;

namespace WebWCFPersonas2Cliente.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        respuesta res = new respuesta();

        public ActionResult Index()
        {            
            try
            {
                using (Service1Client server = new Service1Client())
                {
                    res = server.obtener();
                    if (res.Error)
                    {
                        throw new Exception(res.Mensaje);
                    }
                    List<EntPerson> ls = res.Listp.ToList();
                    return View(ls);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                List<EntPerson> ls = new List<EntPerson>();
                return View(ls);
            }
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(EntPerson ep)
        {            
            try
            {
                using (Service1Client server = new Service1Client())
                {
                    res = server.validar(ep);
                    if (res.Error)
                    {
                        throw new Exception(res.Mensaje);
                    }
                    res = server.agregar(ep);
                    if (res.Error)
                    {
                        throw new Exception(res.Mensaje);
                    }
                    TempData["mensaje"] = res.Mensaje;
                    return RedirectToAction("Index");
                    {//bool val = server.validar(ep);
                    //if (val)
                    //{
                    //    TempData["error"] = "Este Usuario ya Existe";
                    //    return View("Create");
                    //}
                    //else
                    //{
                    //    server.agregar(ep);
                    //    TempData["mensaje"] = "se ha registrado correctamente";
                    //    return View("Index", server.obtener().ToList());
                    //}
                    }
                    
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View();
            }
        }


        [HttpGet]
        public ActionResult Edit(int id)
        {
            using (Service1Client server = new Service1Client())
            {
                
                res = server.obtenerid(id);
                if (res.Error)
                {
                    throw new Exception (res.Mensaje);
                }
                EntPerson ep = res.Entp;
                return View(ep);
                //EntPerson ep = data.obtenerid(id);
                //return View(ep);
            }
        }

        [HttpPost]
        public ActionResult Edit(EntPerson ep)
        {
            try
            {
                using (Service1Client server = new Service1Client())
                {

                    res = server.validar(ep);
                    if (res.Error)
                    {
                        throw new Exception (res.Mensaje);                        
                    }
                    res = server.editar(ep);
                    if (res.Error)
                    {
                        throw new Exception (res.Mensaje);
                    }
                    TempData["mensaje"] = res.Mensaje;
                    return RedirectToAction("Index");
                    { //bool val = server.validar(ep);
                      //if (val)
                      //{
                      //    TempData["error"] = "Este ususario ya existe";
                      //    return View("Edit");
                      //}
                      //else
                      //{
                      //    server.editar(ep);
                      //    TempData["mensaje"] = "Edicion completa";
                      //    return RedirectToAction("Index");
                      //}
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                throw;
            }
        }

        [HttpGet]
        public ActionResult Delete(EntPerson ep)
        {
            using (Service1Client server = new Service1Client())
            {
                try
                {
                    res = server.eliminar(ep);
                    if (res.Error)
                    {
                        throw new Exception(res.Mensaje);
                    }
                    //data.eliminar(ep);
                    TempData["mensaje"] = res.Mensaje;
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                    throw;
                }
            }
        }

        public ActionResult buscar(string palabra)
        {
            try
            {
                using (Service1Client server = new Service1Client())
                {
                    res = server.buscar(palabra);
                    if (res.Error)
                    {
                        throw new Exception(res.Mensaje);
                    }
                    List<EntPerson> list = res.Listp.ToList();
                    //List<EntPerson> dt = data.buscar(palabra).ToList();
                    return View("Index", list);
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Index", new DataTable());
            }
        }


    }
}
using ProductComent.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ProductComent.Controllers
{
    public class HomeController : Controller
    {
        ProductInfoEntities db = new ProductInfoEntities();
        public ActionResult Index()
        {
            var list = db.products.ToList();
            return View(list);
        }

        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(product pro)
        {

            db.products.Add(pro);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
           
            product pro = db.products.Find(id);
            return View(pro);
        }
        [HttpPost]
        public ActionResult Edit(product emp, int id)
        {
            product emp1 = db.products.SingleOrDefault(m => m.productId == id);
            emp1.productId = emp.productId;
            emp1.productName = emp.productName;
            emp1.productType = emp.productType;
            return RedirectToAction("Index");
        }
        [HttpPost]
        public ActionResult CommentPost(string coments, int productId)
        {
            produtComent pro = new produtComent();
            pro.productId = productId;
            pro.comentName = coments;
            db.produtComents.Add(pro);
            db.SaveChanges();
            return RedirectToAction("Details", new {id= productId });
        }
        public ActionResult Details(int id)
        {

            product emp = db.products.Find(id);
            if (emp == null)
            {
                HttpNotFound();
            }

            return View(emp);
        }
        public ActionResult Coments(int id)
        {
            var list = db.produtComents.Where(a => a.productId == id);
            return View(list);
        }
        public ActionResult Delete(int id)
        {

            product emp = db.products.Find(id);
            db.products.Remove(emp);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
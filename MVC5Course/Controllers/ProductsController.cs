﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using Omu.ValueInjecter;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        // private FabricsEntities db = new FabricsEntities();  --> 移到BaseController

        // GET: Products
        public ActionResult Index()
        {
            var data = db.Product.OrderByDescending(p => p.ProductId).Take(10).ToList();
            return View(data);
        }

        public ActionResult Index2()
        {
            var data = db.Product.Where(p=>p.Active==true)
                .OrderByDescending(p => p.ProductId)
                .Take(10)
                .Select(p=> new ProductVIewModel()
                {
                    ProductName=p.ProductName,
                    ProductId=p.ProductId,
                    Price=p.Price,
                    Stock=p.Stock
                });
            return View(data);
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult AddNewProduct()
        {
            return View();
        }

        //mvcpost 按兩次 Tab
        [HttpPost]
        public ActionResult AddNewProduct(ProductVIewModel data)
        {
            if (!ModelState.IsValid)
            {

                return View();
            }

            var product = new Product()
            {
                ProductId = data.ProductId,
                Active = true,
                Price = data.Price,
                Stock = data.Stock,
                ProductName = data.ProductName,
            };

            this.db.Product.Add(product);  //還在記憶體
            this.db.SaveChanges();  //實際儲存到DB
            return RedirectToAction("Index2");
        }

        public ActionResult EditNewProduct(int id)
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult EditNewProduct(int id, ProductVIewModel data)
        {
            if (!ModelState.IsValid)
            {
                return View(data);
            }
            var one = db.Product.Find(id);
            one.InjectFrom(data);
            //one.ProductName = data.ProductName;
            //one.Price = data.Price;
            //one.Stock = data.Stock;
            db.SaveChanges();

            return RedirectToAction("Index2");
        }

        public ActionResult DeleteNewProduct(int id)
        {
            var data = db.Product.Find(id);
            return View(data);
        }

        [HttpPost]
        public ActionResult DeleteNewProduct(int id, ProductVIewModel data)
        {
            var product = db.Product.Find(id);
            if (product==null)
            {
                return HttpNotFound();
            }

            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index2");
        }

        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Product.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

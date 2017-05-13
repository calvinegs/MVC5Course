using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using MVC5Course.Models;
using MVC5Course.Models.ViewModels;

namespace MVC5Course.Controllers
{
    public class ProductsController : BaseController
    {
        //private FabricsEntities db = new FabricsEntities();

        //var repo = new ProductRepository(); // 未包含資料庫連線，只包含資料庫存取
        //repo.UnitOfWork = new EFUnitOfWork();
        //=> 上述二行可用下一行替代
        //ProductRepository repo = RepositoryHelper.GetProductRepository();

        // GET: Products
        public ActionResult Index(bool Active = true)
        {
            //var data = repo.All()
            //    .Where(p => p.Active.HasValue && p.Active == Active)
            //    .OrderByDescending(p => p.ProductId).Take(10);
            var data = repo.GetProduct列表頁所有資料(Active, showAll: true);
            return View(data);

            //var data1 = repo.All();

            //return View(db.Product.ToList());
            //return View(db.Product.OrderByDescending(p => p.ProductName).Take(20));
            //return View(//db.Product
            //    repo.All()  // 採repository方式
            //    //.Where(p => p.Active.HasValue && p.Active.Value == Active) // 原始 EF 模式
            //    .Where(p => p.ProductName.StartsWith("w"))
            //    .OrderByDescending(p=>p.ProductName));
        }

        // GET: Products/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = repo.Get單筆資料ByProductID(id.Value);  // .All().FirstOrDefault(p => p.ProductId == id.Value);  //db.Product.Find(id);
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

        // POST: Products/Create
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                repo.Add(product);
                repo.UnitOfWork.Commit();
                //db.Product.Add(product);
                //db.SaveChanges();
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
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductID(id??1);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Edit/5
        // 若要免於過量張貼攻擊，請啟用想要繫結的特定屬性，如需
        // 詳細資訊，請參閱 http://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ProductId,ProductName,Price,Active,Stock")] Product product)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(product).State = EntityState.Modified;
                //db.SaveChanges();
                // =>

                repo.Update(product);
                repo.UnitOfWork.Commit();
                                
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
            //Product product = db.Product.Find(id);
            Product product = repo.Get單筆資料ByProductID(id??1);
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
            //Product product = db.Product.Find(id);
            //db.Product.Remove(product);
            //db.SaveChanges();
            repo.UnitOfWork.Context.Configuration.ValidateOnSaveEnabled = false;    //強迫關閉驗證
            Product product = repo.Get單筆資料ByProductID(id);
            repo.Delete(product);

            var repoOrderLines = RepositoryHelper.GetOrderLineRepository(repo.UnitOfWork); //共用 unit of work
            foreach (var item in product.OrderLine)
            {
                repoOrderLines.Delete(item);
            }
            //repoOrderLines.UnitOfWork.Commit();   // 上述已共用，所以不須要此行指令

            repo.UnitOfWork.Commit();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                //db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult ListProducts()
        {
            //var data = db.Product
            var data = repo.Where(p => p.Active == true)
                    //.Where(p => p.Active == true)
                    .Select(p => new ProductLiteVM ()
                    {
                        ProductId = p.ProductId,
                        ProductName = p.ProductName,
                        Price = p.Price,
                        Stock = p.Stock
                    })
                    .Take(10);
            return View(data);
        }

        public ActionResult CreateProduct()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateProduct(ProductLiteVM data)
        {
            if (ModelState.IsValid)
            {

                //var data = db.Product.w
                //db.Product.Add(data);

                //db.SaveChanges();
                repo.UnitOfWork.Commit();
                return RedirectToAction("ListProducts");
            }

            return View();
        }
    }
}

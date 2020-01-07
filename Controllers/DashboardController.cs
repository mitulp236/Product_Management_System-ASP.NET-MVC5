using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo_project.Models;
using System.IO;
using System.Net;
using System.Data.Entity;

namespace demo_project.Controllers
{
    public class DashboardController : Controller
    {
        private Contex db = new Contex();
        // GET: Dashboard
        public ActionResult Index()
        {
            if (Session["email"] != null)
            {
                ViewBag.email = Session["email"];
                return View(db.products.ToList());
            }
            else
            {
                //returning back to the Login page
                return RedirectToAction("Login", "Auth");
            }
        }
        // searching purpose
        [HttpPost]
        public ActionResult Index(String searchstr)
        {
            if (Session["email"] != null)
            {
                if(searchstr != "" )
                {
                    ViewBag.email = Session["email"];
                    ViewBag.searchstr = searchstr;
                    var output = db.products.Where(m => m.ProductName.Contains(searchstr));
                    return View(output.ToList());
                }
                return RedirectToAction("Index","Dashboard");                
            }
            else
            {
                //returning back to the Login page
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult Create()
        {
            if (Session["email"] != null)
            {

                return View();
            }
            else
            {
                //returning back to the Login page
                return RedirectToAction("Login", "Auth");
            }
        }

        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        [HttpPost]
        public ActionResult Create(HttpPostedFileBase simage, HttpPostedFileBase limage, Product product)
        {
            if (Session["email"] != null)
            {
                if (ModelState.IsValid)
                {

                    Random r = new Random();
                    if (simage != null && simage.ContentLength > 0)
                        try
                        {
                            String extension = Path.GetExtension(simage.FileName);
                            if (extension.ToLower() == ".png" || extension.ToLower() == ".jpg" || extension.ToLower() == ".jpeg")
                            {
                                string filename = "_" + RandomString(10) + Path.GetFileName(simage.FileName);
                                string path = Path.Combine(Server.MapPath("~/PRODUCT_IMG"), filename);
                                simage.SaveAs(path);
                                product.Simage = "/PRODUCT_IMG/" + filename;

                            }
                            else 
                            {
                                ViewBag.simage = "Please Select Valid Image Format";
                                return View();
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            ViewBag.simage = "ERROR:" + ex.Message.ToString();
                        }
                    else
                    {
                        ViewBag.simage_err = "You have not specified a Small Image.";
                        return View();
                    }

                    if (limage != null && limage.ContentLength > 0)
                        try
                        {
                            String extension1 = Path.GetExtension(limage.FileName);
                            if (extension1.ToLower() == ".png" || extension1.ToLower() == ".jpg" || extension1.ToLower() == ".jpeg")
                            {
                                string filename = "_" + RandomString(10) + Path.GetFileName(limage.FileName);
                                string path = Path.Combine(Server.MapPath("~/PRODUCT_IMG"), filename);
                                limage.SaveAs(path);
                                product.Limage = "/PRODUCT_IMG/" + filename;
                            }
                            else
                            {
                                ViewBag.limage = "Please Select Valid Image Format";
                                return View();
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            ViewBag.simage = "ERROR:" + ex.Message.ToString();
                        }
                    else
                    {
                        ViewBag.limage_err = "You have not specified a Large Image.";
                        return View();
                    }
                    db.products.Add(product);
                    db.SaveChanges();
                    TempData["message"] = product.ProductName + " Successfully Added ! ";
                    return RedirectToAction("Index", "Dashboard");
                }
                return View();
            }
            else
            {
                //returning back to the Login page
                return RedirectToAction("Login", "Auth");
            }
        }

        public ActionResult Edit(int? id)
        {
            if (Session["email"] != null)
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                Product product = db.products.Find(id);
                Session["simage"] = product.Simage;
                Session["limage"] = product.Limage;
                if (product == null)
                {
                    return HttpNotFound();
                }
                ViewBag.id = id;
                return View(product);
            }
            else
            {
                //returning back to the Login page
                return RedirectToAction("Login", "Auth");
            }

            
        }

        [HttpPost]
        public ActionResult Edit(HttpPostedFileBase simage, HttpPostedFileBase limage, Product product)
        {
            //Product p = db.products.Find(product.ProductId);
            //ViewBag.ok = p.Simage;
            //return View();

            if (ModelState.IsValid)
            {

                Random r = new Random();
                if ((simage != null && simage.ContentLength > 0) && (limage != null && limage.ContentLength > 0))
                { 
                    try
                    {

                        string filename = "_" + RandomString(10) + Path.GetFileName(simage.FileName);
                        string path = Path.Combine(Server.MapPath("~/PRODUCT_IMG"), filename);
                        simage.SaveAs(path);
                        product.Simage = "/PRODUCT_IMG/" + filename;


                        string filename1 = "_" + RandomString(10) + Path.GetFileName(limage.FileName);
                        string path1 = Path.Combine(Server.MapPath("~/PRODUCT_IMG"), filename);
                        limage.SaveAs(path1);
                        product.Limage = "/PRODUCT_IMG/" + filename;

                        ViewBag.limage = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.simage = "ERROR:" + ex.Message.ToString();
                    }

                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                TempData["message"] = product.ProductName + " successfully Updated";
                return RedirectToAction("Index");
            }
             else   if ((simage != null && simage.ContentLength > 0) && (limage == null))
                {
                    try
                    {

                        string filename = "_" + RandomString(10) + Path.GetFileName(simage.FileName);
                        string path = Path.Combine(Server.MapPath("~/PRODUCT_IMG"), filename);
                        simage.SaveAs(path);
                        product.Simage = "/PRODUCT_IMG/" + filename;

                        product.Limage = Session["limage"].ToString();


                        ViewBag.limage = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.simage = "ERROR:" + ex.Message.ToString();
                    }

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = product.ProductName + " successfully Updated";
                    return RedirectToAction("Index");
                }
                else if ((simage == null) && (limage != null && limage.ContentLength > 0))
                {
                    try
                    {
                        product.Simage = Session["simage"].ToString();

                        string filename1 = "_" + RandomString(10) + Path.GetFileName(limage.FileName);
                        string path1 = Path.Combine(Server.MapPath("~/PRODUCT_IMG"), filename1);
                        limage.SaveAs(path1);
                        product.Limage = "/PRODUCT_IMG/" + filename1;


                        ViewBag.limage = "File uploaded successfully";
                    }
                    catch (Exception ex)
                    {
                        ViewBag.simage = "ERROR:" + ex.Message.ToString();
                    }

                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = product.ProductName + " successfully Updated";
                    return RedirectToAction("Index");
                }


                else
                {
                    product.Simage = Session["simage"].ToString();
                    product.Limage = Session["limage"].ToString();
                    db.Entry(product).State = EntityState.Modified;
                    db.SaveChanges();
                    TempData["message"] = product.ProductName + " successfully Updated";
                    return RedirectToAction("Index");
                }
        }
            return View();
        }
            

        

        // single delete
        public ActionResult Delete(int? id)
        {
            Product product = db.products.Find(id);
            
            // delete small image
            string filename_simage = product.Simage;
            string path_simage = Request.MapPath("~" + filename_simage);
            if (System.IO.File.Exists(path_simage))
            {
                System.IO.File.Delete(path_simage);
            }

            // delete large image
            string filename_limage = product.Limage;
            string path_limage = Request.MapPath("~" + filename_limage);
            if (System.IO.File.Exists(path_limage))
            {
                System.IO.File.Delete(path_limage);
            }

            // delete database row for that id
            db.products.Remove(product);
            db.SaveChanges();
            TempData["message"] = product.ProductName + "  successfully Removed ";
            return RedirectToAction("Index");

        }

        public ActionResult multi_delete(String[] checkbox)
        {
            int[] getid = null;
            if (checkbox != null)
            {
                getid = new int[checkbox.Length];
                int j = 0;
                foreach (string i in checkbox)
                {
                    int.TryParse(i, out getid[j++]);
                }

                List<Product> getempids = new List<Product>();
                
                getempids = db.products.Where(x => getid.Contains(x.ProductId)).ToList();
                foreach (var s in getempids)
                {
                    db.products.Remove(s);
                }
                db.SaveChanges();
                TempData["message"] = checkbox.Length + " Products Successfully Removed ";
            }
            return RedirectToAction("Index");
        }


    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ninja.model.Entity;
using ninja.model.Manager;

namespace ninja.Controllers
{
    public class InvoiceController : Controller
    {
        private InvoiceManager manager = new InvoiceManager();

        // GET: Invoice
        public ActionResult Index()
        {
            return View(manager.GetAll());
        }

        // GET: Invoice/Detail/5
        public ActionResult Detail(int id)
        {

            Invoice factura = manager.GetById(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewData["InvoiceId"] = id;
            return View(factura);
        }

        // GET: Invoice/New
        public ActionResult New()
        {
            ViewBag.Types = new[] { "A", "B", "C" };
            return View();
        }

        // POST: Invoice/New
        [HttpPost]
        public ActionResult New(Invoice invoice)
        {
            try
            {
                manager.Insert(invoice);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/NewDetailItem
        public ActionResult NewDetailItem(int IdInvoice)
        {
            if (!manager.Exists(IdInvoice))
            {
                return HttpNotFound();
            }

            return View(new InvoiceDetail() { Id = 0, InvoiceId = IdInvoice });
        }

        // POST: Invoice/NewDetailItem
        [HttpPost]
        public ActionResult NewDetailItem(InvoiceDetail invoiceDetail)
        {
            try
            {
                manager.GetById(invoiceDetail.InvoiceId).AddDetail(invoiceDetail);

                return RedirectToAction("Detail", new { id = invoiceDetail.InvoiceId });
            }
            catch
            {
                return View();
            }
        }

        // GET: Invoice/Update/5
        public ActionResult Update(int id)
        {
            Invoice factura = manager.GetById(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            ViewBag.Types = new SelectList(new[] { "A", "B", "C" }, manager.GetById(id).Type);
            return View(factura);
        }

        // POST: Invoice/Update/5
        [HttpPost]
        public ActionResult Update(int id, Invoice model)
        {
            try
            {
                manager.Update(model);

                return RedirectToAction("Index");
            }
            catch
            {
                ViewBag.Types = new[] { "A", "B", "C" };
                return View();
            }
        }

        // GET: Invoice/Delete/5
        public ActionResult Delete(int id)
        {
            Invoice factura = manager.GetById(id);
            if (factura == null)
            {
                return HttpNotFound();
            }
            return View(factura);
        }

        // POST: Invoice/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, Invoice model)
        {
            try
            {
                manager.Delete(id);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

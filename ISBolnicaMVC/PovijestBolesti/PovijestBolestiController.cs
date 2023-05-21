using ISBolnicaBLL.Pacijent;
using ISBolnicaBLL.PovijestBolesti;
using ISBolnicaDAL.DataAccess.Entities;
using ISBolnicaMVC.Models;
using ISBolnicaMVC.Pacijent;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace ISBolnicaMVC.PovijestBolesti
{
    public class PovijestBolestiController : Controller
    {
        private readonly IPovijestBolestiService _povijestBolestiService;
        private readonly IPacijentService _pacijentService;

        public PovijestBolestiController()
        {
            _povijestBolestiService = new PovijestBolestiService();
            _pacijentService = new PacijentService();
        }

        // GET: PovijestBolestiController/Edit/5
        public ActionResult Edit(int pacijentId, int? id)
        {
            if (!id.HasValue)
            {
                ViewData["Doktors"] = _povijestBolestiService.GetDoktors();
                ViewData["Sobas"] = _povijestBolestiService.GetSobas();
                ViewData["Terapijas"] = _povijestBolestiService.GetTerapijas();

                var pacijent = _pacijentService.GetPacijentDetail(pacijentId);
                return View(new PovijestBolestiDetail
                {
                    PovijestBolestiId = 0,
                    PacijentId = pacijentId,
                    Pacijent = $"{pacijent.Ime} {pacijent.Prezime}"
                });
            }
            else
            {
                ViewData["Doktors"] = _povijestBolestiService.GetDoktors();
                ViewData["Sobas"] = _povijestBolestiService.GetSobas();
                ViewData["Terapijas"] = _povijestBolestiService.GetTerapijas();

                var d = _povijestBolestiService.GetPovijestBolestiDetail(id.Value);
                return View(d);
            }
        }

        // POST: PovijestBolestiController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PovijestBolestiDetail pbd)
        {
            try
            {
                _povijestBolestiService.EditPovijestBolesti(pbd);

                return RedirectToAction(nameof(Edit), "Pacijent", new { id = pbd.PacijentId });
            }
            catch(Exception exc)
            {
                ViewBag.Message = exc.Message;

                ViewData["Doktors"] = _povijestBolestiService.GetDoktors();
                ViewData["Sobas"] = _povijestBolestiService.GetSobas();
                ViewData["Terapijas"] = _povijestBolestiService.GetTerapijas();

                return View(pbd);
            }
        }

        // GET: PovijestBolestiController/Delete/5
        public ActionResult Delete(int pacijentId, int id)
        {
            _povijestBolestiService.DeletePovijestBolesti(id);
            return RedirectToAction(nameof(Edit), "Pacijent", new { id = pacijentId });
        }

        // GET: PovijestBolestiController/Close/5
        public ActionResult Close(int pacijentId, int id)
        {
            _povijestBolestiService.ClosePovijestBolesti(id);
            return RedirectToAction(nameof(Edit), "Pacijent", new { id = pacijentId });
        }
    }
}

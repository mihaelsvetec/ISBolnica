using ISBolnicaBLL.Pacijent;
using ISBolnicaBLL.Terapija;
using ISBolnicaMVC.Models;
using ISBolnicaMVC.Terapija;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace ISBolnicaMVC.Terapija
{
    public class TerapijaController : Controller
    {
        private readonly ITerapijaService _terapijaService;

        public TerapijaController(){
            _terapijaService = new TerapijaService();
        }

        // GET: TerapijaController
        public ActionResult Index(int page = 1)
        {
            int pageSize = 10;
            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                ItemsPerPage = pageSize,
                TotalItems = _terapijaService.GetTerapijaCount()
            };
            if (page < 1 || page > pagingInfo.TotalPages)
            {
                return RedirectToAction(nameof(Index), new { page = 1 });
            }

            var terapijas = _terapijaService.GetTerapijas(page, pageSize);

            return View(new TerapijeVM
            {
                PagingInfo = pagingInfo,
                Terapijas = terapijas
            });
        }

        // GET: TerapijaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: TerapijaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddTerapija t)
        {
            _terapijaService.AddTerapija(t);

            return RedirectToAction(nameof(Index), new { page = 1 });
        }


        // GET: TerapijaController/Delete/5
        public ActionResult Delete(int id)
        {
            _terapijaService.DeleteTerapija(id);

            return RedirectToAction(nameof(Index), new { page = 1 });
        }
    }
}

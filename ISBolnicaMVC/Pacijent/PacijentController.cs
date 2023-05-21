using ISBolnicaBLL.Pacijent;
using ISBolnicaMVC.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing.Printing;

namespace ISBolnicaMVC.Pacijent
{
    public class PacijentController : Controller
    {
        private readonly IPacijentService _pacijentService;

        public PacijentController(){
            _pacijentService = new PacijentService();
        }

        // GET: PacijentController
        public ActionResult Index(int page = 1, int sort = 1, bool ascending = true)
        {
            int pageSize = 10;
            var pagingInfo = new PagingInfo
            {
                CurrentPage = page,
                Sort = sort,
                Ascending = ascending,
                ItemsPerPage = pageSize,
                TotalItems = _pacijentService.GetPacijentCount()
            };
            if (page < 1 || page > pagingInfo.TotalPages)
            {
                return RedirectToAction(nameof(Index), new { page = 1, sort, ascending });
            }

            var pacijenti = _pacijentService.GetAllPacijent(page, pageSize, ascending, sort);

            var pacijentiVM = new PacijentiVM
            {
                Pacijenti = pacijenti,
                PagingInfo = pagingInfo
            };

            return View(pacijentiVM);
        }

        // GET: PacijentController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PacijentController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PacijentController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(AddPacijent pacijent)
        {
            try
            {
                _pacijentService.AddPacijent(pacijent);

                return RedirectToAction(nameof(Index), new { page = 1, sort = 1, ascending = false });
            }
            catch (Exception exc)
            {
                ViewBag.Message = exc.Message;
                
                return View(pacijent);
            }
        }

        // GET: PacijentController/Edit/5
        public ActionResult Edit(int id)
        {
            var d = _pacijentService.GetPacijentDetail(id);

            return View(d);
        }

        // POST: PacijentController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PacijentController/Delete/5
        public ActionResult Delete(int id)
        {
            _pacijentService.DeletePacijent(id);

            return RedirectToAction(nameof(Index), new { page = 1, sort = 1, ascending = false });
        }

        // POST: PacijentController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}

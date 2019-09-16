using KingdomJoy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using KingdomJoy.Helpers;


namespace KingdomJoy.Areas.Registrar.Controllers
{
    public class MembersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private FormOptions _formOptions = new FormOptions();
        // GET: Registrar/Members
        public async Task<ActionResult> Index()
        {
            IEnumerable<Member> members = await db.Members.ToListAsync<Member>();
            return View(members);
        }

        // GET: Registrar/Members/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Registrar/Members/Create
        public async Task<ActionResult> Create()
        {
            IEnumerable<Designation> designations = await db.Designations.ToListAsync();
            IEnumerable<MemberTitle> titles = await db.MemberTitles.ToListAsync();
            MembersViewModel viewModel = new MembersViewModel
            {
                Designations = designations,
                Titles = titles
            };

            ViewBag.options = _formOptions.YesNo();
            ViewBag.gender = _formOptions.Gender();
            ViewBag.maritalStatus = _formOptions.MartitalStatus();

            return View(viewModel);
        }


        // POST: Registrar/Members/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Registrar/Members/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Registrar/Members/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Registrar/Members/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Registrar/Members/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}

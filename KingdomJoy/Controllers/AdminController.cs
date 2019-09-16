using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using KingdomJoy.Models;
using KingdomJoy.Extensions;

namespace KingdomJoy.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ApplicationUser _users= new ApplicationUser();
        private ApplicationUserManager _userManager;
        private ApplicationRoleManager _roleManager;
        public AdminController()
        {

        }

        public AdminController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        protected ApplicationRoleManager RoleManager
        {
            get
            {
                return _roleManager ?? Request.GetOwinContext().GetUserManager<ApplicationRoleManager>();
            }

            private set
            {
                _roleManager = value;
            }
        }

        // GET: Admin
        public async Task<ActionResult> Index()
        {
            var usersCount = await db.Users.ToListAsync();
            return View( );
        } 
        
        // GET: Admin/Users
        public async Task<ActionResult> Users()
        {
     
            List<UserViewModel> Users = new List<UserViewModel>();
            await Users.GetUsers();

            return View(Users);
        }

        // GET: Admin/MemberDesignations
        [HttpGet]
        public async Task<ActionResult> MemberDesignations()
        {
            IEnumerable<Designation> designations = await db.Designations.ToListAsync();
            var viewModel = new DesignationViewModel()
            {
                Designations = designations
            };

            return View(viewModel);
        }

        [HttpGet]
        public ActionResult CreateMemberDesignation()
        {
            ViewBag.status = "";
            return View();
        }

        // POST: /Admin/CreateMemberDesignation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateMemberDesignation(Designation model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.Name != null)
                    {
                        var designation = new Designation
                        {
                            Name = model.Name,
                        };

                        db.Designations.Add(designation);
                        await db.SaveChangesAsync();
                        return RedirectToAction("MemberDesignations");
                    }
                    else
                    {
                        ModelState.AddModelError("Name","Designation name cannot be empty");
                        return View("CreateMemberDesignation");
                    }
                }
                catch (Exception e)
                {
                    ModelState.AddModelError("Name",e.Message);
                    return View("CreateMemberDesignation");
                }
            }

            return View("CreateMemberDesignation");
        }

        // GET: Admin/MemberTitles
        public async Task<ActionResult> MemberTitles()
        {
            IEnumerable<MemberTitle> Titles = await db.MemberTitles.ToListAsync();
            var viewModel = new NewTitleViewModel()
            {
                MemberTitles = Titles
            };

            return View(viewModel);
        }

        // POST: /Account/MemberTitles
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> MemberTitles(NewTitleViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (model.MemberTitle.Name != null)
                    {
                        var Title = new MemberTitle
                        {
                            Name = model.MemberTitle.Name,
                        };

                        db.MemberTitles.Add(Title);
                        await db.SaveChangesAsync();

                        ViewBag.status = true;
                        ViewBag.message = "Created Title";
                        IEnumerable<MemberTitle> Titles = await db.MemberTitles.ToListAsync();
                        var viewModel = new NewTitleViewModel()
                        {
                            MemberTitles = Titles
                        };
                        return View(viewModel);
                    }
                    else
                    {
                        ViewBag.status = false;
                        ViewBag.message = "Failed to create Title. Name empty.";
                        IEnumerable<MemberTitle> Titles = await db.MemberTitles.ToListAsync();
                        var viewModel = new NewTitleViewModel()
                        {
                            MemberTitles = Titles
                        };
                        return View(viewModel);
                    }
                }
                catch (Exception e)
                {
            
                    IEnumerable<MemberTitle> Titles = await db.MemberTitles.ToListAsync();
                    var viewModel = new NewTitleViewModel()
                    {
                        MemberTitles = Titles
                    };
                    ViewBag.status = false;
                    ViewBag.message = e.Message;
                    return View(viewModel);
                }
            }
            return View(model);
        }

        //// GET: Admin/DeleteTitle/5
        public async Task<ActionResult> DeleteTitle(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MemberTitle Title = await db.MemberTitles.FindAsync(id);
            db.MemberTitles.Remove(Title);
            await db.SaveChangesAsync();
            return RedirectToAction("MemberTitles");
        }

        // GET: Admin/EditTitle/5
        public async Task<ActionResult> EditTitle(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            MemberTitle memberTitle = await db.MemberTitles.FindAsync(id);
       
            return View(memberTitle);
        }

        // POST: Admin/EditTitle/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditTitle([Bind(Include = "Id,Name")] MemberTitle data)
        {

            if (ModelState.IsValid)
            {
                MemberTitle memberTitle = await db.MemberTitles.FindAsync(data.Id);
              
                if (memberTitle != null)
                {
                    memberTitle.Name = data.Name;
                    db.Entry(memberTitle).State = EntityState.Modified;
                    await db.SaveChangesAsync();
                    return RedirectToAction("MemberTitles");
                }
                return View(data);
            }
            return View(data);

        }


        // GET: Admin/Users
        public async Task<ActionResult> CreateUser()
        {
            RegisterViewModel User = new RegisterViewModel
            {
                Roles = await db.Roles.ToListAsync()
            };
        
            return View(User);
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateUser(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    PhoneNumber = model.PhoneNumber,
                    EmailConfirmed = true,
                    Gender = model.Gender,
                    LastName = model.LastName,
                    Dob = DateTime.Now,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                };
                
               
                var result = await UserManager.CreateAsync(user, model.Password);
                
                if (result.Succeeded)
                {
                    model.Roles = await db.Roles.ToListAsync();
                    var registeredUser = await UserManager.FindByEmailAsync(user.Email);
                    var currentUserRoles = await UserManager.GetRolesAsync(registeredUser.Id);
                    IdentityResult removeResult = await UserManager.RemoveFromRolesAsync(registeredUser.Id, currentUserRoles.ToArray());

                    if (!removeResult.Succeeded)
                    {
                        AddErrors(removeResult);
                    }
                    else
                    {
                        var SelectedRole = await RoleManager.FindByIdAsync(model.Role.ToString());
                        IdentityResult addResult = await UserManager.AddToRoleAsync(registeredUser.Id, SelectedRole.Name);

                        if (addResult.Succeeded)
                        {
                            ViewBag.status = true;
                            return View(model);
                        }
                    }


                    //await SignInManager.SignInAsync(user, isPersistent:false, rememberBrowser:false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");
                    ViewBag.status = true;
                    return View(model);
            
                }
                AddErrors(result);
            }

        
            model.Roles = await db.Roles.ToListAsync();
            ViewBag.status = false;
            // If we got this far, something failed, redisplay form
            return View(model);
        }



        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        //// GET: Admin/Details/5
        //public async Task<ActionResult> Details(string id)
        //{
        //    if (id == null)
        //    {
        //        return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        //    }
        //    ApplicationUser applicationUser = await db.Users.Find(id);
        //    if (applicationUser == null)
        //    {
        //        return HttpNotFound();
        //    }
        //    return View(applicationUser);
        //}

        //// GET: Admin/Create
        //public ActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Admin/Create
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> Create([Bind(Include = "Id,Email,EmailConfirmed,PasswordHash,SecurityStamp,PhoneNumber,PhoneNumberConfirmed,TwoFactorEnabled,LockoutEndDateUtc,LockoutEnabled,AccessFailedCount,UserName")] ApplicationUser applicationUser)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        db.ApplicationUsers.Add(applicationUser);
        //        await db.SaveChangesAsync();
        //        return RedirectToAction("Index");
        //    }

        //    return View(applicationUser);
        //}

        //// GET: Admin/Edit/5
        public async Task<ActionResult> Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ApplicationUser applicationUser = await UserManager.FindByIdAsync(id);
            if (applicationUser == null)
            {
                return HttpNotFound();
            }

            return View(applicationUser);
        }

        //// POST: Admin/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,PhoneNumber,FirstName,LastName")] UserEditViewModel data)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByIdAsync(data.Id);
                if(user != null)
                {
                    user.FirstName = data.FirstName;
                    user.LastName = data.LastName;
                    user.PhoneNumber = data.PhoneNumber;
                    var result = await UserManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        RedirectToAction("GetUsers");
                    }
                    AddErrors(result);
                }
                //db.Entry(applicationUser).State = EntityState.Modified;
                //await db.SaveChangesAsync();

                return RedirectToAction("Users");
            }
            return View(data);
        }

        //// GET: Admin/Delete/5
        public async Task<ActionResult> Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }



        //// POST: Admin/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var user = await UserManager.FindByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            var result = await UserManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return RedirectToAction("Users");
            }
            AddErrors(result);
            return RedirectToAction("Users");
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

//Bao Tran
//Project 1

using System.Web.Mvc;
using WebSite_1.Models;
using System.Linq;



namespace WebSite_1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IClassRepository classRepository;
        private readonly IUserRepository userRepository;
        private readonly IEnrollClassRepository enrollClassRepository;

        public HomeController(IClassRepository classRepository, IUserRepository userRepository, IEnrollClassRepository enrollClassRepository)
        {
            this.classRepository = classRepository;
            this.userRepository = userRepository;
            this.enrollClassRepository = enrollClassRepository;
        }

            public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        
        public ActionResult Delete(int id)
        {
            ViewBag.Message = "Remove Class";
            var user = (WebSite_1.Models.UserModel)Session["User"];
            enrollClassRepository.Remove(user.Id, id);

            return RedirectToAction("UserClasses");
        }

        [Authorize]
        public ActionResult UserClasses()
        {
            var user = (WebSite_1.Models.UserModel)Session["User"];
            //var item = enrollClassRepository.Add(user.Id, id);
            var items = enrollClassRepository.GetAll(user.Id)
                .Select(t => new WebSite_1.Models.EnrollClassItem
                {
                    ClassId = t.ClassId,
                    Name = t.Name,
                    Description = t.Description,
                    Price = t.Price
                })
                .ToArray();
            return View(items);
        }

        [Authorize]
        public ActionResult StudentClasses(int id)
        {
            var user = (WebSite_1.Models.UserModel)Session["User"];
            var item = enrollClassRepository.Add(user.Id, id);
            var items = enrollClassRepository.GetAll(user.Id)
                .Select(t => new WebSite_1.Models.EnrollClassItem
                {
                    //UserId = t.UserId,
                    ClassId = t.ClassId,
                    Name = t.Name,
                    Description = t.Description,
                    Price = t.Price
                })
                .ToArray();
            return View(items);
        }

        public ActionResult ClassList()
        {            
            return View(classRepository.Classes);
        }


        public ActionResult EnrollClass()
        {
            //var class_list = new Classes_Repository.ClassRepository();

            return View(classRepository.Classes);
        }

        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Register(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                userRepository.Register(registerModel.UserName, registerModel.Password);
                return Redirect("~/");
            }
            else
            {
                return View();
            }
        }

        [HttpGet]
        public ActionResult Login()
        {
            ViewData["ReturnUrl"] = Request.QueryString["returnUrl"];
            return View();
        }

        [HttpPost]
        public ActionResult LogIn(LoginModel loginModel, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = userRepository.LogIn(loginModel.UserName, loginModel.Password);

                if (user == null)
                { 
                    ModelState.AddModelError("", "User name and password do not match.");
                }
                else
                {
                    Session["User"] = new WebSite_1.Models.UserModel
                    {
                        Id = user.Id,
                        Name = user.Name
                    };

                    System.Web.Security.FormsAuthentication.SetAuthCookie(
                        loginModel.UserName, false);

                    return Redirect(returnUrl ?? "~/");
                }
            }

            return View(loginModel);
        }

        public ActionResult LogOff()
        {
            Session["User"] = null;
            System.Web.Security.FormsAuthentication.SignOut();

            return Redirect("~/");
        }


    }
}
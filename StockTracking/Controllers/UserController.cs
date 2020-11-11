using StockTracking.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace StockTracking.Controllers
{
    [AllowAnonymous]
    public class UserController : Controller
    {
        // GET: User
        StockTrackingEntities c = new StockTrackingEntities();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(User user)
        {
            var data = c.User.FirstOrDefault(x => x.UserName == user.UserName && x.Password == user.Password);
            if (data != null)
            {
                FormsAuthentication.SetAuthCookie(user.UserName, false);
                return RedirectToAction("Index", "Product");
            }
            ViewBag.Error = "Kullanıcı adı veya Şifre yanlış";

            return View();
        }
        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
        [HttpGet]
        public ActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ResetPassword(User user)
        {

            var model = c.User.Where(x => x.Email == user.Email).FirstOrDefault();
            if (model != null)
            {
                Guid random = Guid.NewGuid();
                model.Password = random.ToString().Substring(0, 15);
                c.SaveChanges();
                SmtpClient client = new SmtpClient("smtp.yandex.ru", 587);
                client.EnableSsl = true;
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress("hasanhsynshn7@yandex.com", "Şifreni Sıfırla");
                mail.To.Add(model.Email);
                mail.IsBodyHtml = true;
                mail.Subject = "Şifre Değiştirme İsteği";
                mail.Body += "Merhaba" +" " + model.UserNameSurname + "<br/> Kullanıcı Adınız = "+" "+model.UserName+"<br/> Şifreniz = "+" "+model.Password;
                NetworkCredential net = new NetworkCredential("hasanhsynshn7@yandex.com", "Hasaneyn536,,..");
                client.Credentials = net;
                client.Send(mail);
                return RedirectToAction("Login");
            }
            ViewBag.Error = "Böyle bir e-mail bulunamadı.";
            return View();
        }

        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(User user)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
           
            c.Entry(user).State = System.Data.Entity.EntityState.Added;
            c.SaveChanges();
            return RedirectToAction("Login");
        }
        public ActionResult Update()
        {
            if (User.Identity.IsAuthenticated)
            {
                var userName = User.Identity.Name;
                var model = c.User.FirstOrDefault(x => x.UserName == userName);
                if (model!=null)
                {
                    return View(model);
                }
                else
                {
                    return View(new User());
                }
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Update(User user)
        {
            c.Entry(user).State = System.Data.Entity.EntityState.Modified;
            c.SaveChanges();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
    
}
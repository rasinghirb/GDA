using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using GDA.Repository;
using Microsoft.AspNetCore.Mvc;
using GDA.Models;

namespace GDA.Controllers
{
    public class UsersController : Controller
    {
        GDARepository _repository = new GDARepository();


        [ViewData]
        public string Title { get; set; }
        public UsersController(GDARepository gDARepository)
        {
            _repository = gDARepository;
        }
        //// GET: /<controller>/  
        public IActionResult Index()
        {
            DataTable dt;
            dt = _repository.GetSPDataTable("SpTbleUserView");
            List<ViewAllUsers> list = new List<ViewAllUsers>();
            list = (from DataRow dr in dt.Rows
                     select new ViewAllUsers
                     {
                         PISNo = Convert.ToInt32(dr["PIS No"]),
                         Name = dr["Name"].ToString(),
                         RankName = dr["Rank"].ToString(),
                         CoyName = dr["Coy"].ToString(),

                     }).ToList<ViewAllUsers>();

            return View(list);
            
        }
        [HttpGet]
        public IActionResult AddNewUser()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddNewUser([Bind] UserModel user)

        {
            if (ModelState.IsValid)
            {
                _repository.AddNewUser(user);
                return RedirectToAction("Index");

            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserModel user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind] UserModel user)
        {
            if (id != user.UserId)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                _repository.UpdateUser(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Find(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserModel user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            UserModel user = _repository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int? id)
        {
            _repository.DeleteUser(id);
            return RedirectToAction("Index");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ToDoList_08272020.Models;

namespace ToDoList_08272020.Controllers
{
    public class UserController : Controller
    {
        private readonly UserdbContext _userdb; 
        public UserController(UserdbContext userdb)
        {
            _userdb = userdb; 
        }
        [Authorize]
        public IActionResult Index()
        {
            var userToDoList = _userdb.ToDoList.ToList(); 

            return View(userToDoList);
        }
        [HttpGet]
        public IActionResult AddTask()
        {
            return View(); 
        }
        [HttpPost]
        public IActionResult AddTask(ToDoList newTask)
        {
            //assine new task userid to id, 
            newTask.UserId = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            if (ModelState.IsValid)
            {
                _userdb.ToDoList.Add(newTask);
                _userdb.SaveChanges(); 
            }
            return RedirectToAction("ToDoList"); 
        }
        [Authorize]
        public IActionResult ToDoList()
        {
            string id = User.FindFirst(ClaimTypes.NameIdentifier).Value;
            List<ToDoList> toDoList = _userdb.ToDoList.Where(x => x.UserId == id).ToList();
            return View(toDoList); 
        }

        public IActionResult DeleteTask(int Id)
        {
            var foundTask = _userdb.ToDoList.Find(Id);
            if(foundTask != null)
            {
                _userdb.ToDoList.Remove(foundTask);
                _userdb.SaveChanges(); 
            }
            return RedirectToAction("ToDoList"); 
        }

        public IActionResult MarkComplete(int Id)
        {
            var foundTask = _userdb.ToDoList.Find(Id);
            if (foundTask != null)
            {
                foundTask.Complete = true;
                _userdb.ToDoList.Update(foundTask);
                _userdb.SaveChanges();
            }
            return RedirectToAction("ToDoList");
        }
    }
}

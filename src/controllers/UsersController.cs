using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using File_Access_Monitoring.Database;
using File_Access_Monitoring.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace File_Access_Monitoring.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyDBContext _context;
        User user;

        public UsersController(MyDBContext context)
        {
            _context = context;
        }

        // GET: Users
        public async Task<IActionResult> Index()
        {
            return _context.Users != null ?
                        View(await _context.Users.Where(u => u.UserType != "Admin").ToListAsync()) :
                        Problem("Entity set 'MyDBContext.Users'  is null.");
        }

        // GET: Users/Details/5
        public async Task<IActionResult> MyProfile()
        {
            user = HttpContext.Session.GetObjectFromJson<User>("User");


            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            return View();
        }


        //[HttpPost]
        //public async Task<IActionResult> Create(User user)
        //{

        //    User tempUser = await _context.Users.FirstOrDefaultAsync(m => m.Email == user.Email);
        //    if (tempUser != null)
        //    {
        //        ViewBag.Error = "Email already in use";
        //        return View();
        //    }
        //    tempUser = await _context.Users.FirstOrDefaultAsync(m => m.Mobile == user.Mobile);
        //    if (tempUser != null)
        //    {
        //        ViewBag.Error = "Mobile Number already in use";
        //        return View();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(user);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction("Login");
        //    }
        //    else
        //    {
        //        ViewBag.Error = "Server Error";
        //    }

        //    return View();
        //}

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            user.DeviceID = Guid.NewGuid().ToString();
            User tempUser;
            if (!string.IsNullOrEmpty(user.Email))
            {

                tempUser = await _context.Users.FirstOrDefaultAsync(m => m.Email == user.Email);
                if (tempUser != null) return Conflict("Email Already In Use");

            }

            tempUser = await _context.Users.FirstOrDefaultAsync(m => m.Mobile == user.Mobile);
            if (tempUser != null) return Conflict("Mobile Already In Use");


           if (ModelState.IsValid)
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
                return Ok(user);
            }
            return BadRequest(user);
        }

        [HttpGet]
        public async Task<IActionResult> Edit()
        {
            user = HttpContext.Session.GetObjectFromJson<User>("User");

            if (user == null || _context.Users == null)
            {
                return NotFound();
            }
            return View(user);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            var existingUser = _context.Users.Find(user.UID);
            if (existingUser == null) return null;
            existingUser.Email = user.Email;

            existingUser.UserName = user.UserName;
            existingUser.Address = user.Address;
            existingUser.Mobile = user.Mobile;

            try
            {
                _context.Update(existingUser);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyProfile");
            }
            catch (Exception ex)
            {
                return null;
            }



        }

        // GET: Users/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Users == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(m => m.UID == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Users == null)
            {
                return Problem("Entity set 'MyDBContext.Users'  is null.");
            }
            var user = await _context.Users.FindAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }



        [HttpGet("Login")]
        [HttpGet("")]

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(string Email, string PWD)
        {
            try
            {
                if (Email == null || PWD == null)
                {
                    return NotFound();
                }

                var user = await _context.Users.FirstOrDefaultAsync(m => (m.Email == Email || m.Mobile == Email) && m.PWD == PWD);
                //var user = await _context.Users.FirstOrDefaultAsync();
                if (user == null)
                {
                    return NotFound();
                }
                HttpContext.Session.SetString("User", JsonConvert.SerializeObject(user));

                return Ok(user);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [HttpGet("Users/Activate/{UID}")]
        public async Task<IActionResult> Activate(int UID)
        {
            var tempUser = await _context.Users.FindAsync(UID);
            if (tempUser != null)
            {
                tempUser.Status = "Active";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        [HttpGet("Users/Deactivate/{UID}")]
        public async Task<IActionResult> Deactivate(int UID)
        {
            var tempUser = await _context.Users.FindAsync(UID);
            if (tempUser != null)
            {
                tempUser.Status = "Deactive";
                await _context.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }
        public async Task<IActionResult> MyAttendance()
        {
            return View();
        }

        private bool UserExists(int id)
        {
            return (_context.Users?.Any(e => e.UID == id)).GetValueOrDefault();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using File_Access_Monitoring.Database;
using File_Access_Monitoring.Models;

namespace File_Access_Monitoring.Controllers
{
    public class AttendancesController : Controller
    {
        private readonly MyDBContext _context;
        User user;
        public AttendancesController(MyDBContext context)
        {
            _context = context;
        }

        // GET: Attendances
        public async Task<IActionResult> Index()
        {
            return _context.Attendances != null ?
                        View(await _context.Attendances.ToListAsync()) :
                        Problem("Entity set 'MyDBContext.Attendances'  is null.");
        }
        [HttpGet("Attendances/{UID}")]
        public async Task<IActionResult> Attendances(int UID)
        {
            var results = await _context.Attendances.Where(a => a.UID == UID).ToListAsync();
            foreach (var item in results)
            {
                item.Duration = new TimeSpan(item.Hours.Value, item.Minutes.Value, 0);

            }
            return View(results);
        }
        [HttpGet("Attendances/MyAttendance")]
        public async Task<IActionResult> MyAttendance()
        {
            user = HttpContext.Session.GetObjectFromJson<User>("User");
            var results = await _context.Attendances.Where(a => a.UID == user.UID).ToListAsync();
            foreach (var item in results)
            {
                item.Duration = new TimeSpan (item.Hours.Value,item.Minutes.Value,0) ;

            }
            return View(results);

        }

        // GET: Attendances/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.AID == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }
        [HttpGet("Attendances/Create")]
        public IActionResult Create()
        {
            user = HttpContext.Session.GetObjectFromJson<User>("User");
            var attendances = _context.Attendances.Where(a => a.UID == user.UID).ToList();

            var attendance = attendances.Where(a => a.CheckIn.Value.Year == DateTime.Now.Year &&
            a.CheckIn.Value.Month == DateTime.Now.Month &&
            a.CheckIn.Value.Day == DateTime.Now.Day

            ).FirstOrDefault();
                //_context.Attendances.SingleOrDefault(a => (a.CheckIn.Value).Date == DateTime.Now.Date && a.UID==user.UID);
            //var todayAttendance = new Attendance() {  };
            if (attendance != null) ViewBag.Attendance = attendance;
            else ViewBag.Attendance = new Attendance() { };
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DateTime CheckIn)
        {
            user = HttpContext.Session.GetObjectFromJson<User>("User");
            Attendance attendance = new Attendance()
            {
                CheckIn = DateTime.Now,
                UID = user.UID,
            };

            //if (ModelState.IsValid)
            {
                _context.Add(attendance);
                await _context.SaveChangesAsync();
                return Ok(attendance);
            }
            return BadRequest();
        }

        // GET: Attendances/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance == null)
            {
                return NotFound();
            }
            return View(attendance);
        }

       
        [HttpPost]
        public async Task<IActionResult> Edit(int AID,DateTime CheckOut)
        {
            CheckOut = DateTime.Now;
            var attendance = _context.Attendances.SingleOrDefault(a=>a.AID == AID);
            if(attendance==null) return BadRequest("Error");
            attendance.CheckOut = CheckOut;
            if (attendance.CheckOut != null)
            {
                attendance.Minutes = (attendance.CheckOut - attendance.CheckIn).GetValueOrDefault().Minutes;
                attendance.Hours = (attendance.CheckOut - attendance.CheckIn).GetValueOrDefault().Hours;

                try
                {
    
                    _context.Update(attendance);
                    await _context.SaveChangesAsync();
                    return Ok(attendance);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttendanceExists(attendance.AID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(attendance);
        }

        // GET: Attendances/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Attendances == null)
            {
                return NotFound();
            }

            var attendance = await _context.Attendances
                .FirstOrDefaultAsync(m => m.AID == id);
            if (attendance == null)
            {
                return NotFound();
            }

            return View(attendance);
        }

        // POST: Attendances/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Attendances == null)
            {
                return Problem("Entity set 'MyDBContext.Attendances'  is null.");
            }
            var attendance = await _context.Attendances.FindAsync(id);
            if (attendance != null)
            {
                _context.Attendances.Remove(attendance);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet("Attendances/DatewiseAttendance/{date}")]
        [HttpGet("Attendances/DatewiseAttendance/")]
        public async Task<IActionResult> DatewiseAttendance(DateTime? date)
        {
            if (date == null) date = DateTime.Now.Date;
            
            var attendances = _context.Attendances.Where(a => (a.CheckIn.Value).Date == date.Value.Date).ToList();

            foreach (var item in attendances)
            {
                item.Update();
            }
            ViewBag.date = date.Value.ToString("yyyy-MM-dd");
            return View(attendances);
        }
        private bool AttendanceExists(int id)
        {
            return (_context.Attendances?.Any(e => e.AID == id)).GetValueOrDefault();
        }
    }
}

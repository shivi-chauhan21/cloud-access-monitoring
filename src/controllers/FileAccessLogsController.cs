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
    public class FileAccessLogsController : Controller
    {
        private readonly MyDBContext _context;

        public FileAccessLogsController(MyDBContext context)
        {
            _context = context;
        }

        // GET: FileAccessLogs
        public async Task<IActionResult> Index()
        {
            return _context.FileAccessLogs != null ?
                        View(await _context.FileAccessLogs.ToListAsync()) :
                        Problem("Entity set 'MyDBContext.FileAccessLogs'  is null.");
        }
        [HttpGet("FileAccessLogs/GetSingleFileAccessLogs/{FID}")]
        public async Task<IActionResult> GetSingleFileAccessLogs(int FID)
        {
            var Link = _context.File.SingleOrDefault(f => f.FID == FID).Link;
            var Result = await _context.FileAccessLogs.Where(f => f.Link == Link).ToListAsync();

            return View( Result);
        }

        // GET: FileAccessLogs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.FileAccessLogs == null)
            {
                return NotFound();
            }

            var fileAccessLog = await _context.FileAccessLogs
                .FirstOrDefaultAsync(m => m.FALID == id);
            if (fileAccessLog == null)
            {
                return NotFound();
            }

            return View(fileAccessLog);
        }

        // GET: FileAccessLogs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FileAccessLogs/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FALID,Link,AccessOn,UID,UserName,AccessType")] FileAccessLog fileAccessLog)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fileAccessLog);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fileAccessLog);
        }

        // GET: FileAccessLogs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.FileAccessLogs == null)
            {
                return NotFound();
            }

            var fileAccessLog = await _context.FileAccessLogs.FindAsync(id);
            if (fileAccessLog == null)
            {
                return NotFound();
            }
            return View(fileAccessLog);
        }

        // POST: FileAccessLogs/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FALID,Link,AccessOn,UID,UserName,AccessType")] FileAccessLog fileAccessLog)
        {
            if (id != fileAccessLog.FALID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fileAccessLog);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileAccessLogExists(fileAccessLog.FALID))
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
            return View(fileAccessLog);
        }

        // GET: FileAccessLogs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.FileAccessLogs == null)
            {
                return NotFound();
            }

            var fileAccessLog = await _context.FileAccessLogs
                .FirstOrDefaultAsync(m => m.FALID == id);
            if (fileAccessLog == null)
            {
                return NotFound();
            }

            return View(fileAccessLog);
        }

        // POST: FileAccessLogs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.FileAccessLogs == null)
            {
                return Problem("Entity set 'MyDBContext.FileAccessLogs'  is null.");
            }
            var fileAccessLog = await _context.FileAccessLogs.FindAsync(id);
            if (fileAccessLog != null)
            {
                _context.FileAccessLogs.Remove(fileAccessLog);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FileAccessLogExists(int id)
        {
          return (_context.FileAccessLogs?.Any(e => e.FALID == id)).GetValueOrDefault();
        }
    }
}

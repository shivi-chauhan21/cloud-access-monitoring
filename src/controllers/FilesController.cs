using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using File_Access_Monitoring.Database;
using File_Access_Monitoring.Models;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.AspNetCore.Components.Routing;

namespace File_Access_Monitoring.Controllers
{
    public class FilesController : Controller
    {
        private readonly MyDBContext _context;
        private readonly IWebHostEnvironment hostingEnvironment;
        User user;
        public FilesController(IWebHostEnvironment environment, MyDBContext context)
        {
            hostingEnvironment = environment;
            _context = context;
        }

        // GET: Files
        public async Task<IActionResult> Index()
        {
            var files = await _context.File.ToListAsync();
            return View(files);
        }
        public async Task<IActionResult> MyFiles()
        {
            List<FileDoc> files = new List<FileDoc>();
            user = HttpContext.Session.GetObjectFromJson<User>("User");

            var allfiles = _context.File.ToList().ToList();
            foreach (var file in allfiles)
            {
                var UIDs = file.AuthorizedUserIDs.Trim().Split(",").ToList();
                if (UIDs.Contains(user.UID.ToString())) { files.Add(file); }
            }
            return View(files);
        }

        // GET: Files/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var file = await _context.File
                .FirstOrDefaultAsync(m => m.FID == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        public async Task<IActionResult> FileAccess(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var file = await _context.File.FirstOrDefaultAsync(m => m.FID == id);
            if (file == null)
            {
                return NotFound();
            }
            if (!string.IsNullOrEmpty(file.AuthorizedUserIDs))
                file.UIDs = file.AuthorizedUserIDs.Split(',').Select(int.Parse).ToList();
            ViewBag.Users = _context.Users.ToList();
            return View(file);
        }
        // GET: Files/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Files/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]

        public async Task<IActionResult> Create(FileDoc fileDoc, IFormFile file)
        {
            fileDoc.CreatedOn = DateTime.Now;
            fileDoc.Extension = file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);

            if (ModelState.IsValid)
            {
                _context.Add(fileDoc);
                await _context.SaveChangesAsync();
                var FID = fileDoc.FID;

                string domainName = HttpContext.Request.Host.Value;



                var fileName = FID + "." + file.FileName.Substring(file.FileName.LastIndexOf(".") + 1);
                var uploads = Path.Combine(hostingEnvironment.WebRootPath, "Files");
                var filePath = Path.Combine(uploads, fileName);



                file.CopyTo(new FileStream(filePath, FileMode.Create));

                return RedirectToAction(nameof(Index));
            }
            return View(file);
        }

        // GET: Files/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var file = await _context.File.FindAsync(id);
            if (file == null)
            {
                return NotFound();
            }
            return View(file);
        }


        [HttpPost]
        public async Task<IActionResult> Edit(FileDoc file)
        {
            var existingFile = _context.File.Find(file.FID);
            existingFile.FileName = file.FileName;


            //if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(existingFile);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FileExists(file.FID))
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
            return View(file);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateFileAccess(int FID, List<string> SelectedUIDs)
        {
            var file = _context.File.Find(FID);
            file.AuthorizedUserIDs = string.Join(",", SelectedUIDs);
            _context.Update(file);
            await _context.SaveChangesAsync();

            return Ok(file);

        }
        // GET: Files/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.File == null)
            {
                return NotFound();
            }

            var file = await _context.File
                .FirstOrDefaultAsync(m => m.FID == id);
            if (file == null)
            {
                return NotFound();
            }

            return View(file);
        }

        // POST: Files/Delete/5
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.File == null)
            {
                return Problem("Entity set 'MyDBContext.File'  is null.");
            }
            var file = await _context.File.FindAsync(id);
            if (file != null)
            {
                _context.File.Remove(file);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet, Route("Files/ResetFileLink/{FID}")]
        public async Task<IActionResult> ResetFileLink(int FID)
        {
            string domainName = Request.Scheme + "://" + Request.Host.Value;

            var file = await _context.File.FindAsync(FID);
            if (file == null) { return NotFound(null); }
            var NewLink = domainName + "/Files/Download/" + Guid.NewGuid().ToString();
            file.Link = NewLink;
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }
        [HttpGet, Route("Files/Download/{Link}")]
        public async Task<IActionResult> Download(string Link)
        {
            


            var NewLink = Request.Scheme + "://" + Request.Host.Value + "/Files/Download/" + Link;
            var file = await _context.File.SingleOrDefaultAsync(f => f.Link == NewLink);
            if (file == null) { return NotFound(null); }
            FileAccessLog fileAccessLog = new FileAccessLog() { Link = NewLink, };

            user = HttpContext.Session.GetObjectFromJson<User>("User");
            if(user == null)
            {
                fileAccessLog.UserName = "Unknown";
                fileAccessLog.UID = 0;
                fileAccessLog.AccessType = "Authorized";
                _context.FileAccessLogs.Add(fileAccessLog);
                await _context.SaveChangesAsync();
                return Unauthorized("You are not authorized for access this link");

            }


            if (file.AuthorizedUserIDs.Split(",").Select(int.Parse).ToList().Contains(user.UID))
            {
                fileAccessLog.UserName = user.UserName;
                fileAccessLog.UID = user.UID;
                fileAccessLog.AccessType = "Authorized";
                file.DownloadCount++;
                _context.FileAccessLogs.Add(fileAccessLog);
                await _context.SaveChangesAsync();
            }
            else
            {
                fileAccessLog.UserName = user.UserName;
                fileAccessLog.UID = user.UID;
                _context.FileAccessLogs.Add(fileAccessLog);
                await _context.SaveChangesAsync();
                return Unauthorized("You are not authorized for access this link");

            }



            var fileName = file.FID + "." + file.Extension;
            var path = Path.Combine(hostingEnvironment.WebRootPath, "Files", fileName);


            Stream stream = new FileStream(path, FileMode.Open);

            return File(stream, "application/octet-stream", Link + "." + file.Extension);

        }

        private bool FileExists(int id)
        {
            return (_context.File?.Any(e => e.FID == id)).GetValueOrDefault();
        }
    }
}

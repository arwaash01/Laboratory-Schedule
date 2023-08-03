using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laboratory_Schedule.Data;
using Laboratory_Schedule.Models;
using Microsoft.AspNetCore.Authorization;

namespace Laboratory_Schedule.Controllers
{
    public class RequestesController : Controller
    {

        private readonly ApplicationDbContext _context;

        public RequestesController(ApplicationDbContext context)
        {
            _context = context;
        }


        // ActionResult or IActionResult: return type for web API controller actions.
        //GET
        //filter
        [Authorize(Roles = "Admin , Recep")]
        //public async Task<IActionResult> Index(string? college, string? studentstatus)
        //{


        //    if (!string.IsNullOrEmpty(college) && !string.IsNullOrEmpty(studentstatus))
        //    {
        //        return View(await _context.Request.Where(r => r.Collage == college && r.StudentStatus == studentstatus).ToListAsync());
        //    }

        //    else if (!string.IsNullOrEmpty(college) || !string.IsNullOrEmpty(studentstatus))
        //    {
        //        return View(await _context.Request.Where(r => r.Collage == college || r.StudentStatus == studentstatus).ToListAsync());
        //    }
        //    else
        //    {
        //        return _context.Request != null ?
        //                    View(await _context.Request.ToListAsync()) :
        //                    Problem("Entity set 'ApplicationDbContext.Requests'  is null.");

        //    }


        //}
        public async Task<IActionResult> Index(string searchString , string searchBy)
        {
            if (!String.IsNullOrEmpty(searchString))
            {

                if (searchBy == "Collage")
                {
                    return View(await _context.Request.Where(s => s.Collage!.Contains(searchString)).ToListAsync());
                }
                else if (searchBy == "Student Status")
                {
                    return View(await _context.Request.Where(s => s.StudentStatus!.Contains(searchString)).ToListAsync());

                } 
            }

              return View(await _context.Request.ToListAsync());

        }

        //GET: Requestes / Create

        public IActionResult Create()
        {  
            // take collages from database
            VMStudentAndCollages vmStudentandCollages = new VMStudentAndCollages();
            var collages =_context.Collages.ToList();
            vmStudentandCollages.CollagesSelectList = new SelectList(collages, "Id", "Name");
            // show Avilable Dates
            var managment = _context.Mangement.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (managment is null)
            {
                ViewBag.ErrorMessage = "You need to set the limit in mangment page";
                return View();
            }
            var limitDays = managment.Value;

            var dateTo = DateTime.Now.AddDays(30);
            List<DateTime> avilableDates = new List<DateTime>();
            for (var date = DateTime.Now; date <= dateTo; date = date.AddDays(1))
            {
                if (date.DayOfWeek.ToString() == "Friday" || date.DayOfWeek.ToString() == "Saturday")
                {
                    continue;
                }
                var requestCount = _context.Request.Where(x => x.TestDate.Date == date.Date).Count();
                if (requestCount >= limitDays)
                {
                    continue;
                }
                avilableDates.Add(date);
            }
            ViewBag.AvailablDates = avilableDates;
           
            return View(vmStudentandCollages);
        }

        //POST : Requestes / Create 
        // create post to validate
        [HttpPost] //  method of posting client data or form data to the server. HTTP
        [ValidateAntiForgeryToken]
       
        public async Task<IActionResult> Create([Bind("Id,NationalResidenceId,UniversityNumber,StudentStatus,Collage,FirstNameEng,FatherNameEng,GrandFatherNameEng,FamilyNameEng,FirstNameArb,FatherNameArb,GrandFatherNameArb,FamilyNameArb,Email,PhoneNumber,BirthDate,MedicalFileNO,TestDate")] Request request)
        {
            VMStudentAndCollages vmStudentandCollages = new VMStudentAndCollages();
            var collages = _context.Collages.ToList();
            vmStudentandCollages.CollagesSelectList = new SelectList(collages, "Id", "Name");
            vmStudentandCollages.Request= request;

            var management = _context.Mangement.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (management is null)
            {
                ViewBag.ErrorMessage = "You need to set limit in management page";
                return View(vmStudentandCollages);
            }
            var limitDays = management.Value;
            var requestsCount = _context.Request.Where(X => X.TestDate == request.TestDate).Count();
            if (requestsCount >= limitDays)
            {
                ViewBag.ErrorMessage = "Sorry,The limit of Requests for this Day is Reached";
                return View(vmStudentandCollages);
            }
            if (ModelState.IsValid)
            {
                _context.Add(request);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Message");
            }
            return View(vmStudentandCollages);
        }
        public IActionResult Message()
        {
            return View();
        }


    }
}

﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Laboratory_Schedule.Data;
using Laboratory_Schedule.Models;



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
        public async Task<IActionResult> Index(string? college, string? studentstatus)
        {
            if (!string.IsNullOrEmpty(college) && !string.IsNullOrEmpty(studentstatus))
            {
                return View(await _context.Request.Where(r => r.Collage == college && r.StudentStatus == studentstatus).ToListAsync());
            }

            else if (!string.IsNullOrEmpty(college) || !string.IsNullOrEmpty(studentstatus))
            {
                return View(await _context.Request.Where(r => r.Collage == college || r.StudentStatus == studentstatus).ToListAsync());
            }
            else
            {
                return _context.Request != null ?
                            View(await _context.Request.ToListAsync()) :
                            Problem("Entity set 'ApplicationDbContext.Requests'  is null.");

            }


        }

        //GET: Requestes / Create
        public IActionResult Create()
        {
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
            return View();
        }

        //POST : Requestes / Create
        [HttpPost] //  method of posting client data or form data to the server. HTTP
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,NationalResidenceId,UniversityNumber,StudentStatus,Collage,FirstNameEng,FatherNameEng,GrandFatherNameEng,FamilyNameEng,FirstNameArb,FatherNameArb,GrandFatherNameArb,FamilyNameArb,Email,PhoneNumber,BirthDate,MedicalFileNO,TestDate")] Request requestes)
        {
            var management = _context.Mangement.Where(x => x.Name == "limitationDays").FirstOrDefault();
            if (management is null)
            {
                ViewBag.ErrorMessage = "You need to set limit in management page";
                return View();
            }
            var limitDays = management.Value;
            var requestsCount = _context.Request.Where(X => X.TestDate == requestes.TestDate).Count();
            if (requestsCount >= limitDays)
            {
                ViewBag.ErrorMessage = "Sorry,The limit of Requests for this Day is Reached";
                return View();
            }
            if (ModelState.IsValid)
            {
                _context.Add(requestes);
                await _context.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
                return RedirectToAction("Message");
            }
            return View(requestes);
        }
        public IActionResult Message()
        {
            return View();
        }


    }
}
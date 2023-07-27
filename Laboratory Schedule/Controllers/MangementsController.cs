using Laboratory_Schedule.Data;
using Laboratory_Schedule.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Laboratory_Schedule.Controllers
{
    public class MangementsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MangementsController(ApplicationDbContext context)
        {
            _context = context;
        }


        // GET: Mangements/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            var limitationCountResult = _context.Mangement.Where(x => x.Name == "limitationDays").FirstOrDefault();


            return View(limitationCountResult == null ? 0 : limitationCountResult.Value);
        }

        // POST: Managements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int limitationDays)
        {
            var limitationDaysObject = _context.Mangement.Where(x => x.Name == "limitationDays").FirstOrDefault();

            if (limitationDaysObject == null)
            {
                limitationDaysObject = new Mangement();
                limitationDaysObject.Name = "limitationDays";
                limitationDaysObject.Value = limitationDays;
                _context.Add(limitationDaysObject);
            }
            else
            {
                limitationDaysObject.Value = limitationDays;
            }
            _context.SaveChanges();
            //return RedirectToAction(nameof(Index),"Requests");
            return View(limitationDays);
        }
    }
}

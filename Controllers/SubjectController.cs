using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTest.Data;
using StudentTest.Dto;
using StudentTest.Models;
using StudentTest.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentTest.Controllers
{
    public class SubjectController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICrudSubject _service;
        private readonly ILogger<SubjectController> _logger;
        public SubjectController(ApplicationDbContext dbContext, ICrudSubject service, ILogger<SubjectController> logger)
        {
            _dbContext = dbContext;
            _service = service;
            _logger = logger;
        }

      
        public async Task<IActionResult> Index(string? search,int page = 1, int pageSize = 5)
        {
            try
            {
                var filter = new FilterStudentDto { Search = search, Page = page, PageSize = pageSize };
                var (subjects, total) = await _service.GetSubjectsAsync(filter);
                var totalPages = (int)Math.Ceiling(total / (double)pageSize);
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = totalPages;
                ViewBag.Search = search;

                if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                    return PartialView("_SubjectTablePartial", subjects);

                return View(subjects);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new student.");

                return Json(new { success = false, message = "Someting went wrong" });
            }
        }

        public async Task<IActionResult> Create(SubjectDto request)
        {
          
            try
            {
                var subject = await _service.AddAsync(request);
                return Json(new
                {
                    success = true,
                    message = "Subject created successfully",
                    subject = new { SubjectKey = subject.SubjectKey, SubjectName = subject.SubjectName }
                });
            }catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new student.");

                return Json(new { success = false, message = "Someting went wrong" });
            }
          

        }
        public async Task<IActionResult> Edit(int id)
        {
            
            try
            {
                var subject = await _service.GetByIdAsync(id);
                if (subject == null) return NotFound();
                return PartialView("EditSubjectPartial", subject);
            }catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new student.");

                return Json(new { success = false, message = "Someting went wrong" });

            }
        }

        [HttpPost]
        public async Task<IActionResult> Edit(EditSubjectDto model)
        {
            try
            {
                var ss = await _service.UpdateAsync(model);
               

                return Json(new { success = true, message = "Successfull update subject" });
            }catch(Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new student.");

                return Json(new { success = false, message = "Someting went wrong" });

            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int SubjectKey)
        {
           
            try
            {
                await _service.DeleteAsync(SubjectKey);
                return Json(new { success = true, message = "Subject Deleted successfully" });
                //}
            }catch(Exception ex)
            {

                _logger.LogError(ex, "An error occurred while adding a new student.");

                return Json(new { success = false, message = "Someting went wrong" });

            }

        }
    }
}


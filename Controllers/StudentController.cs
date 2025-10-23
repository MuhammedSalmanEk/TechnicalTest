using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentTest.Data;
using StudentTest.Dto;
using StudentTest.Models;
using StudentTest.Repository;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentTest.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly ICrudStudent _services;
        private readonly ILogger<StudentController> _logger;
        public  StudentController(ApplicationDbContext dbContext,ICrudStudent services,ILogger<StudentController> logger)
        {
            _dbContext = dbContext;
            _services = services;
            _logger = logger;
        }
        public async Task<IActionResult> Index(string search, string filter, int page = 1, int pageSize = 5)
        {
          
            var filterDto = new FilterStudentDto
            {
                Search = search,
                Filter = filter,
                Page = page,
                PageSize = pageSize
            };
           var (students, totalPages) =await _services.GetStudentsAsync(filterDto);
            ViewBag.CurrentPage = page;
            var totalPagess = (int)Math.Ceiling(totalPages / (double)pageSize);
            ViewBag.TotalPages = totalPagess; 
            ViewBag.Search = search;
            ViewBag.Filter = filter;
            ViewBag.Subjects = await _dbContext.MstSubjects.ToListAsync();

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
                return PartialView("_StudentTablePartial", students);

            return View(students);
        }

        public async Task<IActionResult> Create(StudentDto request)
        {
           
            try
            {
                var student = await _services.AddAsync(request);
                return Json(new { success = true, message = "Student created successfully", student });
            }catch(Exception ex){
                _logger.LogError(ex, "An error occurred while adding a new student.");

                return Json(new { success = false, message = "Sometingwent Wrong"});

            }
         }
          


        
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var data=await _services.GetByIdAsync(id);
                if (data == null)
                    return Json(new { success = false, message = "Not Found" });
                ViewBag.Subjects = await _dbContext.MstSubjects.ToListAsync();
                return PartialView("EditStudentPartial", data);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new student.");

                return Json(new { success = false, message = "Sometingwent Wrong" });

            }

        }

        public async Task<IActionResult> Edit(EditStudenttDto request)
        {
            try
            {

                var data = await _services.UpdateAsync(request);
              
                return Json(new { success = true, message = "Succesfully Updated" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new student.");

                return Json(new { success = false, message = "Sometingwent Wrong" });

            }
        }


        public async Task<IActionResult> Delete(int StudentKey)
        {
            try
            {
                 await _services.DeleteAsync(StudentKey);
                

                return Json(new { success = true, message = "Succesfully Deleted" });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while adding a new student.");

                return Json(new { success = false, message = "Sometingwent Wrong" });

            }
        }
    }
}


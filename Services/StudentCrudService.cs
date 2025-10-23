using System;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StudentTest.Data;
using StudentTest.Dto;
using StudentTest.Models;
using StudentTest.Repository;

namespace StudentTest.Services
{
    public class StudentCrudService : ICrudStudent
    {
        private readonly ApplicationDbContext _db;

        public StudentCrudService(ApplicationDbContext db) => _db = db;



        public async Task<(List<MstStudent> Students, int TotalCount)> GetStudentsAsync(FilterStudentDto filter)
        {
            var query = _db.MstStudents.Include(s => s.Subject).AsQueryable();

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(s => s.StudentName.Contains(filter.Search));

            if (!string.IsNullOrEmpty(filter.Filter))
            {
                if (filter.Filter == "PASS") query = query.Where(s => s.Grade >= 75);
                else if (filter.Filter == "FAIL") query = query.Where(s => s.Grade < 75);
            }

            int total = await query.CountAsync();
            var students = await query
                .OrderBy(s => s.StudentKey)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (students, total);
        }


        public async Task<MstStudent> GetByIdAsync(int id)
        {
            return await _db.MstStudents.Include(s => s.Subject).FirstOrDefaultAsync(s => s.StudentKey == id);
        }


        public async Task<MstStudent> AddAsync(StudentDto request)
        {
            var ss = new MstStudent
            {
                StudentName = request.StudentName,
                SubjectKey = request.SubjectKey,
                Grade = request.Grade
            };

            await _db.MstStudents.AddAsync(ss);
            await _db.SaveChangesAsync();
            return ss;
        }

        public async Task<dynamic> UpdateAsync(EditStudenttDto request)
        {
            var student = await _db.MstStudents.Include(s => s.Subject).FirstOrDefaultAsync(s => s.StudentKey == request.StudentKey);
            if (student == null)
                return false;
            student.StudentName = request.StudentName;
            student.SubjectKey = request.SubjectKey;
            student.Grade = request.Grade;
            await _db.SaveChangesAsync();
            return student;
        }

        public async Task DeleteAsync(int student)
        {
           var record = await GetByIdAsync(student);
            _db.MstStudents.Remove(record);
            await _db.SaveChangesAsync();
        }

        
    }
}


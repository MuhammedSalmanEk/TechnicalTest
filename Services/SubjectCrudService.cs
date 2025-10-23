using System;
using Azure.Core;
using Microsoft.EntityFrameworkCore;
using StudentTest.Data;
using StudentTest.Dto;
using StudentTest.Models;
using StudentTest.Repository;

namespace StudentTest.Services
{
    public class SubjectCrudService : ICrudSubject
    {
     

        private readonly ApplicationDbContext _db;
        public SubjectCrudService(ApplicationDbContext db) => _db = db;

        public async Task<(List<MstSubject> Subjects, int TotalCount)> GetSubjectsAsync(FilterStudentDto filter)
        {


            var query = _db.MstSubjects.AsQueryable();

            if (!string.IsNullOrEmpty(filter.Search))
                query = query.Where(s => EF.Functions.ILike(s.SubjectName, $"%{filter.Search}%"));

            int total = await query.CountAsync();

            var subjects = await query
                .OrderBy(s => s.SubjectKey)
                .Skip((filter.Page - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            return (subjects, total);
        }


        public async Task<MstSubject> GetByIdAsync(int id) =>
            await _db.MstSubjects.FirstOrDefaultAsync(s => s.SubjectKey == id);


        public async Task<MstSubject> AddAsync(SubjectDto subject)
        {
            var data = new MstSubject
            {
                SubjectName = subject.SubjectName,

            };

            await _db.MstSubjects.AddAsync(data);
            await _db.SaveChangesAsync();
            return data;
        }


        public async Task<dynamic> UpdateAsync(EditSubjectDto subject)
        {
            var data = await GetByIdAsync(subject.SubjectKey);
            data.SubjectName = subject.SubjectName;
                await _db.SaveChangesAsync();
            return data;
        }


        public async Task DeleteAsync(int Id)
        {
            var data = await GetByIdAsync(Id);
            _db.MstSubjects.Remove(data);
            await _db.SaveChangesAsync();
            
        }

        
    }
}

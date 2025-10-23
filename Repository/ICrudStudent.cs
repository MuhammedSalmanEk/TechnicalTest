using System;
using StudentTest.Dto;
using StudentTest.Models;

namespace StudentTest.Repository
{
    public interface ICrudStudent
    {
        //Task<dynamic> GetStudent(FilterRoles filter);


        Task<(List<MstStudent> Students, int TotalCount)> GetStudentsAsync(FilterStudentDto filter);
        Task<MstStudent> GetByIdAsync(int id);
        Task<MstStudent> AddAsync(StudentDto student);
        Task<dynamic> UpdateAsync(EditStudenttDto student);
        Task DeleteAsync(int student);
    }
 
}


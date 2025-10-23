using System;
using StudentTest.Dto;
using StudentTest.Models;

namespace StudentTest.Repository
{
	public interface ICrudSubject
	{
        Task<(List<MstSubject> Subjects, int TotalCount)> GetSubjectsAsync(FilterStudentDto filter);
        Task<MstSubject> GetByIdAsync(int id);
        Task<MstSubject> AddAsync(SubjectDto subject);
        Task<dynamic>UpdateAsync(EditSubjectDto subject);
        Task DeleteAsync(int Id);
    }
}


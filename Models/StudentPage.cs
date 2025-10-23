using System;
namespace StudentTest.Models
{


    public class FilterStudentDto
    {
        public string? Search { get; set; }
        public string? Filter { get; set; } 
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }

   

}


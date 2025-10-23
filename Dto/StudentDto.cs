using System;
using StudentTest.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTest.Dto
{
	public class StudentDto
	{

        [Required]
        [MaxLength(50)]
        public string StudentName { get; set; }

        [Required]
        public int SubjectKey { get; set; }


        [Range(0, 100)]
        public int Grade { get; set; }

    }

    public class EditStudenttDto
    {
        public int StudentKey { get; set; }

        public string StudentName { get; set; }

        public int SubjectKey { get; set; }


        public int Grade { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTest.Dto
{
	public class SubjectDto
	{


        [Required]
        [StringLength(50)]
        public string SubjectName { get; set; }
    }

    public class CreateSubjectDto
    {

        [Required]
        [StringLength(50)]
        public string SubjectName { get; set; }
    }


    public class EditSubjectDto
    {
        public int SubjectKey { get; set; }
      
        public string SubjectName { get; set; }
    }
}


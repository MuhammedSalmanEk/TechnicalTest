using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace StudentTest.Models
{
	public class MstStudent
	{
		[Key]
		
		public int StudentKey { get;set;}

		[Required]
		[MaxLength(50)]
		public  string StudentName {get;set;}

		[Required]
        [ForeignKey(nameof(MstSubject))]
        public int SubjectKey { get; set; }

		public MstSubject Subject { get; set; }

		[Range(0,100)]
		public int Grade { get; set; }

		//public string? Remarks { get; set; }
        [NotMapped]
        public string Remarks => Grade >= 75 ? "PASS" : "FAIL";
    }
}


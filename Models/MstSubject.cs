using System;
using System.ComponentModel.DataAnnotations;

namespace StudentTest.Models
{
	public class MstSubject
	{
		[Key]
		public int SubjectKey { get; set; }

		[Required]
        [StringLength(50)]
        public string SubjectName { get; set; }

        public ICollection<MstStudent> MsStudent { get; }

    }
}


using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Blog.Models
{
	public class BlogPost
	{
        
		[Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(Order=1, TypeName="serial")]        
		public int PostId { get; set; }
		[Required]
		public string Creator { get; set; }
		[Required]
		public string Title { get; set; }
		[Required]
		public string Body { get; set; }
		[Required]
		public DateTime Dt { get; set; }
	}
}

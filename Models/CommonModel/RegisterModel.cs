using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using contactForm.Enum;

namespace contactForm.Models.CommonModel
{
	public class RegisterModel
	{

		[Required]
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[Required]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

		[Required]
		[EmailAddress]
		[DisplayName("Email Address")]
		public string EmailAddress { get; set; }

		[Required]
		[DisplayName("Query Type")]
		public QueryType QueryType { get; set; }

		[Required]
        [DisplayName("Message")]
        public string Message { get; set; }
	}
}


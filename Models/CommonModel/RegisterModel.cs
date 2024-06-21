using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using contactForm.Enum;

namespace contactForm.Models.CommonModel
{
	public class RegisterModel
	{

		[Required(ErrorMessage = "First Name is required!")]
		[DisplayName("First Name")]
		public string FirstName { get; set; }

		[Required(ErrorMessage = "Last Name is required!")]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

		[Required(ErrorMessage = "Email is required!")]
		[EmailAddress (ErrorMessage = "The field must be in the email!")]
		[DisplayName("Email Address")]
		public string EmailAddress { get; set; }

		[Required (ErrorMessage = "Query Type is required!")]
		[DisplayName("Query Type")]
		public QueryType? QueryType { get; set; }

		[Required (ErrorMessage = "Message is required!")]
        [DisplayName("Message")]
        public string Message { get; set; }
	}
}


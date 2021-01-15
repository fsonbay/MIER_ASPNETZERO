using System.ComponentModel.DataAnnotations;

namespace DDM.Web.Models.Account
{
    public class SendPasswordResetLinkViewModel
    {
        [Required]
        public string EmailAddress { get; set; }
    }
}
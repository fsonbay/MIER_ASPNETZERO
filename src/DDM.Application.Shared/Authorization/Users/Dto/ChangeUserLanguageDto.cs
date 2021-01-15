using System.ComponentModel.DataAnnotations;

namespace DDM.Authorization.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}

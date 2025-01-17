﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace VisitorRegistrationSystem.Domain.DTOs.UserDTOs
{
    public class UserPasswordChangeDto
    {
        [DisplayName("Şuanki Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        [DataType(DataType.Password)]
        public string CurrentPassword { get; set; }


        [DisplayName("Yeni Şifreniz")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        [DataType(DataType.Password)]
        public string NewPassword { get; set; }

        [DisplayName("Yeni Şifrenizin Tekrarı")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(5, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Girmiş olduğunuz Yeni Şifreniz ile Yeni Şifrenizin tekrarı alanları birbiriyle aynı olmalıdır.")]
        public string RepeatPassword { get; set; }


    }
}

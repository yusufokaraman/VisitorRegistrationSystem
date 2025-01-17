﻿using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VisitorRegistrationSystem.Common.Domain.Entity;

namespace VisitorRegistrationSystem.Domain.DTOs.UserDTOs
{
    public class UserUpdateDto : DtoGetBase
    {
        [Required]
        public int Id { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        public string UserName { get; set; }

        [DisplayName("E-Posta Adresi")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(100, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }



        [DisplayName("İletişim No")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(7, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(7, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        [DisplayName("Resim Ekle")]

        [DataType(DataType.Upload)]
        public IFormFile PictureFile { get; set; }

        [DisplayName("Resim")]
        public string Picture { get; set; }
    }
}

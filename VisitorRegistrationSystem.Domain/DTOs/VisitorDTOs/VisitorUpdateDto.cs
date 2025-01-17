﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VisitorRegistrationSystem.Common.Domain.Entity;

namespace VisitorRegistrationSystem.Domain.DTOs.VisitorDTOs
{
    public class VisitorUpdateDto : DtoGetBase
    {

        [Required]
        public int Id { get; set; }


        [DisplayName("Tc Kimlik No")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(11, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(11, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        public long TcNo { get; set; }


        [DisplayName("Adı")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        public string FirstName { get; set; }

        [DisplayName("Soyadı")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(30, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        public string LastName { get; set; }

        [DisplayName("Doğum Tarihi")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [DisplayFormat(DataFormatString = "{0:d}", NullDisplayText = "Dogum Tarihi Girilmemiş")]
        public DateTime BirthDate { get; set; }

        [DisplayName("İletişim Numarası")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(10, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(10, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        public long ContactNo { get; set; }

        [DisplayName("Birim")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        public int DepartmentId { get; set; }

        public bool IsExit { get; set; }
        public DateTime EnterDate { get; set; }
        public DateTime OutDate { get; set; }


    }
}

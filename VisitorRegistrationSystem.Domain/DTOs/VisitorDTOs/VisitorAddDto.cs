﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Domain.DTOs.VisitorDTOs
{
    public class VisitorAddDto : DtoGetBase
    {
        [DisplayName("Tc Kimlik No")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(11, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(11, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        public string TcNo { get; set; }


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
        [MaxLength(11, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(11, ErrorMessage = "{0} {1} karakterden küçük olmamalıdır")]
        public string ContactNo { get; set; }

        [DisplayName("Birimler")]

        public int DepartmentId { get; set; }

        public IList<Department> Departments { get; set; }

    }
}

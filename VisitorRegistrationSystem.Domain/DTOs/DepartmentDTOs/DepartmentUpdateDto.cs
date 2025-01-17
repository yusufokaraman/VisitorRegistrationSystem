﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using VisitorRegistrationSystem.Common.Domain.Entity;

namespace VisitorRegistrationSystem.Domain.DTOs.DepartmentDTOs
{
    public class DepartmentUpdateDto : DtoGetBase
    {
        [Required]
        public int Id { get; set; }


        [DisplayName("Bölüm Adı")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(50, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(3, ErrorMessage = "{0} {1} karakterden az olmamalıdır")]
        public string Name { get; set; }

        [DisplayName("İletişim Numarası")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        [MaxLength(7, ErrorMessage = "{0} {1} karakterden büyük olamamalıdır")]
        [MinLength(1, ErrorMessage = "{0} {1} karakterden az olmamalıdır")]
        public string CountactNo { get; set; }

        [DisplayName("Aktif Mi?")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        public bool IsActive { get; set; }

        [DisplayName("Silindi Mi?")]
        [Required(ErrorMessage = "{0} boş geçilememelidir.")]
        public bool IsDeleted { get; set; }

    }
}

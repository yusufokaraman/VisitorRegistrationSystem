using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VisitorRegistrationSystem.Common.Domain.Entity;
using VisitorRegistrationSystem.Domain.Entitiy;

namespace VisitorRegistrationSystem.Domain.DTOs.UserDTOs
{
    public class UserDto : DtoGetBase
    {
        public User User { get; set; }
    }
}

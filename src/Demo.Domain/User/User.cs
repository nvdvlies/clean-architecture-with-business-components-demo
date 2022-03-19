using Demo.Domain.Shared.Entities;
using Demo.Domain.Shared.Interfaces;
using System;
using System.Collections.Generic;

namespace Demo.Domain.User
{
    public partial class User : SoftDeleteEntity, IQueryableEntity
    {
        public string Fullname { get; set; }
        public string GivenName { get; set; }
        public string FamilyName { get; set; }
        public string MiddleName { get; set; }
        public string Email { get; set; }
        public Gender? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public string ZoneInfo { get; set; }
        public string Locale { get; set; }
        public List<UserRole> UserRoles { get; set; }
    }
}
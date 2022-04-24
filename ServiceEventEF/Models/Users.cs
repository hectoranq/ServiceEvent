using System;
using System.Collections.Generic;

namespace ServiceEventEF.Models
{
    public partial class Users
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string CryptedPassword { get; set; }
        public string PasswordSalt { get; set; }
        public string PersistenceToken { get; set; }
        public int LoginCount { get; set; }
        public string Role { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string State { get; set; }
        public string Surname { get; set; }
        public int? CompanyId { get; set; }
        public DateTime? LastRequestAt { get; set; }
        public DateTime? LastLoginAt { get; set; }
        public DateTime? CurrentLoginAt { get; set; }
        public string LastLoginIp { get; set; }
        public string CurrentLoginIp { get; set; }
        public DateTime? CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public int? GroupId { get; set; }
        public int? ConcesionariaId { get; set; }
        public bool? InReportMail { get; set; }
        public string SingleAccessToken { get; set; }
    }
}

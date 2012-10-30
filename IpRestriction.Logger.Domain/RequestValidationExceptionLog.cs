using System;
using System.ComponentModel.DataAnnotations;

namespace IpRestriction.Logger.Domain
{
    public class RequestValidationExceptionLog
    {
        [Key]
        public int Id { get; set; }
        public string IpAddress { get; set; }
        public string Page { get; set; }        
        public DateTime LogTime { get; set; }
    }
}

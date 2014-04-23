
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Pass.Processing.Web.Models
{
    public class LogInformation
    {
        [Required]
        public List<string> Logs { get; set; }
    }
}
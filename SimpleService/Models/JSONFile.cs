using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleService.Models
{
    public class JSONFile
    {
        public int Id { get; set; }

        [Required]
        public string File { get; set; }
    }
}
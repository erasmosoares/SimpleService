using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleService.Models
{
    public class MainObject
    {
        public byte Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? Date { get; set; }

    }
}
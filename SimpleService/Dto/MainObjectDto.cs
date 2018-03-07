using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleService.Dto
{
    public class MainObjectDto
    {
        public byte Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? Date { get; set; }
    }
}
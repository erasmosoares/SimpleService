using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleService.Dto
{
    /// <summary>
    /// Domain Transfer Objects
    /// </summary>
    public class MainObjectDto
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? Date { get; set; }
    }
}
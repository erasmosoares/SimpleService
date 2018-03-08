using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SimpleService.Models
{
    /// <summary>
    /// Simple Class for test purpose
    /// </summary>
    public class MainObject
    {
        public int Id { get; set; }

        [StringLength(255)]
        public string Name { get; set; }

        public DateTime? Date { get; set; }

    }
}
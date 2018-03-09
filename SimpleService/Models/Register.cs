using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SimpleService.Models
{
    /// <summary>
    /// Log register
    /// </summary>
    public class Register
    {
        [Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        
        public string Left { get; set; }

        public string Right { get; set; }

        public string Result { get; set; }
    }
}
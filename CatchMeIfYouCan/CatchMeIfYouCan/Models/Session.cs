using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace CatchMeIfYouCan.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        [Display(Name = "Runner")]
        public string RunnerName { get; set; }
        [Required]
        public int Distance { get; set; }
        [Required]
        [Range(1,31)]
        public int Day { get; set; }
        [Required]
        public string Month { get; set; }
        [Required]
        [Range(2000,3000)]
        public int Year { get; set; }


    }
}
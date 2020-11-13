using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SSMCPushNotificationCenter.Models
{
    public class tb_notification
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please fill the title field")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please fill the message field")]
        public string Message { get; set; }
        
        public string Status { get; set; }

        [Required(ErrorMessage = "Please choose image first")]
        public byte[] Image { get; set; }
    }
}
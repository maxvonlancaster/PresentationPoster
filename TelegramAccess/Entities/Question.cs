
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace TelegramAccess.Entities
{
    public class Question
    {
        [Key]
        public int Id { get; set; }
        public Presentation Presentation { get; set; }
        public string Sentence { get; set; }
        public string Options { get; set; }
        public int Correct { get; set; }
        public int Slide { get; set; }
    }
}

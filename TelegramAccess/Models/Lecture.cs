using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramAccess.Models
{
    public class Lecture
    {
        public int Id { get; set; }
        public string Lecturer { get; set; }
        public string LectureTheme { get; set; }
        public string Link { get; set; }
    }
}

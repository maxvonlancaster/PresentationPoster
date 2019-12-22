using System;
using System.Collections.Generic;
using System.Text;
using TelegramAccess.Models;

namespace TelegramAccess.Interfaces
{
    public interface ILectureService
    {
        Lecture GetLecture(int id);
        List<Lecture> GetLectures();
    }
}

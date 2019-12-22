using System;
using System.Collections.Generic;
using System.Text;
using TelegramAccess.Interfaces;
using TelegramAccess.Models;
using TelegramAccess.Constants;

namespace TelegramAccess.Services
{
    public class LectureService : ILectureService
    {
        public List<Lecture> GetLectures() 
        {
            int n = Const.Lectures.Length;
            List<Lecture> lectures = new List<Lecture>();
            for (int i = 0; i < n; i++) 
            {
                lectures.Add(new Lecture() 
                { 
                    Id = i,
                    Lecturer = Const.Lecturers[i],
                    LectureTheme = Const.Lectures[i],
                    Link = Const.Links[i]
                });
            }
            return lectures;
        }

        public Lecture GetLecture(int id) 
        {
            Lecture lecture = new Lecture() 
            {
                Id = id,
                Lecturer = Const.Lecturers[id],
                LectureTheme = Const.Lectures[id],
                Link = Const.Links[id]
            };
            return lecture;
        }
    }
}

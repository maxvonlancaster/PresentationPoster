using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramAccess.Models
{
    public class ImageHolder : IImageHolder
    {
        private List<byte[]> _images;
        private int _current;
        private Guid _guid; // may be a bad design idea; INCAPSULATE!
        private string message;

        public List<byte[]> Images { get => _images; set => _images = value; }
        public int Current { get => _current; set => _current = value; }
        public Guid Guid { get => _guid; set => _guid = value; }
        public string Message { get => message; set => message = value; }

        public byte[] GetImage()
        {
            return _images[_current];
        }
    }
}

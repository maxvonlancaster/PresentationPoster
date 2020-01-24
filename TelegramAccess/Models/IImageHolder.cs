using System;
using System.Collections.Generic;
using System.Text;

namespace TelegramAccess.Models
{
    public interface IImageHolder
    {
        List<byte[]> Images { get; set; }
        int Current { get; set; }
        Guid Guid { get; set; }

        byte[] GetImage();
    }
}

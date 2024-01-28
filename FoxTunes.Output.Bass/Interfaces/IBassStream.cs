﻿using ManagedBass;

namespace FoxTunes.Interfaces
{
    public interface IBassStream
    {
        IBassStreamProvider Provider { get; }

        int ChannelHandle { get; }

        long Length { get; }

        long Position { get; set; }

        bool IsEmpty { get; }

        Errors Errors { get; }

        event AsyncEventHandler Ending;

        event AsyncEventHandler Ended;
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Online
{
    public interface IIPOSyncService
    {
        Task<int> SyncAsync(
            CancellationToken cancellationToken = default);
    }
}

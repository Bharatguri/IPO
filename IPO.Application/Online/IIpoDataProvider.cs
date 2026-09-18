using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Online
{
    public interface IIpoDataProvider
    {
        Task<List<Domain.Entities.IPO>> GetIPOsAsync(
            CancellationToken cancellationToken = default);
    }
}

using IPO.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace IPO.Application.Online
{
    public class IPOSyncService : IIPOSyncService
    {
        private readonly IIpoDataProvider _dataProvider;
        private readonly IIPORepository _repository;

        public IPOSyncService(
            IIpoDataProvider dataProvider,
            IIPORepository repository)
        {
            _dataProvider = dataProvider;
            _repository = repository;
        }

        public async Task<int> SyncAsync(
            CancellationToken cancellationToken = default)
        {
            var externalIpos =
                await _dataProvider.GetIPOsAsync(cancellationToken);

            var existingIpos =
                await _repository.GetAllAsync();

            var changes = 0;

            foreach (var externalIpo in externalIpos)
            {
                var existing = existingIpos.FirstOrDefault(x =>
                    !string.IsNullOrWhiteSpace(x.Symbol) &&
                    x.Symbol.Equals(
                        externalIpo.Symbol,
                        StringComparison.OrdinalIgnoreCase));

                if (existing == null)
                {
                    await _repository.AddAsync(externalIpo);
                    changes++;
                    continue;
                }

                existing.CompanyName = externalIpo.CompanyName;
                existing.OpenDate = externalIpo.OpenDate;
                existing.CloseDate = externalIpo.CloseDate;
                existing.ListingDate = externalIpo.ListingDate;
                existing.MinPrice = externalIpo.MinPrice;
                existing.MaxPrice = externalIpo.MaxPrice;
                existing.LotSize = externalIpo.LotSize;
                existing.IssueSize = externalIpo.IssueSize;
                existing.Type = externalIpo.Type;
                existing.Status = externalIpo.Status;
                existing.ImageUrl = externalIpo.ImageUrl;

                await _repository.UpdateAsync(existing);

                changes++;
            }

            return changes;
        }
    }
}

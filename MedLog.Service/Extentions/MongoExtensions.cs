using MedLog.Domain.Common;
using MedLog.Domain.Configurations;
using MongoDB.Driver;
using MongoDB.Driver.Linq;

namespace MedLog.Service.Extensions
{
    public static class MongoExtensions
    {
        public static async Task<PaginationResult<TDocument>> ToPagedListAsync<TDocument>(
            this IMongoQueryable<TDocument> source, PaginationParams @params) where TDocument : Auditable
        {
            int count = await source.CountAsync();
            var data = await source.Skip((@params.PageIndex - 1) * @params.PageSize)
                                   .Take(@params.PageSize)
                                   .ToListAsync();

            return new PaginationResult<TDocument>(data, count, @params.PageIndex, @params.PageSize);
        }
    }
}

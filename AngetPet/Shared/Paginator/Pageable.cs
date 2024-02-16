using Microsoft.EntityFrameworkCore;

namespace AngetPet.Shared.Paginator
{
    public class Pageable<T> where T : class
    {
        public List<T> Data { get; set; }
        public int Index { get; set; }
        public int Count { get; set; }
        public int TotalCount { get; set; }
        public int TotalPage { get; set; }

        public Pageable(List<T> data, int totalCount, Page page)
        {
            Data = data;
            TotalCount = totalCount;
            TotalPage = (int)Math.Ceiling((double)totalCount / page.Count);
            Count = page.Count;
            Index = page.Index;
        }

        public static async Task<Pageable<T>> ConvertPageable(IQueryable<T> query, Page page)
        {
            int count = query.Count();
            List<T> resource = new List<T>();
            if (count > 0)
            {
                resource = await query.Skip(page.Index-1).Take(page.Count).ToListAsync();
            }
            return new Pageable<T>(resource, count, page);
        }

    }
}

namespace AngetPet.Shared.Paginator
{
    public class Page
    {
        public Page(int index, int count)
        {
            Index = index;
            Count = count;
        }

        public int Index { get; set; }
        public int Count { get; set; }
    }
}

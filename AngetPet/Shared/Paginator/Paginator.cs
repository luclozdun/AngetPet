namespace AngetPet.Shared.Paginator
{
    public class Paginator<T>
    {
        public Page Page { get; set; }
        public T Body { get; set; }
    }
}

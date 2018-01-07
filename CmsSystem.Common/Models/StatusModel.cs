namespace CmsSystem.Common.Models
{
    public class StatusModel<T>
    {
        public string Name { get; set; }

        public T Value { get; set; }
    }
}
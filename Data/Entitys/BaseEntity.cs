namespace asu_management.mvc.Data
{
    public abstract class BaseEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
namespace T_HosCase.Entities
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public int ParentCategoryId { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate {  get; set; }
        public int CreatorUserId { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}

namespace T_HosCase.Entities
{
	public class Product
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int CategoryId { get; set; }
		public Category Category { get; set; }
		public decimal Price { get; set; }
		public string ImagePath { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatorUserId { get; set; }
		public ICollection<ProductProperty> Properties { get; set; }
		
	}
}

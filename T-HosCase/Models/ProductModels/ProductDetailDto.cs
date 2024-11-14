namespace T_HosCase.Models.ProductModels
{
	public class ProductDetailDto
	{
		public int ProductId { get; set; }
		public string ProductName { get; set; }
		public int CategoryId { get; set; }
		public decimal Price { get; set; }
		public string ImagePath { get; set; }
	}
}

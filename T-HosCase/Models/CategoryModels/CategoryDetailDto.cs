namespace T_HosCase.Models.CategoryModels
{
	public class CategoryDetailDto
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int ParentCategoryId { get; set; }
		public bool IsDeleted { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatorUserId { get; set; }
	}
}

namespace T_HosCase.Models.CategoryModels
{
	public class EditCategoryModel
	{
		public int CategoryId { get; set; }
		public string CategoryName { get; set; }
		public int ParentCategoryId { get; set; }
	}
}

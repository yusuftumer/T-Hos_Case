﻿namespace T_HosCase.Models.CategoryModels
{
	public class AddCategoryModel
	{
		public string CategoryName { get; set; }
		public int ParentCategoryId { get; set; }
		public DateTime CreatedDate { get; set; }
		public int CreatorUserId { get; set; }
	}
}
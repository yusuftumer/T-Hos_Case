﻿namespace T_HosCase.Models.ProductModels
{
	public class AddProductModel
	{
		public string ProductName { get; set; }
		public int CategoryId { get; set; }
		public decimal Price { get; set; }
		public string ImagePath { get; set; }
	}
}

﻿namespace OnlineShop.Core.Models
{
	public class Basket
	{
		public int? Id { get; }
		public int UsertId { get; }
		public User User { get; }
		public List<BasketItem> Items { get; }
		public bool isCompleted { get; }

		public Basket() { }

		public Basket(int userId)
		{
			UsertId = userId;
			isCompleted = false;
		}
	}
}
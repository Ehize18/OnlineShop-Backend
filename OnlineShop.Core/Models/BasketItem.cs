﻿namespace OnlineShop.Core.Models
{
	public class BasketItem
	{
		public int? Id { get; }
		public int BasketId { get; }
		public Basket Basket { get; }
		public int GoodId { get; }
		public Good Good { get; }
		public int Count { get; private set; }
		public DateTime CreatedAt { get; }
		public DateTime UpdatedAt { get; }

		public BasketItem() { }

		public BasketItem(int basketId, int goodId)
		{
			BasketId = basketId;
			GoodId = goodId;
			Count = 1;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public void IncreaseCount()
		{
			Count++;
		}

		public void DecreaseCount() 
		{ 
			Count--; 
		}
	}
}

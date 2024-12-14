using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Core.Models
{
	public class DeliveryMethod
	{
		public int? Id { get; }
		public string Title { get; }
		public string Description { get; }
		public DateTime CreatedAt { get; }
		public DateTime UpdatedAt { get; }

		public DeliveryMethod() { }

		public DeliveryMethod(string title, string description, DateTime createdAt, DateTime updatedAt)
		{
			Title = title;
			Description = description;
			CreatedAt = createdAt;
			UpdatedAt = updatedAt;
		}

		public DeliveryMethod(string title, string description)
		{
			Title = title;
			Description = description;
			CreatedAt = DateTime.UtcNow;
			UpdatedAt = CreatedAt;
		}

		public DeliveryMethod(int id, string title, string description)
		{
			Id = id;
			Title = title;
			Description = description;
		}
	}
}

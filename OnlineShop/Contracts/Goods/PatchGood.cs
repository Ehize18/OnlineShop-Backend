namespace OnlineShop.Contracts.Goods
{
	public class PatchGood : PatchBase
	{
		public string? Name { get; set; }
		public string? Description { get; set; }
		public int? Price { get; set; }
		public int? CategoryId { get; set; }
	}
}

namespace OnlineShop.Contracts.GoodCategories
{
	public class PatchCategory : PatchBase
	{
		public string? Title { get; set; }
		public string? Description { get; set; }
		public int? ParentId { get; set; }
	}
}

namespace OnlineShop.Contracts
{
	public abstract class PatchBase
	{
		private HashSet<string> Properties {  get; set; } = new();

		public bool IsFieldPresent(string fieldName)
		{
			return Properties.Contains(fieldName.ToLowerInvariant());
		}

		public void SetHasField(string fieldName)
		{
			Properties.Add(fieldName.ToLowerInvariant());
		}
	}
}

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System.Reflection;

namespace OnlineShop.Contracts
{
	public class PatchRequestContractResolver : DefaultContractResolver
	{
		protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
		{
			var property = base.CreateProperty(member, memberSerialization);

			property.SetIsSpecified += (o, o1) =>
			{
				if (o is PatchBase patchBase)
				{
					patchBase.SetHasField(property.PropertyName);
				}
			};
			return property;
		}
	}
}

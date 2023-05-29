using DocumentFormat.OpenXml.Wordprocessing;
using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi
{
	public class PublicApi_StoryTagModel
	{
		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "label")]
		public string Label { get; set; }
	}
}

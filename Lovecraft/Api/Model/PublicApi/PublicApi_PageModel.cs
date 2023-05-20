using DocumentFormat.OpenXml.Wordprocessing;
using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi;

public class PublicApi_PageModel
{
	[DataMember(Name = "id")]
	public int Id { get; set; }

	[DataMember(Name = "order")]
	public int Order { get; set; }

	[DataMember(Name = "content")]
	public string Content { get; set; }

	[DataMember(Name = "lastUpdatedDateTime")]
	public DateTime LastUpdatedDateTime { get; set; }

	[DataMember(Name = "StoryId")]
	public int StoryId { get; set; }
	
}
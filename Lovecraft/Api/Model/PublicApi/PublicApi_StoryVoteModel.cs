using DocumentFormat.OpenXml.Wordprocessing;
using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi;

public class PublicApi_StoryVoteModel
{
	[DataMember(Name = "storyId")]
	public int StoryId { get; set; }

	[DataMember(Name = "count")]
	public int Count { get; set; }

	[DataMember(Name = "storyName")]
	public string StoryName { get; set; }
}
using DocumentFormat.OpenXml.Wordprocessing;
using Lovecraft.Model.PublicApi;
using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi;

public class PublicApi_StoryCommentModel
{
	[DataMember(Name = "id")]
	public int Id { get; set; }

	[DataMember(Name = "text")]
	public string Text { get; set; }

	[DataMember(Name = "storyId")]
	public int storyId { get; set; }

	[DataMember(Name = "dateCreated")]
	public DateTime DateCreated { get; set; }

	[DataMember(Name = "user")]
	public PublicApi_UserModel? User { get; set; }

}
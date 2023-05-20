using DocumentFormat.OpenXml.Wordprocessing;
using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi
{
	public class PublicApi_EventModel
	{
		[DataMember(Name = "id")]
		public int Id { get; set; }

		[DataMember(Name = "title")]
		public string Title { get; set; }

		[DataMember(Name = "rules")]
		public string Rules { get; set; }

		[DataMember(Name = "coverUrl")]
		public string CoverUrl { get; set; }

		[DataMember(Name = "dateBegin")]
		public DateTime DateBegin { get; set; }

		[DataMember(Name = "dateEnd")]
		public DateTime DateEnd { get; set; }

		[DataMember(Name = "nbStories")]
		public int NbStories { get; set; }
	}
}

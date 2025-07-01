using System.Runtime.Serialization;
using static Lovecraft.Api.Model.StoryHistory;

namespace Lovecraft.Api.Model.PublicApi
{
    public class PublicApi_StoryHistoryModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "userId")]
        public int UserId { get; set; }
        
        [DataMember(Name = "storyId")]
        public int StoryId { get; set; }

        [DataMember(Name = "lastPageReadId")]
        public int LastPageReadId { get; set; }

        [DataMember(Name = "reread")]
        public int Reread { get; set; }


        [DataMember(Name = "date")]
        public DateTime Date { get; set; }

        [DataMember(Name = "historyStateValue")]
        public HistoryState HistoryStateValue { get; set; }

        [DataMember(Name = "story")]
        public PublicApi_StoryModel ?Story { get; set; }

        [DataMember(Name = "progression")]
        public PublicApi_ProgressionModel ?Progression { get; set; }
    }
}

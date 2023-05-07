using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi
{
    [DataContract]
    public class PublicApi_UserTeamModel
    {
        [DataMember(Name = "userId")]
        public int UserId { get; set; }

        [DataMember(Name = "teamId")]
        public int TeamId { get; set; }
    }
}

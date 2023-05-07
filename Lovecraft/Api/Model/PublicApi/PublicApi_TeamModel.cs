using System.Runtime.Serialization;
using Lovecraft.Model.PublicApi;

namespace Lovecraft.Api.Model.PublicApi
{
    [DataContract]
    public class PublicApi_TeamModel
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "dateCreated")]
        public DateTime DateCreated { get; set; }

        [DataMember(Name = "teamLogoUrl")]
        public string? TeamLogoUrl { get; set; }

        [DataMember(Name="members")]
        public List<PublicApi_UserModel>? Members { get; set; }
    }
}

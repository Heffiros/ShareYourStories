using System.Runtime.Serialization;

namespace Lovecraft.Api.Model.PublicApi
{
    public class PublicApi_ProgressionModel
    {
        [DataMember(Name = "currentPagesIndex")]
        public int CurrentPagesIndex { get; set; }

        [DataMember(Name = "maxNbPages")]
        public int MaxNbPages { get; set; }
    }
}

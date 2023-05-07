using System.Runtime.Serialization;

namespace Lovecraft.Model.PublicApi;

public class PublicApi_JwtTokenModel
{
    [DataMember(Name = "userId")]
    public int UserId { get; set; }

    [DataMember(Name = "token")]
    public string Token { get; set; }
}
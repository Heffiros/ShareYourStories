using System.Runtime.Serialization;

namespace Lovecraft.Model.PublicApi;

public class PublicApi_JwtTokenModel
{
    [DataMember(Name = "userId")]
    public int UserId { get; set; }

    [DataMember(Name = "access_token")]
    public string AccessToken { get; set; }

    [DataMember(Name = "refresh_token")]
    public string RefreshToken { get; set; }
}
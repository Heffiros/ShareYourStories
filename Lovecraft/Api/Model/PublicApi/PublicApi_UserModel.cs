using System.Runtime.Serialization;

namespace Lovecraft.Model.PublicApi;


[DataContract]
public class PublicApi_UserModel
{
    [DataMember(Name = "id")]
    public int Id { get; set; }

    [DataMember(Name = "email")]
    public string Email { get; set; }

    [DataMember(Name = "authorName")]
    public string? AuthorName { get; set; }
    
    [DataMember(Name = "profilePictureUrl")]
    public string? ProfilePictureUrl { get; set; }

    [DataMember(Name = "password")]
    public string? Password { get; set; }

    [DataMember(Name = "dateCreated")]
    public DateTime DateCreated { get; set; } 

    [DataMember(Name = "birthDate")]
    public DateTime? BirthDate { get; set; }

}
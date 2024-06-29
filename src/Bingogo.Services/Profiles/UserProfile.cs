using Bingogo.Data.Entities;
using Bingogo.Models;

namespace Bingogo.Services.Profiles;

public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserModel>().ReverseMap();
    }
}

using GeoPet.Controllers.TypesReq;
using GeoPet.Models;

namespace GeoPet.Services.UserService;

public interface IUserService
{
    Task<User> CreateUser(ReqUser request);
    User FindUser (AuthUser request);
}
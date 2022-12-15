using GeoPet.Controllers.TypesReq;
using GeoPet.Models;

namespace GeoPet.Utils;

public interface IJwtTokenManager
{
    string Authenticate(User user);
}
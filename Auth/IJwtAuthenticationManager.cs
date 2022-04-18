using FirstProject.Dtos;

namespace FirstProject.Auth
{
    public interface IJwtAuthenticationManager
    {
          public string GenerateToken(UserDto user);
    }
}
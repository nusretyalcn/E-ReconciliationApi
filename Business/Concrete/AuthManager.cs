using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Core.Utilities.Security.Hashing;
using Core.Utilities.Security.JWT;
using Entities.Dtos;

namespace Business.Concrete;

public class AuthManager: IAuthService
{
    private IUserService _userService;
    private ITokenHelper _tokenHelper;

    public AuthManager(IUserService userService, ITokenHelper tokenHelper)
    {
        _userService = userService;
        _tokenHelper = tokenHelper;
    }

    public IDataResult<User> Register(UserForRegisterDto userForRegisterDto, string password)
    {
        byte[] passwordHash, passwordSalt;
        HashingHelper.CreatePasswordHash(password, out passwordHash, out passwordSalt);
        var user = new User
        {
            Email = userForRegisterDto.Email,
            FirstName = userForRegisterDto.FirstName,
            LastName = userForRegisterDto.LastName,
            PasswordHash = passwordHash,
            PasswordSalt = passwordSalt,
            IsActive = true,
            AddedDate = DateTime.Now,
            MailConfirmValue = Guid.NewGuid().ToString()
        };
        _userService.Add(user);
        return new SuccessDataResult<User>(user, "Kullanıcı kayıt oldu");
    }

    public IDataResult<User> Login(UserForLoginDto userForLoginDto)
    {
        var userToCheck = _userService.GetByEmail(userForLoginDto.Email);
        if (userToCheck.Data == null)
        {
            return new ErrorDataResult<User>("Kullanıcı bulunamadı");
        }

        if (!HashingHelper.VerifyPasswordHash(userForLoginDto.Password, userToCheck.Data.PasswordHash,
                userToCheck.Data.PasswordSalt))
        {
            return new ErrorDataResult<User>("Parola Hatası");
        }

        return new SuccessDataResult<User>(userToCheck.Data, "Giriş Başarılı");
    }

    public IResult UserExists(string email)
    {
        if (_userService.GetByEmail(email).Data != null)
        {
            return new ErrorResult("Kullanıcı zaten mevcut");
        }

        return new SuccessResult();
    }

    public IDataResult<AccessToken> CreateAccessToken(User user, int companyId)
    {
        var claims = _userService.GetClaims(user, companyId);
        var accessToken = _tokenHelper.CreateToken(user, claims.Data, companyId);
        return new SuccessDataResult<AccessToken>(accessToken, "Token oluşturuldu");
    }
}
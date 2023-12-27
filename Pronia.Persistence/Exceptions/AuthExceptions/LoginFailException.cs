namespace Pronia.Persistence.Exceptions;

public class LoginFailException:Exception
{
    public LoginFailException(string message="Username/Email or password is wrong"):base(message)
    {
        
    }
}

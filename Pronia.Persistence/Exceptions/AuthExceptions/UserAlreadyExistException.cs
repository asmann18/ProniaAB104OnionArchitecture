namespace Pronia.Persistence.Exceptions;

public class UserAlreadyExistException:Exception
{
    public UserAlreadyExistException(string message="User is already exist!"):base(message)
    {
            
    }
}

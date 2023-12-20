namespace Pronia.Persistence.Exceptions;

public class TagAlreadyExistException:Exception
{
    public TagAlreadyExistException(string message="Tag is already exist"):base(message)
    {
        
    }
}

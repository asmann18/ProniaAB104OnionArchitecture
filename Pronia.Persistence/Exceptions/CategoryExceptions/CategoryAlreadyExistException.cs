namespace Pronia.Persistence.Exceptions;

public class CategoryAlreadyExistException:Exception
{
    public CategoryAlreadyExistException(string message="Category is already exist"):base(message)
    {
        
    }
}

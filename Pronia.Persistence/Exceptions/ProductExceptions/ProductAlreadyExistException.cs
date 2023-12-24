namespace Pronia.Persistence.Exceptions;

public class ProductAlreadyExistException:Exception
{
    public ProductAlreadyExistException(string message="Product is already exist"):base(message)
    {
        
    }
}

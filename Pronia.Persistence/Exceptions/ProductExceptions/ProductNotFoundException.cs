namespace Pronia.Persistence.Exceptions;

public class ProductNotFoundException:Exception
{
    public ProductNotFoundException(string message="Product is not found"):base(message)
    {
        
    }
}

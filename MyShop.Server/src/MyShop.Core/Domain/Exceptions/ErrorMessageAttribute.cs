namespace MyShop.Core.Domain.Exceptions
{
    [System.AttributeUsage(System.AttributeTargets.Field,
                            AllowMultiple = false)
    ]
    public class ErrorMessageAttribute : System.Attribute
    {
        public string Message;

        public ErrorMessageAttribute(string message)
        {
            Message = message;
        }
    }
}
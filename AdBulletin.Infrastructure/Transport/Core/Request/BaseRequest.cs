namespace AdBulletin.Infrastructure.Transport;

public class BaseRequest
{
    public static BaseRequest<TParameter> Create<TParameter>(TParameter parameter) where TParameter : class, new()
    {
        return new BaseRequest<TParameter> { Parameter = parameter };
    }
}

public class BaseRequest<TParameter> where TParameter : class, new()
{
    public TParameter Parameter { get; set; }

    public BaseRequest()
    {
        Parameter = new TParameter();
    }
}

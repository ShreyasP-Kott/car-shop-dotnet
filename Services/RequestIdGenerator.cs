public interface IRequestIdGenerator
{
    string GetRequestId();
}
public class RequestIdGenerator : IRequestIdGenerator
{
    private readonly string _requestId = Guid.NewGuid().ToString();
    public string GetRequestId() => _requestId;
}
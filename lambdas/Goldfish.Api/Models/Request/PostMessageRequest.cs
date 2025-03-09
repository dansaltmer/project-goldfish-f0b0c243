namespace Goldfish.RestApi.Models.Request;

public class PostMessageRequest
{
    public string Message { get; set; } = string.Empty;

    public PostMessageRequestMedia? Media { get; set; }
}

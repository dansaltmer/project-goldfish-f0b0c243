using Goldfish.Domain;
using Goldfish.Domain.Entities;
using Goldfish.RestApi.Models.Request;

namespace Goldfish.RestApi.Extensions;

public static class PostMessageRequestExtensions
{
    // Lazy and insufficient validation

    public static bool IsMessageValid(this PostMessageRequest? request)
    {
        return request != null && !string.IsNullOrWhiteSpace(request.Message) 
            && request.Message.Length > 9
            && request.Message.Length < 400;
    }

    public static FeedMediaItem? GetMediaDto(this PostMessageRequest? request)
    {
        if (request == null || request.Media == null
            || string.IsNullOrWhiteSpace(request.Media.Type)
            || string.IsNullOrWhiteSpace(request.Media.Url))
        {
            return null;
        }

        return request.Media.Type switch
        {
            Constants.MediaTypes.IFRAME => FeedMediaItem.Iframe(request.Media.Url),
            Constants.MediaTypes.IMAGE => FeedMediaItem.Image(request.Media.Url),
            _ => null
        };
    }
}

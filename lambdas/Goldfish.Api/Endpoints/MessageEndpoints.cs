using Goldfish.Domain.Entities;
using Goldfish.Domain.Services;
using Goldfish.RestApi.Extensions;
using Goldfish.RestApi.Models.Request;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Goldfish.RestApi.Endpoints;

public static class MessageEndpoints
{
    public static RouteGroupBuilder MapMessagesApi(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetFeedMessagesAsync);
        builder.MapPost("/", PostFeedMessageAsync);
        return builder;
    }

    private static async Task<IResult> GetFeedMessagesAsync([FromServices] IMessageStore messageStore, [FromRoute]string feed)
    {
        return Results.Ok(await messageStore.GetMessagesAsync(feed));
    }

    private static async Task<IResult> PostFeedMessageAsync(
        [FromServices] IMessageStore messageStore, 
        [FromRoute] string feed, 
        [FromBody] PostMessageRequest request,
        ClaimsPrincipal user)
    {
        if (!request.IsMessageValid())
        {
            return Results.BadRequest();
        }

        var newMessage = new FeedMessageEntity
        {
            FeedId = feed,
            Timestamp = DateTime.UtcNow,
            Profile = new UserProfile
            {
                Id = user.GetId(),
                Name = user.GetName(),
                Avatar = user.GetAvatar()
            },
            Media = request.GetMediaDto(),
            Text = request.Message
        };

        await messageStore.PutMessageAsync(newMessage);
        return Results.Ok(newMessage);
    }
}

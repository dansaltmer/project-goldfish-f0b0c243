using Goldfish.RestApi.Models;
using Goldfish.RestApi.Services;
using Microsoft.AspNetCore.Mvc;

namespace Goldfish.RestApi.Endpoints;

public static class MessageEndpoints
{
    public static RouteGroupBuilder MapMessagesApi(this RouteGroupBuilder builder)
    {
        builder.MapGet("/", GetChannelMessagesAsync);
        builder.MapPost("/", PostChannelMessageAsync);
        return builder;
    }

    private static async Task GetChannelMessagesAsync([FromServices] IMessageStore messageStore, [FromRoute]string channel)
    {
        throw new NotImplementedException();
    }

    private static async Task<MessageDto> PostChannelMessageAsync([FromServices] IMessageStore messageStore, [FromRoute] string channel)
    {
        var newMessage = new MessageDto
        {
            ChannelId = channel,
            Timestamp = DateTime.UtcNow,
            Profile = new ProfileDto
            {
                // Todo: take from validated token
                Avatar = "https://lh3.googleusercontent.com/a/ACg8ocIkYojdEDw8QyXsqptd32bcxpwr5UbydVX8WP7pIKgPd40XSZrJ=s96-c",
                Name = "fake dan"
            },
            Text = "Bacon ipsum dolor amet swine shankle capicola biltong jowl filet mignon pig shank kielbasa chuck pork belly. Chuck pork belly tail turducken capicola shank. Short loin bacon rump shank, meatloaf frankfurter salami swine shankle. Meatball alcatra hamburger, pork jowl tail boudin tenderloin capicola tongue biltong."
        };

        await messageStore.PutMessageAsync(newMessage);

        return newMessage;
    }
}

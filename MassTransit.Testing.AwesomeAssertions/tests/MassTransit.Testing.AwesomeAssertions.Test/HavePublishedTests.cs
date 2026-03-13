namespace MassTransit.Testing.AwesomeAssertions.Test;

public class HavePublishedTests
{
    [Fact]
    public async Task Published_message_throws_no_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));

        Action action = () => endpoint.Should().HavePublished<OrderCreated>();

        action.Should().NotThrow();
    }

    [Fact]
    public void No_published_message_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();

        Action action = () => endpoint.Should().HavePublished<OrderCreated>();

        action.Should()
            .Throw<XunitException>()
            .WithMessage("*Expected a published message of type*OrderCreated*but found none*");
    }

    [Fact]
    public async Task Published_wrong_type_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCancelled(1));

        Action action = () => endpoint.Should().HavePublished<OrderCreated>();

        action.Should()
            .Throw<XunitException>()
            .WithMessage("*Expected a published message of type*OrderCreated*but found none*");
    }
}

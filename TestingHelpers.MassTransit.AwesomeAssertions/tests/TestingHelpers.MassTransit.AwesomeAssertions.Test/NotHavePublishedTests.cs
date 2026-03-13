namespace TestingHelpers.MassTransit.AwesomeAssertions.Test;

public class NotHavePublishedTests
{
    [Fact]
    public void No_published_message_throws_no_exception()
    {
        var endpoint = new FakePublishEndpoint();

        Action action = () => endpoint.Should().NotHavePublished<OrderCreated>();

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Published_message_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));

        Action action = () => endpoint.Should().NotHavePublished<OrderCreated>();

        action.Should()
            .Throw<XunitException>()
            .WithMessage("*Expected no published message of type*OrderCreated*but found 1*");
    }

    [Fact]
    public async Task Published_different_type_throws_no_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCancelled(1));

        Action action = () => endpoint.Should().NotHavePublished<OrderCreated>();

        action.Should().NotThrow();
    }
}
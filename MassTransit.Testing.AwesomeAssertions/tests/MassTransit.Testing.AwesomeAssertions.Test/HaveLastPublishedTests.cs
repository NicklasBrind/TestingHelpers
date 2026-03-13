namespace MassTransit.Testing.AwesomeAssertions.Test;

public class HaveLastPublishedTests
{
    [Fact]
    public async Task Last_published_returns_last_message()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCreated(2, "Bob"));

        Action action = () => endpoint.Should()
            .HaveLastPublished<OrderCreated>()
            .Which.OrderId.Should().Be(2);

        action.Should().NotThrow();
    }

    [Fact]
    public void No_message_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();

        Action action = () => endpoint.Should().HaveLastPublished<OrderCreated>();

        action.Should()
            .Throw<XunitException>()
            .WithMessage("*Expected a published message of type*OrderCreated*but found none*");
    }

    [Fact]
    public async Task Which_with_wrong_value_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));

        Action action = () => endpoint.Should()
            .HaveLastPublished<OrderCreated>()
            .Which.CustomerName.Should().Be("Bob");

        action.Should().Throw<XunitException>();
    }

    [Fact]
    public async Task Only_considers_matching_type()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCancelled(99));

        Action action = () => endpoint.Should()
            .HaveLastPublished<OrderCreated>()
            .Which.OrderId.Should().Be(1);

        action.Should().NotThrow();
    }
}

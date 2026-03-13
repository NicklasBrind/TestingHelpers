namespace MassTransit.Testing.AwesomeAssertions.Test;

public class HavePublishedSingleTests
{
    [Fact]
    public async Task Single_message_throws_no_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));

        Action action = () => endpoint.Should().HavePublishedSingle<OrderCreated>();

        action.Should().NotThrow();
    }

    [Fact]
    public void No_message_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();

        Action action = () => endpoint.Should().HavePublishedSingle<OrderCreated>();

        action.Should()
            .Throw<XunitException>()
            .WithMessage("*Expected exactly 1 published message of type*OrderCreated*but found 0*");
    }

    [Fact]
    public async Task Multiple_messages_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCreated(2, "Bob"));

        Action action = () => endpoint.Should().HavePublishedSingle<OrderCreated>();

        action.Should()
            .Throw<XunitException>()
            .WithMessage("*Expected exactly 1 published message of type*OrderCreated*but found 2*");
    }

    [Fact]
    public async Task Which_gives_access_to_the_message()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(42, "Alice"));

        Action action = () => endpoint.Should()
            .HavePublishedSingle<OrderCreated>()
            .Which.OrderId.Should().Be(42);

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Which_with_wrong_value_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(42, "Alice"));

        Action action = () => endpoint.Should()
            .HavePublishedSingle<OrderCreated>()
            .Which.OrderId.Should().Be(99);

        action.Should().Throw<XunitException>();
    }
}

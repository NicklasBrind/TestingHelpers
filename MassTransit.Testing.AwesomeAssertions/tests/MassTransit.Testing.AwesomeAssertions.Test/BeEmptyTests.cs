namespace MassTransit.Testing.AwesomeAssertions.Test;

public class BeEmptyTests
{
    [Fact]
    public void No_messages_throws_no_exception()
    {
        var endpoint = new FakePublishEndpoint();

        Action action = () => endpoint.Should().BeEmpty();

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Published_message_throws_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));

        Action action = () => endpoint.Should().BeEmpty();

        action.Should()
            .Throw<XunitException>()
            .WithMessage("*Expected no published messages*but found 1*");
    }

    [Fact]
    public async Task After_clear_throws_no_exception()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        endpoint.Clear();

        Action action = () => endpoint.Should().BeEmpty();

        action.Should().NotThrow();
    }
}
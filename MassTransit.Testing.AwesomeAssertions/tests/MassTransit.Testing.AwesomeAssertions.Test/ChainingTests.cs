namespace MassTransit.Testing.AwesomeAssertions.Test;

public class ChainingTests
{
    [Fact]
    public async Task Can_chain_multiple_assertions_with_and()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderShipped(1, "TRACK-123"));

        Action action = () => endpoint.Should()
            .HavePublished<OrderCreated>()
            .And.HavePublished<OrderShipped>()
            .And.NotHavePublished<OrderCancelled>();

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Chained_assertion_fails_on_second_condition()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));

        Action action = () => endpoint.Should()
            .HavePublished<OrderCreated>()
            .And.HavePublished<OrderShipped>();

        action.Should().Throw<XunitException>();
    }

    [Fact]
    public async Task Clear_resets_published_messages()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        endpoint.Clear();

        Action action = () => endpoint.Should().NotHavePublished<OrderCreated>();

        action.Should().NotThrow();
    }
}
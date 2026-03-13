namespace TestingHelpers.MassTransit.AwesomeAssertions.Test;

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

    [Fact]
    public async Task Which_gives_access_to_the_collection()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCreated(2, "Bob"));

        Action action = () => endpoint.Should()
            .HavePublished<OrderCreated>()
            .Which.Should().HaveCount(2);

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Which_contain_single_with_predicate()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCreated(2, "Bob"));

        Action action = () => endpoint.Should()
            .HavePublished<OrderCreated>()
            .Which.Should().ContainSingle(x => x.CustomerName == "Alice");

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Which_contain_with_predicate()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCreated(2, "Bob"));

        Action action = () => endpoint.Should()
            .HavePublished<OrderCreated>()
            .Which.Should().Contain(x => x.OrderId == 2);

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Which_only_contain()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCreated(2, "Bob"));

        Action action = () => endpoint.Should()
            .HavePublished<OrderCreated>()
            .Which.Should().OnlyContain(x => x.OrderId > 0);

        action.Should().NotThrow();
    }

    [Fact]
    public async Task Which_last_element()
    {
        var endpoint = new FakePublishEndpoint();
        await endpoint.Publish(new OrderCreated(1, "Alice"));
        await endpoint.Publish(new OrderCreated(2, "Bob"));

        Action action = () => endpoint.Should()
            .HavePublished<OrderCreated>()
            .Which.Last().CustomerName.Should().Be("Bob");

        action.Should().NotThrow();
    }
}
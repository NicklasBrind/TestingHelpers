using AwesomeAssertions.Execution;

namespace MassTransit.Testing.AwesomeAssertions;

public static class FakePublishEndpointExtensions
{
    public static FakePublishEndpointAssertions Should(this FakePublishEndpoint instance)
    {
        return new FakePublishEndpointAssertions(instance, AssertionChain.GetOrCreate());
    }
}
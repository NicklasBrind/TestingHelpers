using AwesomeAssertions.Execution;

namespace TestingHelpers.MassTransit.AwesomeAssertions;

public static class FakePublishEndpointExtensions
{
    public static FakePublishEndpointAssertions Should(this FakePublishEndpoint instance)
    {
        return new FakePublishEndpointAssertions(instance, AssertionChain.GetOrCreate());
    }
}
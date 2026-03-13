namespace MassTransit.Testing.AwesomeAssertions.Test;

public record OrderCreated(int OrderId, string CustomerName);
public record OrderCancelled(int OrderId);
public record OrderShipped(int OrderId, string TrackingNumber);
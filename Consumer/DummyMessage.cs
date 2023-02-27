using MassTransit;

namespace Messages;

[EntityName("testing-message")]
public record DummyMessage(string Value);
using MassTransit;
using XoDotNet.GameEvents.Events;

namespace XoDotNet.GameEvents.Consumers;

public class UpdateStateConsumer : IConsumer<UpdateStateEvent>
{
    public async Task Consume(ConsumeContext<UpdateStateEvent> context)
    {
        // TODO: send update_state to the hub
        throw new NotImplementedException();
    }
}
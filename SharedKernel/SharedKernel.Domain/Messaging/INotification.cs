namespace SharedKernel.Domain.Messaging
{
    public interface INotification : IMessage
    {
        string Property { get;}
        string Message { get; }
    }
}

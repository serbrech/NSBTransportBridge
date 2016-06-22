namespace Messages.Events
{
    public interface ISwellSizeChanged : ICanBeBridged
    {
        int Size { get; set; }
    }
}
public interface IWebService
{
    void Request();
    //Instead of an EventDispatcher, we put the actual Signals into the Interface
    CallbackWebServiceSignal Signal { get; }
}

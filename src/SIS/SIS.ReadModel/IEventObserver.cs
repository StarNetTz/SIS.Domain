using SIS.PL;

namespace SIS.ReadModel
{
    public interface IEventObserver
    {
        void EventAppeared(IEvent ev);
    }
}

using System;
using System.Linq;

namespace SIS.Api.ServiceInterface
{
    public interface IMessageBus
    {
        void Send(object message);
    }
}
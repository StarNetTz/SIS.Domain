using StarNet.DDD.PL;
using System;

namespace SIS
{
    public interface IApplicationService
    {
        void Execute(ICommand command);
    }
}

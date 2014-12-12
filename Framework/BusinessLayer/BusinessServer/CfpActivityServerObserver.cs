using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallTraversal.Framework.BusinessLayer.BusinessServer
{
    public interface CfpActivityServerObserver
    {
        void onActivityComplete(string verb, Guid sessionId, Guid transactionId, bool isSuccess, string errorMessage);
    }
}

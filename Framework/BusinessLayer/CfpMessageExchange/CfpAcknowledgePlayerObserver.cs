using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public interface CfpAcknowledgePlayerObserver
    {
        void onAcknowledge(Guid transactionGuid, bool isSuccess, string errorMessage);
    }
}

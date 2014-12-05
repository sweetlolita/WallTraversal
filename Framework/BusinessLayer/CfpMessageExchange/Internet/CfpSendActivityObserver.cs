using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange.Internet
{
    public interface CfpSendActivityObserver
    {
        void onSend(Guid appGuid, string appData);
    }
}

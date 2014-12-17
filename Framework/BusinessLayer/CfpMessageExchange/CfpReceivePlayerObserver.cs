using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public interface CfpReceivePlayerObserver
    {
        void onAppData(string appData);
    }
}

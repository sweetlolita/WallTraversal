using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallTraversal.EndPoint.ClientIntranet
{
    public interface WallTraversalClientIntranetObserver
    {
        void onAppData(string appData);
    }
}

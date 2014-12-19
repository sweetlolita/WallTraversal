using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WallTraversal.EndPoint.ClientInternet
{
    public interface WallTraversalClientInternetObserver
    {
        void onStarted();

        void onStopped();
    }
}

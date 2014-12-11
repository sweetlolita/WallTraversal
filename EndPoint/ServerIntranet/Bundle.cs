using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.EndPoint;
using CFlat.Utility;
using CFlat.Bridge.Cfp.Gutter;
using WallTraversal.Framework.BusinessLayer.CfpMessageExchange;
using WallTraversal.Framework.BusinessLayer.BusinessServer;

namespace WallTraversal.EndPoint.ServerIntranet
{
    class Bundle : CfpGutterObserver, CfpActivityServerObserver
    {
        private CfpServer cfpServer { get; set; }
        private CfpActivityServer cfpActivityServer { get; set; }

        private BiMap<Guid, Guid> appGuidtoSessionGuidMap { get; set; }

        private CfpRegisterPlayer cfpRegisterPlayer { get; set; }
        private CfpGprsReceiveDelegateReceivePlayer cfpGprsReceiveDelegateReceivePlayer { get; set; }
        public Bundle(string ipAddress, int port)
        {
            cfpServer = new CfpServer(ipAddress, port, this);
            cfpActivityServer = new CfpActivityServer(this);

            appGuidtoSessionGuidMap = new BiMap<Guid, Guid>();

            cfpRegisterPlayer = new CfpRegisterPlayer(appGuidtoSessionGuidMap);
            cfpGprsReceiveDelegateReceivePlayer = new CfpGprsReceiveDelegateReceivePlayer(appGuidtoSessionGuidMap, cfpServer);

            cfpServer.registerActivity(CfpRegisterPlayground.verbRef, typeof(CfpRegisterPlayground), cfpRegisterPlayer);
            cfpServer.registerActivity(CfpGprsReceiveDelegateReceivePlayground.verbRef, typeof(CfpGprsReceiveDelegateReceivePlayground), cfpGprsReceiveDelegateReceivePlayer);
        }

        public void start()
        {
            Logger.debug("WallTraversal.EndPoint.ServerInternet.Bundle: starting...");

            cfpActivityServer.start();
            cfpServer.start();

            Logger.debug("WallTraversal.EndPoint.ServerInternet.Bundle: start.");
        }

        public void stop()
        {
            Logger.debug("WallTraversal.EndPoint.ServerInternet.Bundle: stopping...");

            cfpServer.stop();
            cfpActivityServer.stop();

            Logger.debug("WallTraversal.EndPoint.ServerInternet.Bundle: stop.");
        }


        void CfpGutterObserver.onCfpActivity(Activity activity)
        {
            Logger.debug("Bundle: activity {0} has been post to generic server.", activity.playground.verb);
            cfpActivityServer.postRequest(activity);
        }

        void CfpGutterObserver.onCfpConnected(Guid sessionId)
        {
            Logger.debug("Bundle: session {0} connected.", sessionId);
        }

        void CfpGutterObserver.onCfpDisconnected(Guid sessionId)
        {
            Logger.debug("Bundle: session {0} disconnected.", sessionId);
        }

        void CfpGutterObserver.onCfpError(Guid sessionId, string errorMessage)
        {
            Logger.error("CfpGutter: session {0} encountered an error {0}", sessionId, errorMessage);
        }

        void CfpActivityServerObserver.onActivityComplete(Guid sessionId, Guid transactionId, bool isSuccess, string errorMessage)
        {
            CfpAcknowledgePlayground playground = new CfpAcknowledgePlayground();
            playground.transactionId = transactionId;
            playground.isSuccess = isSuccess;
            playground.errorMessage = errorMessage;
            cfpServer.send(sessionId, playground);
        }
    }
}

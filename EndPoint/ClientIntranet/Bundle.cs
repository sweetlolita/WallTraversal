using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.EndPoint;
using CFlat.Utility;
using CFlat.Bridge.Cfp.Gutter;
using WallTraversal.Framework.BusinessLayer.CfpMessageExchange;

namespace WallTraversal.EndPoint.ClientIntranet
{
    public class Bundle : CfpGutterObserver , CfpReceivePlayerObserver , CfpAcknowledgePlayerObserver
    {
        private WallTraversalClientIntranetObserver wallTraversalClientIntranetObserver { get; set; }
        private Guid appGuid { get; set; }
        private CfpClient cfpClient { get; set; }
        private CfpAcknowledgePlayer cfpAcknowledgePlayer { get; set; }
        private CfpReceivePlayer cfpReceivePlayer { get; set; }


        private Guid registerTransactionGuid { get; set; }

        public Bundle(Guid appGuid, 
            string clientIpAddress, int clientPort, string serverIpAddress, int serverPort,
            WallTraversalClientIntranetObserver wallTraversalClientIntranetObserver)
        {
            this.appGuid = appGuid;
            this.wallTraversalClientIntranetObserver = wallTraversalClientIntranetObserver;

            cfpClient = new CfpClient(clientIpAddress, clientPort, serverIpAddress, serverPort, this);

            cfpAcknowledgePlayer = new CfpAcknowledgePlayer(this);
            cfpReceivePlayer = new CfpReceivePlayer(this);

            cfpClient.registerActivity(CfpAcknowledgePlayground.verbRef, typeof(CfpAcknowledgePlayground), cfpAcknowledgePlayer);
            cfpClient.registerActivity(CfpReceivePlayground.verbRef, typeof(CfpReceivePlayground), cfpReceivePlayer);
        }

        public void start()
        {
            Logger.debug("WallTraversal.EndPoint.ClientInternet.Bundle: starting...");

            cfpClient.start();

            Logger.debug("WallTraversal.EndPoint.ClientInternet.Bundle: start.");
        }

        public void stop()
        {
            Logger.debug("WallTraversal.EndPoint.ClientInternet.Bundle: stopping...");

            cfpClient.stop();

            Logger.debug("WallTraversal.EndPoint.ClientInternet.Bundle: stop.");
        }

        void CfpGutterObserver.onCfpActivity(Activity activity)
        {
            Logger.debug("Bundle: activity {0} has come.", activity.playground.verb);
            activity.act();
            
        }

        void CfpGutterObserver.onCfpConnected(Guid sessionId)
        {
            Logger.debug("Bundle: session {0} connected.", sessionId);
            this.registerTransactionGuid = register(appGuid);
        }

        void CfpGutterObserver.onCfpDisconnected(Guid sessionId)
        {
            Logger.debug("Bundle: session {0} disconnected.", sessionId);
            if (wallTraversalClientIntranetObserver != null)
            {
                wallTraversalClientIntranetObserver.onStopped();
            }
        }

        void CfpGutterObserver.onCfpError(Guid sessionId, string errorMessage)
        {
            Logger.error("Bundle: session {0} encountered an error: {1}", sessionId, errorMessage);
        }

        public Guid register(Guid appGuid)
        {
            CfpRegisterPlayground registerPlayground = new CfpRegisterPlayground();
            registerPlayground.appGuid = appGuid;
            registerPlayground.transactionId = Guid.NewGuid();
            cfpClient.send(registerPlayground);
            return registerPlayground.transactionId;
        }

        void CfpReceivePlayerObserver.onAppData(string appData)
        {
            if(wallTraversalClientIntranetObserver != null)
            {
                wallTraversalClientIntranetObserver.onAppData(appData);
            }
        }

        void CfpAcknowledgePlayerObserver.onAcknowledge(Guid transactionGuid, bool isSuccess, string errorMessage)
        {
            if (wallTraversalClientIntranetObserver != null &&
                transactionGuid == this.registerTransactionGuid && 
                isSuccess == true)
            {
                wallTraversalClientIntranetObserver.onStarted();
            }
        }
    }
}

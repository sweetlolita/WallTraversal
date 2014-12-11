using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.EndPoint;
using CFlat.Utility;
using CFlat.Bridge.Cfp.Gutter;
using WallTraversal.Framework.BusinessLayer.CfpMessageExchange;


namespace WallTraversal.EndPoint.ClientInternet
{
    class Bundle : CfpGutterObserver
    {
        private CfpClient cfpClient { get; set; }
        private CfpAcknowledgePlayer cfpAcknowledgePlayer { get; set; }

        public Bundle(string clientIpAddress, int clientPort, string serverIpAddress, int serverPort)
        {
            cfpClient = new CfpClient(clientIpAddress, clientPort, serverIpAddress, serverPort, this);

            cfpAcknowledgePlayer = new CfpAcknowledgePlayer();

            cfpClient.registerActivity(CfpAcknowledgePlayground.verbRef, typeof(CfpAcknowledgePlayground), cfpAcknowledgePlayer);
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
        }

        void CfpGutterObserver.onCfpDisconnected(Guid sessionId)
        {
            Logger.debug("Bundle: session {0} disconnected.", sessionId);
        }

        void CfpGutterObserver.onCfpError(Guid sessionId, string errorMessage)
        {
            Logger.error("CfpGutter: session {0} encountered an error {0}", sessionId, errorMessage);
        }

        public void register(Guid appGuid)
        {
            CfpRegisterPlayground registerPlayground = new CfpRegisterPlayground();
            registerPlayground.appGuid = appGuid;
            registerPlayground.transactionId = Guid.NewGuid();
            cfpClient.send(registerPlayground);
        }

        public void send(string appData)
        {
            CfpSendPlayground sendPlayground = new CfpSendPlayground();
            sendPlayground.appData = appData;
            sendPlayground.transactionId = Guid.NewGuid();
            cfpClient.send(sendPlayground);
        }

    }
}

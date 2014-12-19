using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.EndPoint;
using CFlat.Utility;
using CFlat.Bridge.Cfp.Gutter;
using WallTraversal.Framework.BusinessLayer.CfpMessageExchange;
using WallTraversal.Framework.BusinessLayer.BusinessServer;
using WallTraversal.Gateway.Gprs.GprsTunnel;

namespace WallTraversal.EndPoint.ServerInternetExtension
{
    class GprsTransmitter : CfpGutterObserver, CfpActivityServerObserver, CfpAcknowledgePlayerObserver
    {
        private readonly Guid extensionGuid = Guid.Parse("6638A58E-5190-4407-AB0C-ED3AA5213097");

        //its a server extension defined by business logic, providing gprs sending service.
        //but actually in transport layer , it works as a cfp client connected to internet server 
        private CfpClient cfpClient { get; set; }
        private CfpActivityServer cfpActivityServer { get; set; }

        private CfpGrpsSendDelegatePlayer cfpGrpsSendDelegatePlayer { get; set; }

        private CfpAcknowledgePlayer cfpAcknowledgePlayer { get; set; }

        private GprsSender gprsSender { get; set; }

        private Guid registerTransactionGuid { get; set; }
        public GprsTransmitter()
        {
            string serverIpAddress = ConfigParser.getValueFromCFlatConfig("ServerInternetIpAddress");
            string serverPortString = ConfigParser.getValueFromCFlatConfig("ServerInternetPort");
            int serverPort = Convert.ToInt32(serverPortString);

            string clientIpAddress = ConfigParser.getValueFromCFlatConfig("GprsTransmitterIpaddress");
            string clientPortString = ConfigParser.getValueFromCFlatConfig("GprsTransmitterPort");
            int clientPort = Convert.ToInt32(clientPortString);

            cfpClient = new CfpClient(clientIpAddress, clientPort, serverIpAddress, serverPort, this);
            cfpActivityServer = new CfpActivityServer(this);

            gprsSender = new GprsSender();

            cfpGrpsSendDelegatePlayer = new CfpGrpsSendDelegatePlayer(gprsSender);
            cfpAcknowledgePlayer = new CfpAcknowledgePlayer(this);

            cfpClient.registerActivity(CfpGprsSendDelegatePlayground.verbRef, typeof(CfpGprsSendDelegatePlayground), cfpGrpsSendDelegatePlayer);
            cfpClient.registerActivity(CfpAcknowledgePlayground.verbRef, typeof(CfpAcknowledgePlayground), cfpAcknowledgePlayer);

            
        }

        public void start()
        {
            Logger.debug("WallTraversal.EndPoint.ServerInternet.Bundle: starting...");

            gprsSender.start();
            cfpActivityServer.start();
            cfpClient.start();
            
            Logger.debug("WallTraversal.EndPoint.ServerInternet.Bundle: start.");
        }

        public void stop()
        {
            Logger.debug("WallTraversal.EndPoint.ServerInternet.Bundle: stopping...");

            cfpClient.stop();
            cfpActivityServer.stop();
            gprsSender.stop();

            Logger.debug("WallTraversal.EndPoint.ServerInternet.Bundle: stop.");
        }


        void CfpGutterObserver.onCfpActivity(Activity activity)
        {
            if (activity.playground.verb == CfpGprsSendDelegatePlayground.verbRef)
            {
                Logger.debug("Bundle: activity {0} has been post to generic server.", activity.playground.verb);
                cfpActivityServer.postRequest(activity);
            }
            else if (activity.playground.verb == CfpAcknowledgePlayground.verbRef)
            {
                activity.act();
            }
        }

        void CfpGutterObserver.onCfpConnected(Guid sessionId)
        {
            Logger.debug("Bundle: session {0} connected.", sessionId);
            this.registerTransactionGuid = register();
        }

        void CfpGutterObserver.onCfpDisconnected(Guid sessionId)
        {
            Logger.debug("Bundle: session {0} disconnected.", sessionId);
        }

        void CfpGutterObserver.onCfpError(Guid sessionId, string errorMessage)
        {
            Logger.error("Bundle: session {0} encountered an error {1}", sessionId, errorMessage);
        }

        void CfpActivityServerObserver.onActivityComplete(string verb, Guid sessionId, Guid transactionId, bool isSuccess, string errorMessage)
        {
            //todo: send complete
            Logger.debug("GprsTransmitter: transaction {0} complete. result: {1} errorMessage : {2}",
                transactionId, isSuccess, errorMessage);
        }

        public Guid register()
        {
            CfpRegisterPlayground registerPlayground = new CfpRegisterPlayground();
            registerPlayground.appGuid = extensionGuid;
            registerPlayground.transactionId = Guid.NewGuid();
            cfpClient.send(registerPlayground);
            return registerPlayground.transactionId;

        }

        void CfpAcknowledgePlayerObserver.onAcknowledge(Guid transactionGuid, bool isSuccess, string errorMessage)
        {
            if (transactionGuid == this.registerTransactionGuid &&
                isSuccess == true)
            {
                Logger.info("GprsTransmitter: get to go.");
            }
        }
    }
}

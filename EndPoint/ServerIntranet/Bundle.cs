using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.EndPoint;
using CFlat.Utility;
using CFlat.Bridge.Cfp.Gutter;
using WallTraversal.Framework.BusinessLayer.CfpMessageExchange;
using WallTraversal.Framework.BusinessLayer.BusinessServer;
using DPMP_COMM;

namespace WallTraversal.EndPoint.ServerIntranet
{
    class Bundle : CfpGutterObserver, CfpActivityServerObserver
    {
        private CfpServer cfpServer { get; set; }
        private CfpActivityServer cfpActivityServer { get; set; }

        private BiMap<Guid, Guid> appGuidtoSessionGuidMap { get; set; }

        private CfpRegisterPlayer cfpRegisterPlayer { get; set; }
        private CfpGprsReceiveDelegateReceivePlayer cfpGprsReceiveDelegateReceivePlayer { get; set; }
        public Bundle()
        {
            string ipAddress = ConfigParser.getValueFromCFlatConfig("ServerIntranetIpAddress");
            string portString = ConfigParser.getValueFromCFlatConfig("ServerIntranetPort");
            int port = Convert.ToInt32(portString);
            FuncLayer.onAppData = this.onAppData;
            TerminalPool pool = new TerminalPool();
            //pool.OnAppMsgReceived += AppMsgReceived;
            pool.LoadTerminalList();

            cfpServer = new CfpServer(ipAddress, port, this);
            cfpActivityServer = new CfpActivityServer(this);

            appGuidtoSessionGuidMap = new BiMap<Guid, Guid>();

            cfpRegisterPlayer = new CfpRegisterPlayer(appGuidtoSessionGuidMap);
            cfpGprsReceiveDelegateReceivePlayer = new CfpGprsReceiveDelegateReceivePlayer(appGuidtoSessionGuidMap, cfpServer);


            cfpServer.registerActivity(CfpRegisterPlayground.verbRef, typeof(CfpRegisterPlayground), cfpRegisterPlayer);
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

        void CfpActivityServerObserver.onActivityComplete(string verb, Guid sessionId, Guid transactionId, bool isSuccess, string errorMessage)
        {
            if (verb == CfpRegisterPlayground.verbRef)
            {
                CfpAcknowledgePlayground playground = new CfpAcknowledgePlayground();
                playground.transactionId = transactionId;
                playground.isSuccess = isSuccess;
                playground.errorMessage = errorMessage;
                cfpServer.send(sessionId, playground);
            }
        }

        void onAppData(string jsonPayload)
        {
            Logger.debug("Bundle: activity gprsReceiveDelegate has come.");

            Traversal traversal = Traversal.fromJson<Traversal>(jsonPayload);
            CfpGprsReceiveDelegateReceivePlayground playground = new CfpGprsReceiveDelegateReceivePlayground();

            playground.appGuid = traversal.appGuid;
            playground.appData = traversal.appTraversal;

            Activity receiveActivity = new Activity();
            receiveActivity.playground = playground;
            receiveActivity.player = cfpGprsReceiveDelegateReceivePlayer;

            cfpActivityServer.postRequest(receiveActivity);
            Logger.debug("Bundle: activity gprsReceiveDelegate has been post to generic server.");
        }
    }
}

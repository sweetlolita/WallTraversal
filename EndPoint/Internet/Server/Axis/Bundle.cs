using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.EndPoint;
using CFlat.Utility;
using WallTraversal.Framework.BusinessLayer.BusinessServer.Internet;
using WallTraversal.Framework.BusinessLayer.CfpMessageExchange.CommonEntity;
using WallTraversal.Framework.BusinessLayer.CfpMessageExchange.Internet;

namespace WallTraversal.EndPoint.Internet.Server.Axis
{
    class Bundle : CfpSendActivityObserver
    {
        private BiMap<Guid, Guid> appGuidtoSessionGuidMap { get; set; }
        private CfpServer cfpServer { get; set; }
        private CfpRegisterActivity cfpRegisterActivity { get; set; }
        private CfpSendActivity cfpSendActivity { get; set; }
        private BusinessServerInternet businessServerInternet { get; set; }
        public Bundle(string ipAddress, int port)
        {
            appGuidtoSessionGuidMap = new BiMap<Guid, Guid>();
            cfpRegisterActivity = new CfpRegisterActivity(appGuidtoSessionGuidMap);
            cfpSendActivity = new CfpSendActivity(appGuidtoSessionGuidMap, this);

            cfpServer = new CfpServer(ipAddress, port);
            businessServerInternet = new BusinessServerInternet();

            cfpServer.registerActivity("register", cfpRegisterActivity);
            cfpServer.registerActivity("send", cfpSendActivity);
        }

        public void start()
        {
            Logger.debug("WallTraversal.EndPoint.Internet.Server.Axis.Bundle: starting...");

            businessServerInternet.start();
            cfpServer.start();

            Logger.debug("WallTraversal.EndPoint.Internet.Server.Axis.Bundle: start.");
        }

        public void stop()
        {
            Logger.debug("WallTraversal.EndPoint.Internet.Server.Axis.Bundle: stopping...");

            cfpServer.stop();
            businessServerInternet.stop();

            Logger.debug("WallTraversal.EndPoint.Internet.Server.Axis.Bundle: stop.");
        }

        void CfpSendActivityObserver.onSend(Guid appGuid, string appData)
        {
            businessServerInternet.postGprsSendRequest(appGuid, appData);
        }
    }
}

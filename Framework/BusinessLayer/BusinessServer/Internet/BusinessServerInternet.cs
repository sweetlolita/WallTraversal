using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.GenericServer;
using CFlat.GenericServer.Context;
using WallTraversal.Framework.BusinessLayer.BusinessServer.CommonEntity;
using CFlat.Utility;

namespace WallTraversal.Framework.BusinessLayer.BusinessServer.Internet
{
    public class BusinessServerInternet : CFlatGenericServer
    {
        private GprsSendBusinessActivity gprsSendBusinessActivity { get; set; }
        private GprsSendResponse response { get; set; }
        public BusinessServerInternet()
        {
            gprsSendBusinessActivity = new GprsSendBusinessActivity();
            response = new GprsSendResponse();
            registerActivity("gprs", "send", gprsSendBusinessActivity);
            
        }

        public void postGprsSendRequest(Guid appGuid, string appData)
        {
            Logger.debug("BusinessServerInternet: incoming GprsSendRequest of appGuid : {0} , appData : {1}",
                appGuid, appData);
            CfgsRequest request = new CfgsRequest();
            request.business = "gprs";
            request.method = "send";
            Traversal traversal = new Traversal();
            traversal.appGuid = appGuid;
            traversal.encodeAndSetString(appData);
            request.param.Add(traversal);
            CfgsContext context = new CfgsContext(request, response);
            postRequest(context);
            Logger.debug("BusinessServerInternet: GprsSendRequest post.");
        }
    }
}

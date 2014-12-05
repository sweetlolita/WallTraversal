using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.GenericServer;
using CFlat.GenericServer.Context;

namespace WallTraversal.Framework.BusinessLayer.BusinessServer.Intranet
{
    public class BusinessServerIntranet : CFlatGenericServer
    {
        private GprsReceiveBusinessActivity gprsReceiveBusinessActivity { get; set; }
        private GprsReceiveResponse response { get; set; }

        public BusinessServerIntranet()
        {
            gprsReceiveBusinessActivity = new GprsReceiveBusinessActivity();
            response = new GprsReceiveResponse();
            registerActivity("gprs", "receive", gprsReceiveBusinessActivity);
            
        }

        public void postGprsReceiveRequest(List<object> paramList)
        {
            CfgsRequest request = new CfgsRequest();
            request.business = "gprs";
            request.method = "receive";
            paramList.ForEach(o => request.param.Add(o));
            CfgsContext context = new CfgsContext(request, response);
            postRequest(context);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;
using CFlat.Bridge.Cfp.CommonEntity;

namespace WallTraversal.Framework.BusinessLayer.BusinessServer
{
    public class CfpActivityServer : SingleListServer<Activity>
    {
        private CfpActivityServerObserver genericActivityServerObserver { get; set; }

        public CfpActivityServer(CfpActivityServerObserver genericActivityServerObserver)
        {
            this.genericActivityServerObserver = genericActivityServerObserver;
        }

        protected override void handleRequest(Activity request)
        {
            Guid sessionId = (request.player as CfpPlayer).sessionId;
            Guid transactionId = (request.playground as CfpPlayground).transactionId;
            bool isSuccess = true;
            string errorMessage = "";
            Logger.debug("BusinessServer: handling request = {0}",
                request.playground.verb);
            try
            {
                request.act();
                Logger.debug("GenericActivityServer: finish request = {0}",
                    request.playground.verb);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                Logger.error("CFlatGenericServer: encounted an error, skip this request for reason: {0}", ex.Message);
                isSuccess = false;
                errorMessage = ex.Message;
            }
            finally
            {
                if(genericActivityServerObserver != null)
                {
                    genericActivityServerObserver.onActivityComplete(sessionId, transactionId, isSuccess, errorMessage);
                }
            }
        }
    }
}

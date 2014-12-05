using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange.Internet
{
    public class CfpSendActivity : ActivityBase
    {
        private BiMap<Guid, Guid> appGuidtoSessionGuidMap { private get; private set; }
        private CfpSendActivityObserver cfpSendActivityObserver { get; set; }
        public CfpSendActivity(BiMap<Guid, Guid> appGuidtoSessionGuidMapRef, CfpSendActivityObserver cfpSendActivityObserver)
        {
            appGuidtoSessionGuidMap = appGuidtoSessionGuidMapRef;
            this.cfpSendActivityObserver = cfpSendActivityObserver;
        }
        public override void act(List<object> paramList)
        {
            Logger.debug("SendActivity: validating param...");
            validateParamCount(paramList, 2);
            //param 0 is sessionId
            validateParamAsSpecificType(paramList, 0, typeof(Guid));
            //param 1 is SendString json string
            validateParamAsSpecificType(paramList, 1, typeof(string));
            string sendStringJson = paramList.ElementAt<object>(1) as string;
            Logger.debug("SendActivity: parsing param...");
            SendString sendString = SendString.fromJson<SendString>(sendStringJson);
            Logger.debug("SendActivity: searching appGuid by sessionId...");
            Guid appGuid = appGuidtoSessionGuidMap.searchKeyByValue((Guid)paramList.ElementAt<object>(0));
            Logger.debug("SendActivity: posting request to business server...");
            cfpSendActivityObserver.onSend(appGuid, sendString.appData);
        }

    }
}

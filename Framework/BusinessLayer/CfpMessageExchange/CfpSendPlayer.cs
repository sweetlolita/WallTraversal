using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;
using CFlat.Bridge.Cfp.CommonEntity;
using CFlat.Bridge.Cfp.EndPoint;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpSendPlayer : CfpPlayer
    {
        private readonly Guid extensionGuid = Guid.Parse("6638A58E-5190-4407-AB0C-ED3AA5213097");
        private BiMap<Guid, Guid> appGuidtoSessionGuidMapRef { get; set; }
        private CfpServerInterface cfpServerInterface { get; set; }
        public CfpSendPlayer(BiMap<Guid, Guid> appGuidtoSessionGuidMapRef, CfpServerInterface cfpServerInterface)
        {
            this.appGuidtoSessionGuidMapRef = appGuidtoSessionGuidMapRef;
            this.cfpServerInterface = cfpServerInterface;
        }

        public override void play(PlaygroundBase playgroundBase)
        {
            CfpSendPlayground sendPlayground = playgroundBase as CfpSendPlayground;
            Guid appGuid = appGuidtoSessionGuidMapRef.searchKeyByValue(sessionId);
            if(appGuid == Guid.Empty)
            {
                throw new CfpException("CfpSendPlayer: appguid not found for session " + sessionId + ", it may not registered.");
            }
            Guid delegateSessionId = appGuidtoSessionGuidMapRef.searchValueByKey(extensionGuid);
            if(delegateSessionId == Guid.Empty)
            {
                throw new CfpException("CfpSendPlayer: gprs send delegate not available.");
            }
            CfpGprsSendDelegatePlayground delegatePlayground = new CfpGprsSendDelegatePlayground();
            delegatePlayground.appGuid = appGuid;
            delegatePlayground.appData = sendPlayground.appData;
            cfpServerInterface.send(delegateSessionId, delegatePlayground);
        }
    }
}

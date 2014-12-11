using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.CommonEntity;
using CFlat.Utility;
using CFlat.Bridge.Cfp.EndPoint;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpGprsReceiveDelegateReceivePlayer : CfpPlayer
    {
        private BiMap<Guid, Guid> appGuidtoSessionGuidMapRef { get; set; }
        private CfpServerInterface cfpServerInterface { get; set; }
        public CfpGprsReceiveDelegateReceivePlayer(BiMap<Guid, Guid> appGuidtoSessionGuidMapRef, CfpServerInterface cfpServerInterface)
        {
            this.appGuidtoSessionGuidMapRef = appGuidtoSessionGuidMapRef;
            this.cfpServerInterface = cfpServerInterface;
        }

        public override void play(PlaygroundBase playgroundBase)
        {
            CfpGprsReceiveDelegateReceivePlayground recvPlayground = playgroundBase as CfpGprsReceiveDelegateReceivePlayground;

            Guid clientSessionId = appGuidtoSessionGuidMapRef.searchValueByKey(recvPlayground.appGuid);
            if (clientSessionId == Guid.Empty)
            {
                throw new CfpException("CfpReceivePlayer: gprs recv client not available, it may not register.");
            }

            CfpReceivePlayground cfpReceivePlayground = new CfpReceivePlayground();
            cfpReceivePlayground.appData = recvPlayground.appData;
            cfpServerInterface.send(clientSessionId, cfpReceivePlayground);
        }
    }
}

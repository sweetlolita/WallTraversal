using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;
using CFlat.Bridge.Cfp.CommonEntity;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpRegisterPlayer : CfpPlayer
    {
        private BiMap<Guid, Guid> appGuidtoSessionGuidMapRef { get; set; }

        public CfpRegisterPlayer(BiMap<Guid, Guid> appGuidtoSessionGuidMapRef)
        {
            this.appGuidtoSessionGuidMapRef = appGuidtoSessionGuidMapRef;
        }
        public override void play(PlaygroundBase playgroundBase)
        {
            CfpRegisterPlayground playground = playgroundBase as CfpRegisterPlayground;
            Logger.debug("RegisterActivity: putting registered client into map...");
            appGuidtoSessionGuidMapRef.put(
                playground.appGuid,
                sessionId
                );
            Logger.debug("RegisterActivity: appId {0} with sessionId {1} registered complete.",
                playground.appGuid, sessionId);
        }
    }
}

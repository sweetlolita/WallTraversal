using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.CommonEntity;
using CFlat.Utility;
using WallTraversal.Gateway.Gprs.GprsTunnel;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpGrpsSendDelegatePlayer : CfpPlayer
    {
        private GprsSender gprsSenderRef { get; set; }
        public CfpGrpsSendDelegatePlayer(GprsSender gprsSenderRef)
        {
            this.gprsSenderRef = gprsSenderRef;
        }
        public override void play(PlaygroundBase playgroundBase)
        {
            CfpGprsSendDelegatePlayground delegatePlayground = playgroundBase as CfpGprsSendDelegatePlayground;
            Logger.debug("incoming gprs request: {0} | {1}", delegatePlayground.appGuid, delegatePlayground.appData);
            Traversal traversal = new Traversal();
            traversal.appGuid = delegatePlayground.appGuid;
            traversal.appTraversal = delegatePlayground.appData;
            gprsSenderRef.send(traversal.toJson());
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.CommonEntity;
using CFlat.Utility;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpAcknowledgePlayer : CfpPlayer
    {
        private CfpAcknowledgePlayerObserver cfpAcknowledgePlayerObserver { get; set; }
        public CfpAcknowledgePlayer(CfpAcknowledgePlayerObserver cfpAcknowledgePlayerObserver)
        {
            this.cfpAcknowledgePlayerObserver = cfpAcknowledgePlayerObserver;
        }
        public override void play(PlaygroundBase playgroundBase)
        {
            CfpAcknowledgePlayground playground = playgroundBase as CfpAcknowledgePlayground;
            Logger.debug("CfpAcknowledgePlayer: transaction {0} returns with {1}, errorMessage : {2}.",
                playground.transactionId, playground.isSuccess, playground.errorMessage);
            if(cfpAcknowledgePlayerObserver != null)
            {
                cfpAcknowledgePlayerObserver.onAcknowledge(playground.transactionId, playground.isSuccess, playground.errorMessage);
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.CommonEntity;
using CFlat.Utility;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpReceivePlayer : CfpPlayer
    {

        public override void play(PlaygroundBase playgroundBase)
        {
            CfpReceivePlayground playground = playgroundBase as CfpReceivePlayground;
            Logger.debug("CfpReceivePlayer: APP DATA: {1}", playground.appData);
        }
    }
}

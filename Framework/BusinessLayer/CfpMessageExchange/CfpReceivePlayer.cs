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
        private CfpReceivePlayerObserver cfpReceivePlayerObserver { get; set; }
        public CfpReceivePlayer(CfpReceivePlayerObserver cfpReceivePlayerObserver)
        {
            this.cfpReceivePlayerObserver = cfpReceivePlayerObserver;
        }
        public override void play(PlaygroundBase playgroundBase)
        {
            CfpReceivePlayground playground = playgroundBase as CfpReceivePlayground;
            Logger.debug("CfpReceivePlayer: on app data: {0}", playground.appData);
            if(cfpReceivePlayerObserver != null)
            {
                cfpReceivePlayerObserver.onAppData(playground.appData);
            }
        }
    }
}

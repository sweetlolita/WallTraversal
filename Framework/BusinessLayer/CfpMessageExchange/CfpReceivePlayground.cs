using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.CommonEntity;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpReceivePlayground : CfpPlayground
    {
        public const string verbRef = "receive";
        public CfpReceivePlayground()
        {
            verb = verbRef;
        }

        public string appData { get; set; }
    }
}

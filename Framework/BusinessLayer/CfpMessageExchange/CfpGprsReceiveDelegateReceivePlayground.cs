using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.CommonEntity;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpGprsReceiveDelegateReceivePlayground : CfpPlayground
    {
        public const string verbRef = "gprsReceiveDelegate";
        public CfpGprsReceiveDelegateReceivePlayground()
        {
            verb = verbRef;
        }

        public Guid appGuid { get; set; }
        public string appData { get; set; }
    }
}

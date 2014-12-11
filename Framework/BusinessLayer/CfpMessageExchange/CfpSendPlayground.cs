using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.CommonEntity;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpSendPlayground : CfpPlayground
    {
        public const string verbRef = "send";
        public CfpSendPlayground()
        {
            verb = verbRef;
        }

        public string appData { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.CommonEntity;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpAcknowledgePlayground : CfpPlayground
    {
        public const string verbRef = "acknowledge";
        public CfpAcknowledgePlayground()
        {
            verb = verbRef;
        }

        public bool isSuccess { get; set; }
        public string errorMessage { get; set; }
    }
}

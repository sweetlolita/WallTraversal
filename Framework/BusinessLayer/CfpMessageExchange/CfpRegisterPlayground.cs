using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Bridge.Cfp.CommonEntity;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange
{
    public class CfpRegisterPlayground : CfpPlayground
    {
        public const string verbRef = "register";
        public CfpRegisterPlayground()
        {
            verb = verbRef;
        }

        public Guid appGuid { get; set; }
    }
}

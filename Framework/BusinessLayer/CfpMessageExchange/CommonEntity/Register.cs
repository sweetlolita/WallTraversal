using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange.CommonEntity
{
    public class Register : Jsonable
    {
        public Guid appGuid { get; set; }
    }
}

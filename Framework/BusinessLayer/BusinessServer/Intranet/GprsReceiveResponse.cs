using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.GenericServer.Context;
using CFlat.Utility;

namespace WallTraversal.Framework.BusinessLayer.BusinessServer.Intranet
{
    class GprsReceiveResponse : CfgsResponse
    {
        public override void onResponsed()
        {
            Logger.debug("gprs receive responsed with {0}, error message :{1}", this.isSuccess, this.errorMessage);
        }
    }
}

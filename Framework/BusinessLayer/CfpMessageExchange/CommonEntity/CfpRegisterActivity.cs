using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;

namespace WallTraversal.Framework.BusinessLayer.CfpMessageExchange.CommonEntity
{
    public class CfpRegisterActivity : ActivityBase
    {
        private BiMap<Guid, Guid> appGuidtoSessionGuidMap { private get; private set; }
        public CfpRegisterActivity(BiMap<Guid, Guid> appGuidtoSessionGuidMapRef)
        {
            appGuidtoSessionGuidMap = appGuidtoSessionGuidMapRef;
        }
        public override void act(List<object> paramList)
        {
            Logger.debug("RegisterActivity: validating param...");
            validateParamCount(paramList, 2);
            //param 0 is sessionId
            validateParamAsSpecificType(paramList, 0, typeof(Guid));
            //param 1 is Register json string
            validateParamAsSpecificType(paramList, 1, typeof(string));
            string registerJson = paramList.ElementAt<object>(1) as string;
            Logger.debug("RegisterActivity: parsing param...");
            Register register = Register.fromJson<Register>(registerJson);
            Logger.debug("RegisterActivity: putting registered client into map...");
            appGuidtoSessionGuidMap.put(
                register.appGuid,
                (Guid)paramList.ElementAt<object>(0)
                );
        }
    }
}

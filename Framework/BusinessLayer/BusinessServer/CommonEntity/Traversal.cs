using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CFlat.Utility;
using WallTraversal.Framework.BusinessLayer.Cryption;

namespace WallTraversal.Framework.BusinessLayer.BusinessServer.CommonEntity
{
    public class Traversal : Jsonable
    {
        public Guid appGuid { get; set; }
        public string appTraversal { get; set; }

        public void encodeAndSetBytes(byte[] source)
        {
            appTraversal = Base64Codec.encodeFromBytes(source);
        }

        public void encodeAndSetString(string source)
        {
            appTraversal = Base64Codec.encodeFromString(source);
        }

        public byte[] decodeAndGetBytes(string source)
        {
            return Base64Codec.decodeAsBytes(source);
        }

        public string decodeAndGetString(string source)
        {
            return Base64Codec.decodeAsString(source);
        }
    }
}

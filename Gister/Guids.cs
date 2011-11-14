// Guids.cs
// MUST match guids.h
using System;

namespace EchelonTouchInc.Gister
{
    static class GuidList
    {
        public const string guidGisterPkgString = "282f92e9-8465-4d0e-b815-4a87a990219c";
        public const string guidGisterCmdSetString = "652e87f9-3c5d-4e8b-8b80-f5a0efd16d69";

        public static readonly Guid guidGisterCmdSet = new Guid(guidGisterCmdSetString);
    };
}
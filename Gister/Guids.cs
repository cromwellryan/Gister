// Guids.cs
// MUST match guids.h
using System;

namespace EchelonTouchInc.Gister
{
    static class GuidList
    {
        public const string guidGisterPkgString = "282f92e9-8465-4d0e-b815-4a87a990219c";
        public const string guidGisterCmdSetString = "652e87f9-3c5d-4e8b-8b80-f5a0efd16d69";

        public const string guidGisterWithDescriptionStringCmdSet = "9567B479-B4A9-4658-ABB2-230EB94C94D6";

        public static readonly Guid guidGisterCmdSet = new Guid(guidGisterCmdSetString);
        public static readonly Guid guidGisterWithDescriptionCmdSet = new Guid(guidGisterWithDescriptionStringCmdSet);
    };
}
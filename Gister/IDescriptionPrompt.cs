using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EchelonTouchInc.Gister
{
    public interface IDescriptionPrompt
    {
        bool GistPrivate { get; }
        string Description { get; }
        void Prompt();
    }
}

using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace EchelonTouchInc.Gister.Api
{
    public class VsStatusUpdates : StatusUpdates
    {
        private readonly IOleComponentUIManager uiManager;

        public VsStatusUpdates(IOleComponentUIManager uiManager)
        {
            this.uiManager = uiManager;
        }

        public void NotifyUserThat(string messagetotelltheuser)
        {
            if (messagetotelltheuser != null) uiManager.SetStatus(messagetotelltheuser, UInt32.Parse("0"));

        }
    }
}

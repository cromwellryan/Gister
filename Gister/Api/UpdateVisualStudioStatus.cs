using System;
using Microsoft.VisualStudio.Shell.Interop;

namespace EchelonTouchInc.Gister.Api
{
    public class UpdateVisualStudioStatus : UpdatesStatus
    {
        private readonly IOleComponentUIManager uiManager;

        public UpdateVisualStudioStatus(IOleComponentUIManager uiManager)
        {
            this.uiManager = uiManager;
        }

        public void NotifyUserThat(string messagetotelltheuser)
        {
            if (messagetotelltheuser != null) uiManager.SetStatus(messagetotelltheuser, UInt32.Parse("0"));

        }
    }
}

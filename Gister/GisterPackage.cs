using System;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.ComponentModel.Design;
using System.Windows.Forms;
using EchelonTouchInc.Gister.Api;
using EchelonTouchInc.Gister.Api.Credentials;
using EnvDTE;
using Microsoft.VisualStudio.Editor;
using Microsoft.VisualStudio.Shell.Interop;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.TextManager.Interop;
using Microsoft.VisualStudio.Shell;
using Microsoft.VisualStudio.Utilities;


namespace EchelonTouchInc.Gister
{
    /// <summary>
    /// This is the class that implements the package exposed by this assembly.
    ///
    /// The minimum requirement for a class to be considered a valid package for Visual Studio
    /// is to implement the IVsPackage interface and register itself with the shell.
    /// This package uses the helper classes defined inside the Managed Package Framework (MPF)
    /// to do it: it derives from the Package class that provides the implementation of the 
    /// IVsPackage interface and uses the registration attributes defined in the framework to 
    /// register itself and its components with the shell.
    /// </summary>
    // This attribute tells the PkgDef creation utility (CreatePkgDef.exe) that this class is
    // a package.
    [PackageRegistration(UseManagedResourcesOnly = true)]
    // This attribute is used to register the informations needed to show the this package
    // in the Help/About dialog of Visual Studio.
    [InstalledProductRegistration("#110", "#112", "1.0", IconResourceID = 400)]
    // This attribute is needed to let the shell know that this package exposes some menus.
    [ProvideMenuResource("Menus.ctmenu", 1)]
    [Guid(GuidList.guidGisterPkgString)]
    public sealed class GisterPackage : Package
    {
        /// <summary>
        /// Default constructor of the package.
        /// Inside this method you can place any initialization code that does not require 
        /// any Visual Studio service because at this point the package object is created but 
        /// not sited yet inside Visual Studio environment. The place to do all the other 
        /// initialization is the Initialize method.
        /// </summary>
        public GisterPackage()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering constructor for: {0}", this));
        }

        /////////////////////////////////////////////////////////////////////////////
        // Overriden Package Implementation
        #region Package Members

        /// <summary>
        /// Initialization of the package; this method is called right after the package is sited, so this is the place
        /// where you can put all the initilaization code that rely on services provided by VisualStudio.
        /// </summary>
        protected override void Initialize()
        {
            Trace.WriteLine(string.Format(CultureInfo.CurrentCulture, "Entering Initialize() of: {0}", this));

            base.Initialize();

            // Add our command handlers for menu (commands must exist in the .vsct file)
            var mcs = GetService(typeof(IMenuCommandService)) as OleMenuCommandService;

            if (null == mcs) return;

            // Create the command for the menu item.
            var createGistCommand = new CommandID(GuidList.guidGisterCmdSet, (int)PkgCmdIDList.cmdCreateGist);
            var createGistMenuItem = new MenuCommand(CreateGist, createGistCommand);
            mcs.AddCommand(createGistMenuItem);
        }
        #endregion

        /// <summary>
        /// This function is the callback used to execute a command when the a menu item is clicked.
        /// See the Initialize method to see how the menu item is associated to this function using
        /// the OleMenuCommandService service and the MenuCommand class.
        /// </summary>
        private void CreateGist(object sender, EventArgs e)
        {
            var view = GetActiveTextView();

            if (NotReadyRockAndRoll(view)) return;

            var content = GetCurrentContentForGist(view);
            var fileName = GetCurrentFilenameForGist();

            var credentials = GetGitHubCredentials();

            NotifyUserThat("Creating gist for {0}", fileName);

            if (credentials == GitHubCredentials.Anonymous)
            {
                NotifyUserThat("Cancelled Gist");
                return;
            }

            var uploadsGists = new UploadsGists
                                   {
                                       GitHubSender = new HttpGitHubSender(),
                                       CredentialsAreBad = () =>
                                                               {
                                                                   NotifyUserThat("Gist not created.  Invalid GitHub Credentials");
                                                                   new CachesGitHubCredentials().AssureNotCached();
                                                               },
                                       Uploaded = url =>
                                                      {
                                                          Clipboard.SetText(url);
                                                          new CachesGitHubCredentials().Cache(credentials);

                                                          NotifyUserThat("Gist created successfully.  Url placed in the clipboard.");
                                                      }
                                   };

            uploadsGists.UseCredentials(credentials);

            uploadsGists.Upload(fileName, content);

        }

        private static GitHubCredentials GetGitHubCredentials()
        {
            var retrievers = new RetrievesCredentials[]
                                 {
                                     new CachesGitHubCredentials(),
                                     new RetrievesUserEnteredCredentials()
                                 };

            var firstAppropriate = (from applier in retrievers
                                    where applier.IsAvailable()
                                    select applier).First();

            return firstAppropriate.Retrieve();
        }

        private bool NotReadyRockAndRoll(IPropertyOwner view)
        {
            return view == null ||
                   Dte.ActiveDocument == null;
        }

        private void NotifyUserThat(string format, params object[] args)
        {
            var uiManager = ((IOleComponentUIManager)GetService(typeof(SOleComponentUIManager)));

            if (uiManager == null) return;

            var message = string.Format(format, args);

            uiManager.SetStatus(message, UInt32.Parse("0"));
        }

        private string GetCurrentFilenameForGist()
        {
            return Dte.ActiveDocument.Name;
        }

        private DTE Dte
        {
            get { return (DTE)GetService(typeof(DTE)); }
        }

        private static string GetCurrentContentForGist(ITextView view)
        {
            if (SelectionIsAvailable(view))
                return GetSelectedText(view);

            return view.TextSnapshot.GetText();
        }

        private static string GetSelectedText(ITextView view)
        {
            return view.Selection.SelectedSpans[0].GetText();
        }

        private static bool SelectionIsAvailable(ITextView view)
        {
            if (view == null) throw new ArgumentNullException("view");

            return !view.Selection.IsEmpty && view.Selection.SelectedSpans.Count > 0;
        }

        private IWpfTextView GetActiveTextView()
        {
            IWpfTextView view = null;
            IVsTextView vTextView;

            var txtMgr = (IVsTextManager)GetService(typeof(SVsTextManager));
            const int mustHaveFocus = 1;

            txtMgr.GetActiveView(mustHaveFocus, null, out vTextView);

            var userData = vTextView as IVsUserData;
            if (null != userData)
            {
                object holder;

                var guidViewHost = DefGuidList.guidIWpfTextViewHost;
                userData.GetData(ref guidViewHost, out holder);

                var viewHost = (IWpfTextViewHost)holder;
                view = viewHost.TextView;
            }

            return view;
        }
    }
}
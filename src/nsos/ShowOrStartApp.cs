/************************************************************
 * This code is licensed under "The MIT License"
 * Copyright (c) by Alberto Gonzalez
 *
 * Please see the included 'LICENSE.txt' file for the full
 * text of the license.
 ************************************************************/

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using nl4net.Helpers;

namespace nTools.nSoS
{
    public class ShowOrStartApp
    {
        public const int EXIT_OK = 0;
        public const int EXIT_ERROR = 1;

        private IInformationHerald _herald;

        public ShowOrStartApp(IInformationHerald infoHerald)
        {
            if (infoHerald == null) throw new ArgumentNullException("infoHerald", "InformationHerald is required");
            _herald = infoHerald;
        }

        public int Run(string[] args)
        {
            try
            {
                var settings = new ShowOrStartSettings(args);
                if (settings.HasErrors)
                {
                    _herald.ShowHelp(settings.ErrorMessage);
                    return EXIT_ERROR;
                }

                if (settings.IsRequestingInfo)
                {
                    ProvideInfo(settings);
                    return EXIT_OK;
                }

                Process proc = FindProcess(settings.NameOrTitle);
                IntPtr handle = (proc != null) ? proc.MainWindowHandle : IntPtr.Zero;

                if (!ShowedOrStarted(handle, settings))
                {
                    _herald.ShowHelp(string.Format("'{0}' was not found and no command was specified", settings.NameOrTitle));
                    return EXIT_ERROR;
                }
                return EXIT_OK;
            }
            catch (Exception ex)
            {
                _herald.ShowErrorMessage("UNEXPECTED ERROR: {0}", ex.Message);
                return EXIT_ERROR;
            }
        }

        bool ShowedOrStarted(IntPtr handle, ShowOrStartSettings settings)
        {
            if (handle != IntPtr.Zero)
            {
                if (WindowHelper.IsMinimized(handle))
                    WindowHelper.Restore(handle);
            }
            else if (!string.IsNullOrEmpty(settings.Command))
            {
                Process p = Process.Start(settings.Command, settings.CommandArgs);
                handle = p.MainWindowHandle;
            }
            if (handle == IntPtr.Zero)
                return false;

            WindowHelper.MoveToForeground(handle);
            return true;
        }

        void ProvideInfo(ShowOrStartSettings settings)
        {
            if (settings.InfoRequestedType == InfoRequestType.Help)
                _herald.ShowHelp();
            else if (settings.InfoRequestedType == InfoRequestType.ProcessList)
                _herald.ShowWindowedProcessList();
            else if (settings.InfoRequestedType == InfoRequestType.Version)
                _herald.ShowBanner();
        }

        Process FindProcess(string nameOrTitle)
        {
            IEnumerable<Process> procs = ProcessHelper.FindWindowedProcessesByName(nameOrTitle);
            if (!procs.Any())
                procs = ProcessHelper.FindWindowedProcessByTitle(nameOrTitle);
            if (!procs.Any())
                procs = ProcessHelper.FindWindowedProcessByTitleStartingWith(nameOrTitle);
            if (!procs.Any())
                procs = ProcessHelper.FindWindowedProcessByTitleContaining(nameOrTitle);

            // TODO: ??? Provide way to select if found multiple ???

            return (procs.Any()) ? procs.First() : null;
        }
    }
}

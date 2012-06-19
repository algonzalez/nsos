/************************************************************
 * This code is licensed under "The MIT License"
 * Copyright (c) by Alberto Gonzalez
 *
 * Please see the included 'LICENSE.txt' file for the full
 * text of the license.
 ************************************************************/

using System;
using System.Diagnostics;
using System.Linq;
using nl4net.Helpers;

namespace nTools.nSoS
{
    class ConsoleInformationHerald : InformationHerald, IInformationHerald
    {
        public void ShowBanner()
        {
            Console.Out.WriteLine(Banner);
        }

        public void ShowErrorMessage(string errorMessage) { ShowErrorMessage("ERROR:", errorMessage); }
        public void ShowErrorMessage(string label, string message)
        {
            if (!string.IsNullOrEmpty(label))
                Console.Error.Write("{0} ", label);
            Console.Error.WriteLine(message ?? "");
        }

        public void ShowHelp() { ShowHelp(null); }
        public void ShowHelp(string errorMessage)
        {
            ShowBanner();
            Console.Out.WriteLine();
            if (!string.IsNullOrEmpty(errorMessage))
            {
                ShowErrorMessage("ERROR:", errorMessage);
                Console.Out.WriteLine();
            }
            ShowUsage();
        }

        public void ShowMessage(string message) { ShowMessage(null, message); }
        public void ShowMessage(string label, string message)
        {
            if (!string.IsNullOrEmpty(label))
                Console.Out.Write("{0} ", label);
            Console.Out.WriteLine(message ?? "");
        }

        public void ShowUsage()
        {
            System.IO.TextWriter o = Console.Out;
            Console.Out.WriteLine(Usage);
        }

        public void ShowWindowedProcessList()
        {
            var curProc = Process.GetCurrentProcess();
            var allWindowedProcs = Process.GetProcesses()
                                    .Where(p => p.MainWindowHandle != IntPtr.Zero
                                        // TODO: un-comment when done seeing what changes when AsssemblyInfo attributes are set
                                        //                                        && p.Id != curProc.Id
                                        && !(p.ProcessName == "explorer" && string.IsNullOrEmpty(p.MainWindowTitle)))
                                    .OrderBy(p => p.ProcessName);
            bool isNotFirst = false;
            string divider = null;

            Console.Out.WriteLine("List of Running Processes:");
            Console.Out.WriteLine();
            foreach (var proc in allWindowedProcs)
            {
                if (isNotFirst)
                    Console.Out.WriteLine(divider ?? (divider = new string('-', Console.WindowWidth - 1)));
                Console.Out.WriteLine("Proc Name: {0} / Id: {1}", proc.ProcessName, proc.Id);
                Console.Out.WriteLine("     Desc: {0}", proc.MainModule.FileVersionInfo.FileDescription);
                Console.Out.WriteLine("     File: {0}", proc.MainModule.FileName);
                if (proc.ProcessName == "cmd" && proc.MainWindowTitle.EndsWith(Environment.CommandLine))
                {
                    // handles running from existing cmd window where the command line is temporarily appended to the title.
                    Console.Out.WriteLine("    Title: {0}", proc.MainWindowTitle.Substring(0, proc.MainWindowTitle.Length - Environment.CommandLine.Length - 3));
                }
                else
                {
                    Console.Out.WriteLine("    Title: {0}", proc.MainWindowTitle);
                }
                isNotFirst = true;
            }
            if (ProcessHelper.IsProcessWindowed(curProc)) Console.ReadLine();
        }
    }
}

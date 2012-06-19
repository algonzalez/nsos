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

namespace nl4net.Helpers
{
    static class ProcessHelper
    {
        public static bool IsCurrentProcessWindowed()
        {
            return IsProcessWindowed(Process.GetCurrentProcess());
        }

        public static bool IsProcessWindowed(Process proc)
        {
            proc.Refresh();
            return (proc.MainWindowHandle != IntPtr.Zero);
        }

        public static IEnumerable<Process> FindWindowedProcessesByName(string name)
        {
            return Process.GetProcesses()
                .Where(p => p.MainWindowHandle != IntPtr.Zero
                    && string.Compare(name, p.ProcessName, StringComparison.OrdinalIgnoreCase) == 0);
        }
        public static IEnumerable<Process> FindWindowedProcessByTitle(string title)
        {
            return Process.GetProcesses()
                .Where(p => p.MainWindowHandle != IntPtr.Zero
                    && !string.IsNullOrEmpty(p.MainWindowTitle)
                    && string.Compare(title, p.MainWindowTitle, StringComparison.OrdinalIgnoreCase) == 0);
        }

        public static IEnumerable<Process> FindWindowedProcessByTitleStartingWith(string titleFragment)
        {
            return Process.GetProcesses()
                .Where(p => p.MainWindowHandle != IntPtr.Zero
                    && !string.IsNullOrEmpty(p.MainWindowTitle)
                    && p.MainWindowTitle.StartsWith(titleFragment, StringComparison.OrdinalIgnoreCase));
        }

        public static IEnumerable<Process> FindWindowedProcessByTitleContaining(string titleFragment)
        {
            titleFragment = titleFragment.ToUpper();
            return Process.GetProcesses()
                .Where(p => p.MainWindowHandle != IntPtr.Zero
                    && !string.IsNullOrEmpty(p.MainWindowTitle)
                    && p.MainWindowTitle.ToUpper().Contains(titleFragment)
                    && !p.MainWindowTitle.EndsWith(Environment.CommandLine));
        }
    }
}

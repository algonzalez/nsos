/************************************************************
 * This code is licensed under "The MIT License"
 * Copyright (c) by Alberto Gonzalez
 *
 * Please see the included 'LICENSE.txt' file for the full
 * text of the license.
 ************************************************************/

using System;
using System.Runtime.InteropServices;

namespace nl4net.Helpers
{
    static class WindowHelper
    {
        public static bool IsMaximized(IntPtr hWnd)
        {
            return IsZoomed(hWnd);
        }

        public static bool IsMinimized(IntPtr hWnd)
        {
            return IsIconic(hWnd);
        }

        public static void Hide(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_HIDE);
        }

        public static void Maximize(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_MAXIMIZED);
        }

        public static void Minimize(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_MINIMIZED);
        }

        public static void MoveToForeground(IntPtr hWnd)
        {
            SetForegroundWindow(hWnd);
        }

        public static void Restore(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_RESTORE);
        }

        public static void Show(IntPtr hWnd)
        {
            ShowWindow(hWnd, SW_NORMAL);
        }

        [DllImport("user32.dll")]
        static extern bool IsIconic(IntPtr hWnd);

        [DllImport("user32.dll")]
        static extern bool IsZoomed(IntPtr hWnd);

        [DllImportAttribute("User32.dll")]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        const int SW_HIDE = 0;
        const int SW_NORMAL = 1;
        const int SW_MAXIMIZED = 3;
        const int SW_MINIMIZED = 6;
        const int SW_RESTORE = 9;

        [DllImport("user32.dll")]
        static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
    }
}

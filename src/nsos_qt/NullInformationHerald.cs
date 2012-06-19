/************************************************************
 * This code is licensed under "The MIT License"
 * Copyright (c) by Alberto Gonzalez
 *
 * Please see the included 'LICENSE.txt' file for the full
 * text of the license.
 ************************************************************/

namespace nTools.nSoS
{
    class NullInformationHerald : IInformationHerald
    {
        public void ShowBanner() { }

        public void ShowErrorMessage(string errorMessage) { }
        public void ShowErrorMessage(string label, string message) { }

        public void ShowHelp() { }
        public void ShowHelp(string errorMessage) { }

        public void ShowMessage(string message) { }
        public void ShowMessage(string label, string message) { }

        public void ShowUsage() { }

        public void ShowWindowedProcessList() { }
    }
}

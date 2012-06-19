/************************************************************
 * This code is licensed under "The MIT License"
 * Copyright (c) by Alberto Gonzalez
 *
 * Please see the included 'LICENSE.txt' file for the full
 * text of the license.
 ************************************************************/

namespace nTools.nSoS
{
    public interface IInformationHerald
    {
        void ShowBanner();
        void ShowErrorMessage(string errorMessage);
        void ShowErrorMessage(string label, string message);
        void ShowHelp();
        void ShowHelp(string errorMessage);
        void ShowMessage(string message);
        void ShowMessage(string label, string message);
        void ShowUsage();
        void ShowWindowedProcessList();
    }
}

/************************************************************
 * This code is licensed under "The MIT License"
 * Copyright (c) by Alberto Gonzalez
 *
 * Please see the included 'LICENSE.txt' file for the full
 * text of the license.
 ************************************************************/

using System.Linq;

namespace nTools.nSoS
{
    class ShowOrStartSettings
    {
        public ShowOrStartSettings(string[] args)
        {
            InfoRequestedType = InfoRequestType.None;

            if (!args.Any())
            {
                ErrorMessage = "No arguments or options were specified";
                return;
            }

            NameOrTitle = args[0];

            if (NameOrTitle.StartsWith("?"))
            {
                InfoRequestedType = InfoRequestType.Help;
            }

            // options start with slash or dash
            if ("/-".Contains(NameOrTitle[0]))
            {
                string option = NameOrTitle;
                char optionStartChar = '#';

                // strip leading dashes. Allows support for long form of option (ex. --Help)
                option = new string(option.Skip(1).SkipWhile(c => c == '-').ToArray());
                if (option.Length > 0)
                    optionStartChar = char.ToLower(option[0]);

                if (optionStartChar == 'l')
                    InfoRequestedType = InfoRequestType.ProcessList;
                else if (optionStartChar == 'v')
                    InfoRequestedType = InfoRequestType.Version;
                else if ("?h".Contains(optionStartChar))
                    InfoRequestedType = InfoRequestType.Help;

                if (!IsRequestingInfo)
                    ErrorMessage = string.Format("Unrecognized option '{0}'", args[0]);
            }

            Command = (args.Length > 1) ? args[1] : null;
            CommandArgs = (args.Length > 2) ? string.Join(" ", args.Skip(2).ToArray()) : ""; ;
        }

        public InfoRequestType InfoRequestedType { get; private set; }
        public bool IsRequestingInfo { get { return InfoRequestedType != InfoRequestType.None; } }

        public string NameOrTitle { get; private set; }
        public string Command { get; private set; }
        public string CommandArgs { get; private set; }

        public string ErrorMessage { get; private set; }
        public bool HasErrors { get { return !string.IsNullOrEmpty(ErrorMessage); } }
    }
}

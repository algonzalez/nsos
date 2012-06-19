/************************************************************
 * This code is licensed under "The MIT License"
 * Copyright (c) by Alberto Gonzalez
 *
 * Please see the included 'LICENSE.txt' file for the full
 * text of the license.
 ************************************************************/

using System;

namespace nTools.nSoS
{
    static class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            var herald = new NullInformationHerald();
            var app = new ShowOrStartApp(herald);
            return app.Run(args);
        }
    }
}

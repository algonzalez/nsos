/************************************************************
 * This code is licensed under "The MIT License"
 * Copyright (c) by Alberto Gonzalez
 *
 * Please see the included 'LICENSE.txt' file for the full
 * text of the license.
 ************************************************************/

namespace nTools.nSoS
{
    class Program
    {
        static int Main(string[] args)
        {
            var herald = new ConsoleInformationHerald();
            var app = new ShowOrStartApp(herald);
            return app.Run(args);
        }
    }
}

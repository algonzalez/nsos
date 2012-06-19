/************************************************************
 * This code is licensed under "The MIT License"
 * Copyright (c) by Alberto Gonzalez
 *
 * Please see the included 'LICENSE.txt' file for the full
 * text of the license.
 ************************************************************/

namespace nTools.nSoS
{
    abstract class InformationHerald
    {
        protected readonly string Banner =
@"Nomadic Show or Start - nSoS (pronounced nSauce). Version 1.0
Copyright 2012 by Alberto Gonzalez, All Rights Reserved.
nSoS locates a running app and, if found, brings it to the foreground;
    if not found, it runs the given command with the given arguments.";

        protected readonly string Usage =
@"Usage:
  nsos <nameOrTitle> <command> [arg1 [arg2 [... [argN]]]]
  nsos -l | --list
  nsos -? | -h | --help
  nsos -v | --version

Arguments:
  nameOrTitle     the process name, the exact window title,
                  text that the window title starts with
                  or text the window title contains
                  NOTE: All comparisons are case in-sensitive
  command         the app to run if <nameOrTitle> is not found
  arg1..argN      the arguments to pass to the <command>

Options:
  -l, --list      lists running processes that may be shown
  -?, -h, --help  show this message
  -v, --version   shows the version of nSoS";
    }
}

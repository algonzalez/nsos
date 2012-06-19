# Nomadic Show or Start (nSoS - pronounced nSauce)

## Description

nSoS (which I pronounce as nSauce) locates a running app and, if found, brings it to the foreground; if not found, it runs the given command with the given arguments.

There are two versions of the utility:

- nsos.exe -- regular interactive console application. Will open a window if run directly from the GUI and not from an existing console (ex. cmd.exe)
- nsos_qt.exe -- quiet version that will not display any messages and will not open a window when run directly.

## Usage

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
      -v, --version   shows the version of nSoS

## Roadmap

Planned feature additions and/or enhancements:

- configuration file to define aliases
- localize strings and messages

## Authors

**Alberto Gonzalez** (aka "Al")

  - Github: [http://github.com/algonzalez](http://github.com/algonzalez)
  - Twitter: [@AlGonzalez](http://twitter.com/algonzalez)

## Copyright & License

Nomadic Show or Start is Copyright 2012 by Alberto Gonzalez, All Rights Reserved.

It is licensed under the MIT License (the "License"); you may not use this work except in compliance with the License. 

You may obtain a copy of the License in the LICENSE.txt file, or at [http://www.opensource.org/licenses/mit-license.php](http://www.opensource.org/licenses/mit-license.php)


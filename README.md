# Nomadic Show or Start (nSoS - pronounced nSauce)

nSoS (which I pronounce as nSauce) locates a running app and, if found, brings it to the foreground; if not found, it runs the given command with the given arguments.

Story: User requests a shell window via hotkey and expects a single instance.

    As a command line user
    I want to bring up a single instance of my shell via a hotkey
    So that I get quick access without having multiple windows floating around

Acceptance Criteria:  (presented as Scenarios)
    
    Scenario 1: Shell is running
    Given a shortcut to my shell
          And a hotkey is defined
     When the hotkey is pressed,
     Then find an existing shell window
          and bring it to the front

    Scenario 2: Shell is not running
    Given a shortcut to my shell
          And a hotkey is defined
     When the hotkey is pressed
     Then launch a new shell

How I use nSoS in these scenarios:

    First, I created a .lnk file on my desktop that calls "nsos_qt.exe (o,o) c:\bin\cn.cmd"
    Note: The first argument is a fragment of what shows in my shell's window caption.
          The second parameter is a batch file that launches cmd.exe configured to my liking.
    Next, I set the shortcut hotkey to Ctrl + Alt + C
    Now, whenever I press the hotkey, my console window comes to the front and I
    dont' have to worry about having a bunch of windows open.

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

## Buid

Run the install-packages.cmd to install any missing project dependencies via NuGet.

Run build-release.cmd or build-debug.cmd to build and test nSoS. The build artifacts will be placed into a folder named Deploy.

NOTE: The projects build against .NET Framework v3.5.

### Dependencies

NuGet packages required by the nsosTests project:

- NSubstitute 
- Shouldly
- XUnit
- XUnit.Extensions
- XUnit.Runners

## Roadmap

Planned feature additions and/or enhancements:

- configuration file to define aliases
- localize strings and messages
- considering option to interactively select a window if more than one is found


## Authors

**Alberto Gonzalez** (aka "Al")

  - Github: [http://github.com/algonzalez](http://github.com/algonzalez)
  - Twitter: [@AlGonzalez](http://twitter.com/algonzalez)

## Copyright & License

Nomadic Show or Start is Copyright 2012 by Alberto Gonzalez, All Rights Reserved.

It is licensed under the MIT License (the "License"); you may not use this work except in compliance with the License. 

You may obtain a copy of the License in the LICENSE.txt file, or at [http://www.opensource.org/licenses/mit-license.php](http://www.opensource.org/licenses/mit-license.php)


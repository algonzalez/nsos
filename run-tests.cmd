@echo off
setlocal
if (%1)==(-?) goto help
if /I (%1)==(--help) goto help
if (%1)==(/?) goto help
if (%1)==(?) goto help

for /D %%d IN (src\packages\xunit.runners*) do set #TestRunnerPath=%%~Fd\tools 
if not defined #TestRunnerPath goto missingTestRunner

if /I (%1)==(gui) goto runInGui
if not (%1)==() goto unrecognizedOption

:runInConsole
"%#TestRunnerPath%\xunit.console.exe" src\nsosTests\bin\release\nsosTests.dll
goto end

:runInGui
"%#TestRunnerPath%\xunit.gui.exe" src\nsosTests\bin\release\nsosTests.dll
goto end

:missingTestRunner
echo ERROR: The test runner was not found. 
echo        Try running install-packages.cmd to install any missing 
echo        project dependencies.
goto end

:unrecognizedOption
set #ErrorMsg=UNEXPECTED OPTION: '%1'
goto help

:help
echo Will attempt to run the project tests. By default, the tests will be run in 
echo the console. If gui is specified as a command line option, they will be run 
echo in a GUI.
if not defined #ErrorMsg goto help-usage
echo.
echo %#ErrorMsg%
:help-usage
echo.
echo Usage:
echo   run-tests.cmd [option]
echo.
echo Options:
echo   {none}       Runs the tests in the console
echo   gui          Runs the tests in a GUI
echo   -?, --help   Displays this message
goto end

:end
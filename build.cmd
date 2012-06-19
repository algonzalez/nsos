@echo off
rem ============================================================
rem TODO: description block
rem ============================================================
setlocal
rem make sure that we are starting in the same directory as the calling batch file
pushd "%~dp0"

rem ============================================================
rem Uses the default .NET Framework installation paths to 
rem determine the location for the msbuild.exe tool.
rem Change these values if the differ on your machine.
rem ============================================================
set MSBUILD20EXE=%SystemRoot%\Microsoft.NET\Framework\v2.0.50727\MSBuild.exe
set MSBUILD35EXE=%SystemRoot%\Microsoft.NET\Framework\v3.5\MSBuild.exe
set MSBUILD40EXE=%SystemRoot%\Microsoft.NET\Framework\v4.0.30319\MSBuild.exe

set BUILDOUTPUTDIRROOT=deploy
set BUILDOUTPUTDIRLIBROOT=%BUILDOUTPUTDIRROOT%\lib

set NUGETEXE=tools\nuget.exe
for /D %%d IN (src\packages\xunit.runners*) do set #TESTRUNNERPATH=%%~Fd\tools
if "%PROCESSOR_ARCHITECTURE%"=="AMD64" (
    set TESTRUNNEREXE=%#TESTRUNNERPATH%\xunit.console.exe
) else (
    set TESTRUNNEREXE=%#TESTRUNNERPATH%\xunit.console.x86.exe
)

rem Test if the tools exist
if not exist "%TESTRUNNEREXE%" (
    call :ToolNotFound "%TESTRUNNEREXE%"
    goto :END
)

rem ============================================================
rem Choose the framework to build. 
rem Set to 1 for to run the build for that version and 0 to not.
rem NOTE: make sure the correct project is defined in the
rem       appropriate framework section below.
rem ============================================================
set BUILDNET20=0
set BUILDNET35=1
set BUILDNET40=0

set BUILDCONFIG=%1
if "%BUILDCONFIG%"=="" set BUILDCONFIG=Debug

if not exist "%BUILDOUTPUTDIRROOT%"\ md "%BUILDOUTPUTDIRROOT%"

rem ============================================================
rem Build items using .NET Framework v3.5
rem ============================================================
if "%BUILDNET35%"=="1" (
    if exist "%MSBUILD35EXE%" (
        rem ========================================
        rem Builds section
        rem ========================================
        "%MSBUILD35EXE%" src\nsos_qt\nsos_qt.csproj /tv:3.5 /t:rebuild /p:Configuration="%BUILDCONFIG%";OutDir=..\..\%BUILDOUTPUTDIRROOT%\%BUILDCONFIG%\;NET35=1 
        if ERRORLEVEL 1 (
            call :UnexpectedError BUILD
            goto :END
        )
        rem NOTE: building the tests builds the dependent assemblies being tested as well
        "%MSBUILD35EXE%" src\nsosTests\nsosTests.csproj /tv:3.5 /t:rebuild /p:Configuration="%BUILDCONFIG%";OutDir=..\..\%BUILDOUTPUTDIRROOT%\%BUILDCONFIG%\;NET35=1 
        if ERRORLEVEL 1 (
            call :UnexpectedError BUILD
            goto :END
        )

        rem ========================================
        rem Tests section
        rem ========================================
        echo.
        "%TESTRUNNEREXE%" "%BUILDOUTPUTDIRROOT%\%BUILDCONFIG%\nsosTests.dll" /noshadow
        if ERRORLEVEL 1 (
            call :UnexpectedError TESTING
            goto :END
        )
    ) else (
        call :FrameworkNotFound 3.5
        goto :END
    )
)

goto :END

:FrameworkNotFound
echo.
echo ERROR: Unable to build for v%1 of the .NET Framework.
echo        The %1 .NET Framework does not appear to be installed.
goto :EOF

:ToolNotFound
echo.
echo ERROR: Could not find %1
echo        This tool is required for running the build.
echo        Try running install-packages.cmd to install any missing 
echo        project dependencies.
goto :EOF

:UnExpectedError
echo.
echo ERROR: Unexpected %1 error was encountered
goto :EOF

:END
rem GoodBye!
popd
endlocal

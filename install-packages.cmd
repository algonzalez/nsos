@echo off
tools\nuget install src\.nuget\packages.config -O src\packages
tools\nuget install src\nsosTests\packages.config -O src\packages

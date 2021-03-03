# OFX Battleship State Tracker
This repo is a coding test for [OFX](https://www.ofx.com), which contains a basic battleship api for a single player

## The Task
The task is to implement a Battleship state tracking API for a single player that must support the following logic:

- Create a board
- Add a battleship to the board
- Take an “attack” at a given position, and report back whether the attack resulted in a hit or a miss.

The API should not support the entire game, just the state tracker. No graphical interface or persistence layer is required.


## Quick Walkthrough of the Application Structure

The application consists of the following projects:
1) OfxCodeTest.Battleship
    * This project is the web api layer
2) OfxCodeTest.Battleship.Services
   * This project contains the business logic, interfaces and models
3) * OfxCodeTest.Battleship.Tests
    * This project has unit tests for all the acceptance criteria

### Dependancy Injection
All the services are injected as a Singleton, as it was advised that no persistance layer is needed.
As it was a single player game, Singleton instance does the job
A better approach would have been in memory Persistance using EF Core

## Clone this project

```
git clone 

## Running the Application
The application was written in .NET 5, so please ensure you have .NET 5 installed. If you don't, you can download it from https://dotnet.microsoft.com/download/dotnet/5.0

**Visual Studio 2019**
If you are using Visual Studio 2017 on a Microsoft environment , open the solution file in the source directory, (BattleshipGame.sln) and run the build. The nuget packages (NUnit, Newtonsoft) should auto restore.


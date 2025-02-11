# Developer Evaluation Project

## Introduction 

This repository contains the project for Developer Evalution at Ambev. 

The test consists in build an API that can handles Sales. More instruction on: [Project Instructions](/.doc/instructions.md)

## Requirements

- .NET Core SDK (Version 8 or superior)
- Docker (If you're using Windows, it's necessary install Docker Desktop)

## Configuration

### Running the project

To run the project simply type the command: 

```bash
cd docker-compose up -d
```

Another alternative for running the project is throught VisualStudio run and build features

### Database - Migrations

This project use Migration to update the database schema: [Migrations](https://docs.microsoft.com/pt-br/ef/core/managing-schemas/migrations/?tabs=dotnet-core-cli).

By default, the project is configured to access a local database using SQL Server; 
however, to maintain this database, it is necessary to run migration creation and update commands. 
The configured structure is located in the "Ambev.DeveloperEvaluation.ORM" project. 

All commands must be run in the folder of this project. Before running the commands, type:

```bash
cd src/Ambev.DeveloperEvaluation.ORM
```

Below, we list the most used commands:

### Creating migrations

```bash
dotnet ef migrations add [MigrationName] --context DefaultContext -o ./Migrations
```

The -o parameter should be used to create the files in the specified Migrations folder.

The other parameters are used to indicate which project and context class should be loaded at runtime for dotnet-ef to create the migration classes.

To update the base, type:

```bash
dotnet ef database update
```

## Project Instructions

This section describes the instructions to develop the project. 

See [Project Instructions](/.doc/instructions.md)
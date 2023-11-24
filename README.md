# Take a Quiz

## Overview

This is a Blazor Webassembly project made with ASP.NET Core 6. Take a Quiz is a Web app where you as a user are able to create your own Quizzes which will be playable by all registered users of the platform.

The application is ASP.NET Core hosted which means that both the client and server is located under the same project solution with a shared folder. The shared folder consist of viewmodels and logical functions shared between both the server and client.

## Features

Take a Quiz comes with many features and provides a solid foundation that can be easily improved and expanded.

### Currently Implemented Features:

- User Authentication: Register a new user and log in with proper authentication using identity.
- Quiz Creation: Create new quizzes with a question-specific timer and media (either a video or image displayed for each question). There is support for an unlimited number of questions for each quiz.
- Quiz Search and Viewing: Every created quiz can be searched for and viewed.
- Profile Overview: View your own quizzes on your profile, including the number of times they have been played. You can also see who played them and the scores they received.

### Future Updates:

- Quiz Rating System: Users can rate quizzes, and quizzes can be sorted by rating and total plays.
- Styling Overhaul: Major styling improvements for a more polished and user-friendly interface.

These planned updates aim to enhance the user experience, engagement, and overall aesthetics of the application.

## Code

## Reflections

## Tools

All of the tools used during the development of this program:

#### Integrated Development Environment (IDE)

- Visual Studio

#### Relational Database Management System (RDBMS)

- SQL Server 2022

#### Database Client (GUI)

- Microsoft SQL Server Management Studio 19

#### Dependencies / Packages

##### Clientside:

- Microsoft.AspNetCore..Components.WebAssembly (6.0.22)
- Microsoft.AspNetCore..Components.WebAssembly.Authentication (6.0.22)
- Microsoft.AspNetCore..Components.WebAssembly.DevServer (6.0.22)
- Microsoft.Extensions.Http (6.0.0)
- MudBlazor (6.11.0)

##### Serverside:

- Microsoft.AspNetCore.ApiAuthorization.Dientityserver (6.0.22)
- Microsoft.AspNetCore.Authentication.JwtBearer (6.0.22)
- Microsoft.AspNetCore.Components.WebAssembly.Server (6.0.22)
- Microsoft.AspNetCore.Diagnostics.EntityFrameworkCore (6.0.22)
- Microsoft.AspNetCore.Identity.EntityFrameworkCore (6.0.23)
- Microsoft.AspNetCore.Identity.UI (6.0.23)
- Microsoft.EntityFrameworkCore.Sqlite (6.0.23)
- Microsoft.EntityFrameworkCore.SqlServer (6.0.23)
- Microsoft.EntityFrameworkCore.Tools (6.0.23)
- Microsoft.IdentityModel.Tokens (6.33.0)
- Microsoft.VisualStudio.Web.CodeGeneration.Design (6.0.16)
- System.IdentityModel.Tokens.Jwt (6.33.0)

#### Middleware

- Identity Authentication

#### Object Relational Mapping (ORM)

- Entity Framework

#### Framework

- .NET Core 6

#### Languages

- C#
- SQL
- JavaScript
- HTML5
- CSS3

## Contributors

<p align="center">
  <a href="https://github.com/lordstimpa/SPA-Game-Project/graphs/contributors">
    <img src="https://contrib.rocks/image?repo=lordstimpa/SPA-Game-Project" alt="Contributors">
  </a>
</p>

<p align="center">
  <a href="https://www.github.com/lordstimpa">Steven Dalfall</a> | <a href="https://www.github.com/L-stal">Leo Stålenhag</a>
</p>

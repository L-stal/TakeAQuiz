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

### Files uploaded by user logic

When a user loads a media file during the creation of a quiz the method below will be run on change. It handles the logical steps in ensuring that the file is saved to list without any issues. Issues such as the file exceeding the size limit and also making sure that the file is prepared for being transfered to the server side by running a convertion method.

```
private async Task LoadFileAsync(InputFileChangeEventArgs e, int currentIndex)
{
    file = e.File;

    if (file != null)
    {
        if (file.Size > maxFileSize)
        {
            errors.Add($"Error: File size exceeds the limit of {maxFileSize / (1024 * 1024)} MB");
            return;
        }

        var uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.Name);

        var fileView = new FileUploadViewModel
        {
            NewFileName = uniqueFileName,
            FileBytes = await ConvertFileToByteArray(file,maxFileSize)
        };

        try
        {
            if (currentIndex >= 0 && currentIndex < files.Count)
            {
                currentQuestion.Media = files[currentIndex].NewFileName;
            }
            else
            {
                files.Add(fileView);
                currentQuestion.Media = uniqueFileName;
            }
        }
        catch (Exception ex)
        {
            errors.Add($"Error: {ex.Message}");
        }
    }
}
```

### Convert file to bytes method

This is the method which is run on each file uploaded by the user. The file will be converted to an array of bytes which will be saved on the RAM for the duration of its use. The duration it will be saved is until the quiz has been aborted or submitted to the database and the files themselves have been transfered to a folder on the server-side.

```
private async Task<byte[]> ConvertFileToByteArray(IBrowserFile file, long maxSize)
{

    using (var ms = new MemoryStream())
    {
        await file.OpenReadStream(maxSize).CopyToAsync(ms);
        return ms.ToArray();
    }
}
```

## Reflections

### Design

We decided to use MudBlazor for styling which is a component library specifically suited for Blazor. It saved us a lot of time by using ready made components for all of the different front-end specific functionality. The time which was saved we used for learning more in-depth about Blazor WebAssembly and adding more features to the application.

Since the project had a time-frame of 4 weeks until completion we decided to use MudBlazor. But if it was the right decision is questionable, our inexperience of using the library caused issues which never would have happened if we were to use traditional CSS or Bootstrap. The advantage of using MudBlazor was that we were able to connect the backend logic with the frontend functionality in a very short time.

We have prioritized the functionality in our application and therefor the design of the layout is lacking a unique style.

### Performance

Currently we are not using any sort of logic to store data in the cache storage or in cookies. If we were to add some sort of momentary storage then that would save us additional loadtimes which are caused by multiple API calls.

Our entire application is dependant on API calls made for gathering all of our data from our database. No data is saved for later use except for the login details.

The application as a whole is working well. We have implemented a functionality which allows the user to upload media files which are transfered suprisingly fast to a folder on the server. This is made by momentarily converting the files into bytes of data for the duration of the transfer from client to server. Thereafter it is converted back into the original format once it has reached the server.

This is good for the moment but if we were to publish this project into a production environment with its own domain and such, then we would have to implement another method since the traffic will affect this type of file transfer. The loading time will be longer since the memory of RAM sticks will be exceeded.

### Scalability

We have setup a good foundation of our project which increases the possibility for scaling of the application. API endpoints, major functionality and other parts are made with separation of concern in mind during development. This makes it easier to navigate and also add functionality to already developed code.

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

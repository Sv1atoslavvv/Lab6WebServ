# Lab6WebServ

ASP.NET Core MVC application for laboratory work No. 6. It continues the website from Lab 5 and moves the navigation bar into an MVC ViewComponent.

## Solution structure

- `EmailSenderWeb` — MVC application with the `NavbarViewComponent` and its Razor view.
- `EmailSenderWeb.Services` — email sending service built with MailKit.
- `EmailSenderWeb/Views/Shared/Components/Navbar/Default.cshtml` — localized navbar markup, including the language selector.

The website has a top navigation menu and no separate sidebar, so this project implements one Navbar ViewComponent as allowed by the assignment note.

## Run

Open `EmailSenderLab6.sln` in Visual Studio, or run from the repository root:

```powershell
dotnet restore .\EmailSenderLab6.sln
dotnet build .\EmailSenderLab6.sln --no-restore
dotnet run --project .\EmailSenderWeb\EmailSenderWeb.csproj
```

SMTP credentials are configured locally through user secrets or environment variables. Do not commit credentials.

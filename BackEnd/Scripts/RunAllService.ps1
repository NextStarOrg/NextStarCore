dotnet build ..\NextStarSolution\NextStarSolution.sln
start-process -filepath "powershell" -WindowStyle Minimized -argumentlist "-file .\RunIdentity.ps1"
start-process -filepath "powershell" -WindowStyle Minimized -argumentlist "-file .\RunManageGateway.ps1"
start-process -filepath "powershell" -WindowStyle Minimized -argumentlist "-file .\RunBlogServiceAPI.ps1"

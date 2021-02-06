@echo off 
set user_microservice_path=%cd%/userMicroservice/User
set score_microservice_path=%cd%/scoreMicroservice/Score
set game_microservice_path=%cd%/gameMicroservice/Game
start /min cmd /c  && cd %user_microservice_path% && start dotnet ef database update
start /min cmd /c  && cd %score_microservice_path% && start dotnet ef database update
start /min cmd /c  && cd %game_microservice_path% && start dotnet ef database update
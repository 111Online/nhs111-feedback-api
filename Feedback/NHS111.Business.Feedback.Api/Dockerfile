FROM microsoft/aspnet:4.7

WORKDIR /inetpub/wwwroot

ARG source
COPY ./obj/octopacked .
COPY ./start.ps1 .

# RENAME THE OCTOPACK FILE 
RUN dir .\ -Filter *.nupkg | Rename-Item -newname { [io.path]::ChangeExtension($_.name, "zip") }
# EXPAND THE FILE
RUN dir .\ -Filter *.zip | Expand-Archive -Path {$_.FullName} -DestinationPath .

RUN Set-WebBinding 'Default Web Site' -BindingInformation "*:80:" -PropertyName "Port" -Value "87"
CMD powershell ./start.ps1
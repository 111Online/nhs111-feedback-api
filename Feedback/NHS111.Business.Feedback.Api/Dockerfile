FROM microsoft/aspnet:4.7

WORKDIR /inetpub/wwwroot

ARG source
COPY ./obj/Docker/publish/_PublishedWebsites/nhs111-feeback-api .
COPY ./replace_appsettings.ps1 .

RUN Set-WebBinding 'Default Web Site' -BindingInformation "*:80:" -PropertyName "Port" -Value "87"
CMD powershell ./replace_appsettings.ps1
FROM microsoft/aspnetcore
WORKDIR /app
EXPOSE 80
COPY ./published .
ENTRYPOINT ["dotnet", "app.dll"]

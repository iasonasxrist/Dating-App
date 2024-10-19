# Stage 1: Build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copy the solution file
COPY . "DatingApp"

# Copy the project files
COPY ["./DatingApp.csproj", "DatingApp/DatingApp/"]

# Restore dependencies
WORKDIR "/src/DatingApp"
RUN dotnet restore "DatingApp.csproj"

# Copy the rest of the application code
COPY . .

# Build the project
WORKDIR "/src/DatingApp"
RUN dotnet build "DatingApp.csproj" -c Release -o /app/build

# Stage 2: Publish the app
FROM build AS publish
WORKDIR "/src/DatingApp"
RUN dotnet publish "DatingApp.csproj" -c Release -o /app/publish

# Stage 3: Final image to run the app
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80

# Copy the published files from the build container
COPY --from=publish /app/publish .

# Entry point to run the application
ENTRYPOINT ["dotnet", "DatingApp.dll"]

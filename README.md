# Dating App

## Introduction

This Dating App is a modern platform for users to connect and interact. It uses Angular for the frontend and .NET Core for the backend, featuring role-based authentication, AutoMapper for data transfer objects, Cloudinary for image handling, and SQL Server for database management.

## Technologies Used

- **Frontend**: Angular
- **Backend**: .NET Core
- **Authentication**: JWT (JSON Web Tokens)
- **Object Mapping**: AutoMapper
- **Image Storage & CDN**: Cloudinary
- **Database**: SQL Server

## Setup and Installation

### Prerequisites

- **Node.js**: [Download & Install](https://nodejs.org/en/download/)
- **.NET Core SDK**: [Download & Install](https://dotnet.microsoft.com/download)
- **SQL Server**: [Download & Install](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- **Angular CLI**: 


### Getting Started

1. **Clone the repository**



### Backend Setup

1. **Navigate to the backend directory**

2. **Restore .NET dependencies**

3. **Update the database**

4. **Run the backend**



### Frontend Setup

1. **Navigate to the frontend directory**

2. **Install npm packages**

3. **Start the Angular server**


4. **Access the app at**: `http://localhost:4200`

## Configuration

Update the environment configuration files in both the frontend (`/Frontend/src/environments`) and backend (`/Backend/appsettings.json`) projects with your settings:

- **Database connection strings**
- **Authentication secrets**
- **Cloudinary API keys**

## Roles

- **Admin**: Full administrative access.
- **User**: Access to basic profile and interaction features.

## AutoMapper

AutoMapper is configured to map database entities to Data Transfer Objects (DTOs). The mappings are set up in the AutoMapper profiles in the backend project.

## Cloudinary Integration

Images are managed through Cloudinary. Set up your Cloudinary settings in the backend configuration file to enable image uploads.

## Contributing

Contributions are welcome! Please read the [contributing guidelines](CONTRIBUTING.md) before making a pull request.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE.md) file for details.

# Connectly

## Project Overview

Connectly is social network project that aims to develop a web-based social networking platform that enables users to connect, share, and interact with each other. The platform will offer features such as user profiles, friend requests and a news feed, providing an engaging and interactive user experience.

## Key Features

- **User Authentication**: Secure user registration and login system.
- **Friendship Management**: Ability to send, accept, and manage friend requests.
- **News Feed**: A dynamic feed displaying posts, updates, and activities from friends.
- **Post Creation and Interaction**: Users can create posts and share content.
 
## Technologies Used

- **Frontend**: HTML, CSS, JavaScript
- **Backend**: C#
- **Database**: Microsoft SQL Server

## Getting Started

### Prerequisites
Ensure you have the following installed on your system:

- .NET SDK (version 6.0 or higher)

- A compatible database (SQL Server, MySQL, or SQLite)

### Compilation
#### 1. Clone repository

Open a terminal and run the following command to clone the project repository:

- git clone https://github.com/dimitardimitrov05/Social-Network.git

#### 2. Restore Dependencies

Run the following command to restore all required NuGet packages:

- dotnet restore

#### 3. Build the Project

To compile the project, use the following command:

- dotnet build

### Installation and Configuration

#### 1 .Database Configuration

Update the database connection string in the appsettings.json file, located in the project’s root directory, with your SQL server

#### 2. Apply Migrations

Open up the Package Manager Console and run the migrations to set up the database schema:

- Update-Database

#### 3. Set initial user

On the first launch, the system will automatically create an admin user:

- Username: admin
- Password: admin

### Running the project

#### 1. Launch the application

The application will be accessible at http://localhost by default.

#### 2. Login

To access the system, login with the initial admin user.

### Main Features

#### Registration

-Users can register only if they have received an invitation with a unique code.

#### Login/Logout

-Users can log in with a valid username and password.

#### Friend Management

-Users can send, accept, and decline friend requests.

#### Timeline

-Users can view and post on their timeline, as well as see posts from friends and friends of friends.

## Contact

This concludes the setup guide for this project. If you need further assistance you can reach me via email:
- **Full name**: Dimitar Dimitrov
- **Email**: d_dimitrov1005@abv.bg

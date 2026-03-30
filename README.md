# Fitness Hub

A full-stack ASP.NET Core MVC web application for fitness enthusiasts with user authentication, blog features, and comments.

## Features

- Home page with fitness overview
- Exercises list and details
- Blog with fitness tips and articles
- **Comments system**: Users can comment on blog posts
- User authentication (Login/Register)
- Authenticated users can create and publish blog posts
- Built with SOLID principles
- Minimal and clean UI design

## User Credentials

**Demo Account:**
- Username: `admin`
- Password: `admin123`

Or register a new account to create your own blog posts and comments.

## Getting Started

1. Ensure .NET 10 SDK is installed.
2. Run `dotnet build` to build the project.
3. Run `dotnet run` to start the application.
4. Open http://localhost:5022 in your browser.

## Features in Detail

### Blog Comments
- Browse blog posts and read comments
- Add comments to any blog post (logged in or as Anonymous)
- Comments display author name and timestamp
- Comments are ordered by most recent first

### Authentication
- Session-based authentication
- Password hashing with SHA256
- Login and Register functionality
- Only authenticated users can create blog posts

## Project Structure

- **Controllers/**: Handle HTTP requests
  - `HomeController`: Home page
  - `ExercisesController`: Exercises management
  - `BlogController`: Blog posts and comments management
  - `AuthController`: User authentication
- **Models/**: Data models
  - `Exercise`, `BlogPost`, `User`, `Comment`
- **Services/**: Business logic
  - `ExerciseService`, `BlogService`, `AuthService`, `CommentService`
- **Interfaces/**: Service contracts
- **Views/**: UI templates (Razor)

## Database

Currently uses in-memory storage for simplicity. Can be easily extended to use a real database (SQL Server, PostgreSQL, etc.).
# Booking Brains - Backend Project (ASP.NET)

## Overview

Booking Brains is an apartment booking platform that connects property owners with renters. This repository contains the backend implementation of the system built with ASP.NET, providing APIs for user management, apartment listings, booking reservations, and review functionality.

## Key Features

### User Management
- Registration and authentication for both regular users and owners
- Role-based access control (User/Owner)
- Profile management

### Apartment Management
- CRUD operations for apartment listings (owners only)
- Photo upload requirements (minimum 4 photos per listing)
- Search and filtering functionality

### Booking System
- Reservation creation with availability validation
- Booking status tracking (Pending/Confirmed/Canceled/Completed)
- Prevention of double bookings
- Booking history for users

### Review System
- Post-stay reviews linked to completed bookings
- One review per booking policy
- Rating system for apartments

## Technical Stack

- **Framework**: ASP.NET Core
- **Database**: Entity Framework Core with SQL Server
- **Authentication**: JWT Bearer Tokens
- **API Documentation**: Swagger/OpenAPI
- **Pagination**: For search results
- **Email Notifications**: For booking confirmations and updates

## Getting Started

### Prerequisites

- .NET 6.0 SDK or later
- SQL Server
- Visual Studio 2022 or VS Code (with C# extensions)

### Installation

1. **Clone the repository**
   ```bash
   git clone https://github.com/aminatpwk/Booking-App
   cd Booking-App
   ```

2. **Configure the connection string in `appsettings.json`**
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "YOUR_SQL_URL"
     }
   }
   ```

3. **Run database migrations**
   ```bash
   dotnet ef database update
   ```

4. **Run the application**
   ```bash
   dotnet run
   ```

The application will start and be available at `https://localhost:7157` or `http://localhost:5184`.

## API Endpoints

The API follows RESTful conventions with these main resources:

- **`/api/users`** - User registration and authentication
- **`/api/apartments`** - Apartment listings and search
- **`/api/bookings`** - Booking management
- **`/api/reviews`** - Review submissions

### API Documentation

Access Swagger documentation at `/swagger` when running the application for detailed API endpoint information.

## Business Rules

- Only owners can create/update apartment listings
- Bookings require availability validation
- Reviews can only be submitted for completed bookings
- All apartment listings must include at least 4 photos
- Users can only review apartments they've actually stayed in

## License

This project is proprietary software developed by Amina.

---

*treat people with kindness :)*

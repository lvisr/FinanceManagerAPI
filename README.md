# Finance Manager API
Finance Manager API is a **RESTful web service** built with **ASP.NET Core** and **Entity Framework Core**.  
It helps users **manage their finances** by tracking accounts, transactions, and balances.

## Features
- **User Authentication** (JWT-based)
- **Account Management** (Checking, Savings, Cash, Debt)
- **Transaction Tracking** (Income, Expenses, Transfers)
- **Seeding Initial Data**
- **Secure Password Hashing**
- **SQLite Database** (Using EF Core)

## Authentication & Security
Uses JWT authentication for secure access.
Users must log in to get a Bearer Token.
All protected routes require the Authorization: Bearer <token> header.

## Technologies Used
- ASP.NET Core 8
- Entity Framework Core 8
- SQLite (for development)
- JWT Authentication
- Swagger UI (for API documentation)
- C# 12

## Endpoints
### USER Controller
| HTTP Method |	Endpoint |	Auth Required |	Description |
| ------------- | ------------- | ------------- | ------------- |
| POST |	/api/users/register |	❌ |	Register a new user |
| POST |	/api/users/login |	❌ |	Authenticate & Get Token |
| GET |	/api/users/profile |	✅ |	Get user profile |
| PUT |	/api/users/profile |	✅ |	Update user profile |

### ACCOUNT Controller			
| HTTP Method |	Endpoint |	Auth Required |	Description |
| ------------- | ------------- | ------------- | ------------- |
| POST |	/api/accounts |	✅ |	Create a new account |
| GET |	/api/accounts |	✅ |	Get all accounts for user |
| GET |	/api/accounts/{id} |	✅ |	Get details of a specific account |
| PUT |	/api/accounts/{id} |	✅ |	Update an account |
| DELETE |	/api/accounts/{id} |	✅ |	Delete an account |

### TRANSACTION Controller			
| HTTP Method |	Endpoint |	Auth Required |	Description |
| ------------- | ------------- | ------------- | ------------- |
| POST |	/api/transactions |	✅ |	Create a new transaction |
| GET |	/api/transactions |	✅ |	Get all transactions for user |
| GET |	/api/transactions/account/{id} |	✅ |	Get transactions for an account |
| DELETE |	/api/transactions/{id} |	✅ |	Delete a transaction |


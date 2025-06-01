# WebApplication1

A comprehensive ASP.NET Core 9.0 web application showcasing multiple architectural patterns including MVC, Razor Pages, Minimal APIs, and traditional API Controllers with built-in Identity authentication and jQuery-enhanced frontend.

## 🏗️ Architecture Overview

This application demonstrates the flexibility of ASP.NET Core by combining multiple architectural patterns:

- **MVC (Model-View-Controller)** - Traditional web application pattern for structured web pages
- **Razor Pages** - Page-focused development model for simpler scenarios
- **Minimal APIs** - Lightweight approach for creating HTTP APIs with minimal code
- **API Controllers** - Full-featured controllers for complex API scenarios
- **ASP.NET Core Identity** - Complete authentication and authorization system

## 🚀 Features

- ✅ **Multi-pattern Architecture** - MVC, Razor Pages, Minimal APIs, and API Controllers
- ✅ **Authentication & Authorization** - ASP.NET Core Identity with user registration/login
- ✅ **Entity Framework Core** - Data access with SQL Server support
- ✅ **Docker Support** - Containerized deployment ready
- ✅ **Modern UI** - Bootstrap-based responsive design with jQuery enhancements
- ✅ **Frontend Interactivity** - jQuery for DOM manipulation, AJAX calls, and UI enhancements
- ✅ **Development Tools** - Hot reload, exception handling, and diagnostics

## 🛠️ Technology Stack

- **Framework**: ASP.NET Core 9.0
- **Language**: C# with nullable reference types
- **Database**: SQL Server / LocalDB
- **ORM**: Entity Framework Core 9.0
- **Authentication**: ASP.NET Core Identity
- **Frontend**: HTML5, CSS3, Bootstrap, JavaScript, jQuery
- **UI Libraries**: jQuery 3.x, Bootstrap 5.x
- **Containerization**: Docker (Linux containers)

## 📋 Prerequisites

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [SQL Server LocalDB](https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb) or SQL Server
- [Docker Desktop](https://www.docker.com/products/docker-desktop) (optional, for containerization)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)

## 🚀 Getting Started

### 1. Clone the Repository
```bash
git clone <your-repo-url>
cd WebApplication1
```

### 2. Restore Dependencies
```bash
dotnet restore
```

### 3. Update Database
```bash
dotnet ef database update
```

### 4. Run the Application
```bash
dotnet run
```

The application will be available at:
- HTTPS: `https://localhost:5001`
- HTTP: `http://localhost:5000`

## 🏃‍♂️ Quick Start with Docker

### Build and Run with Docker
```bash
# Build the Docker image
docker build -t webapplication1 .

# Run the container
docker run -p 8080:8080 webapplication1
```

Access the application at `http://localhost:8080`

## 📁 Project Structure

```
WebApplication1/
├── Areas/
│   └── Identity/           # Identity UI customizations
├── Controllers/            # MVC and API Controllers
│   └── HomeController.cs   # Main MVC controller
├── Data/
│   ├── ApplicationDbContext.cs  # EF Core context
│   └── Migrations/         # Database migrations
├── Models/                 # Data models and view models
├── Views/                  # MVC views and layouts
│   ├── Home/              # Home controller views
│   └── Shared/            # Shared layouts and partials
├── wwwroot/               # Static files (CSS, JS, images)
│   ├── css/               # Custom stylesheets
│   ├── js/                # Custom JavaScript and jQuery files
│   └── lib/               # Third-party libraries (jQuery, Bootstrap)
├── appsettings.json       # Application configuration
├── Program.cs             # Application entry point
├── Dockerfile             # Docker configuration
└── README.md              # This file
```

## 🎯 Application Patterns

### 1. MVC Pattern
Located in `/Controllers` and `/Views`
- Traditional Model-View-Controller architecture
- Server-side rendering with Razor views
- Ideal for complex web applications with rich UI

**Example**: `HomeController.cs` with corresponding views in `/Views/Home/`

### 2. Razor Pages
Configured in `Program.cs` with `MapRazorPages()`
- Page-focused development model
- Co-located page models and views
- Great for simple CRUD operations and forms

### 3. Minimal APIs
Can be added to `Program.cs`
```csharp
app.MapGet("/api/hello", () => "Hello World!");
app.MapPost("/api/data", (MyModel model) => Results.Ok(model));
```

### 4. API Controllers
Traditional controllers decorated with `[ApiController]`
```csharp
[ApiController]
[Route("api/[controller]")]
public class ValuesController : ControllerBase
{
    [HttpGet]
    public IActionResult Get() => Ok(new { message = "Hello from API Controller" });
}
```

## 📊 Models Architecture

The Models layer represents the data structure and business logic of your application. This section covers the different types of models used across MVC, Razor Pages, and API patterns.

### Model Types Overview

#### 1. **Entity Models** (Data Models)
- Represent database tables via Entity Framework Core
- Define the structure of your data
- Include navigation properties for relationships

#### 2. **View Models** 
- Designed specifically for MVC views
- Combine data from multiple entities
- Include UI-specific properties and validation

#### 3. **Page Models**
- Used with Razor Pages
- Contain page-specific logic and data
- Handle HTTP requests for the page

#### 4. **DTO Models** (Data Transfer Objects)
- Used for API communication
- Lightweight objects for data transfer
- Separate internal models from external APIs

#### 5. **Request/Response Models**
- API request and response objects
- Include validation attributes
- Handle API-specific data structures

### Current Models

#### ErrorViewModel
Located in `Models/ErrorViewModel.cs`
```csharp
namespace WebApplication1.Models
{
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }
        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
```

**Purpose**: Handles error display in MVC views, particularly for the Error action in HomeController.

### Model Examples and Patterns

#### 1. Entity Models (Database Models)

```csharp
// Models/Product.cs
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication1.Models
{
    public class Product
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;
        
        // Navigation Properties
        public int CategoryId { get; set; }
        public Category Category { get; set; } = null!;
        
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
    }
}

// Models/Category.cs
namespace WebApplication1.Models
{
    public class Category
    {
        public int Id { get; set; }
        
        [Required]
        [StringLength(50)]
        public string Name { get; set; } = string.Empty;
        
        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}
```

#### 2. View Models (MVC)

```csharp
// Models/ViewModels/ProductListViewModel.cs
namespace WebApplication1.Models.ViewModels
{
    public class ProductListViewModel
    {
        public IEnumerable<Product> Products { get; set; } = new List<Product>();
        public string? SearchTerm { get; set; }
        public int CategoryId { get; set; }
        public IEnumerable<Category> Categories { get; set; } = new List<Category>();
        public PaginationInfo Pagination { get; set; } = new();
    }
    
    public class PaginationInfo
    {
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalItems { get; set; }
    }
}

// Models/ViewModels/ProductCreateViewModel.cs
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.ViewModels
{
    public class ProductCreateViewModel
    {
        [Required(ErrorMessage = "Product name is required")]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters")]
        [Display(Name = "Product Name")]
        public string Name { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "Price is required")]
        [Range(0.01, 999999.99, ErrorMessage = "Price must be between $0.01 and $999,999.99")]
        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters")]
        [Display(Name = "Description")]
        [DataType(DataType.MultilineText)]
        public string? Description { get; set; }
        
        [Required(ErrorMessage = "Please select a category")]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }
        
        public IEnumerable<Category> AvailableCategories { get; set; } = new List<Category>();
    }
}
```

#### 3. Page Models (Razor Pages)

```csharp
// Pages/Products/Index.cshtml.cs
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Data;
using WebApplication1.Models;

namespace WebApplication1.Pages.Products
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Product> Products { get; set; } = default!;
        
        [BindProperty(SupportsGet = true)]
        public string? SearchString { get; set; }
        
        [BindProperty(SupportsGet = true)]
        public int? CategoryId { get; set; }

        public async Task OnGetAsync()
        {
            var products = from p in _context.Products.Include(p => p.Category)
                          select p;

            if (!string.IsNullOrEmpty(SearchString))
            {
                products = products.Where(p => p.Name.Contains(SearchString));
            }

            if (CategoryId.HasValue)
            {
                products = products.Where(p => p.CategoryId == CategoryId);
            }

            Products = await products.ToListAsync();
        }
    }
}
```

#### 4. DTO Models (API)

```csharp
// Models/DTOs/ProductDto.cs
namespace WebApplication1.Models.DTOs
{
    public class ProductDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string? Description { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public bool IsActive { get; set; }
    }
    
    public class ProductCreateDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [Range(0.01, double.MaxValue)]
        public decimal Price { get; set; }
        
        [StringLength(500)]
        public string? Description { get; set; }
        
        [Required]
        public int CategoryId { get; set; }
    }
    
    public class ProductUpdateDto : ProductCreateDto
    {
        public bool IsActive { get; set; } = true;
    }
}
```

#### 5. Request/Response Models

```csharp
// Models/Requests/ProductSearchRequest.cs
namespace WebApplication1.Models.Requests
{
    public class ProductSearchRequest
    {
        public string? SearchTerm { get; set; }
        public int? CategoryId { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int Page { get; set; } = 1;
        public int PageSize { get; set; } = 10;
        public string SortBy { get; set; } = "Name";
        public string SortDirection { get; set; } = "asc";
    }
}

// Models/Responses/ApiResponse.cs
namespace WebApplication1.Models.Responses
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public T? Data { get; set; }
        public List<string> Errors { get; set; } = new();
        
        public static ApiResponse<T> SuccessResult(T data, string? message = null)
        {
            return new ApiResponse<T>
            {
                Success = true,
                Data = data,
                Message = message
            };
        }
        
        public static ApiResponse<T> ErrorResult(string message, List<string>? errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }
    
    public class PagedResponse<T>
    {
        public IEnumerable<T> Data { get; set; } = new List<T>();
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public bool HasPrevious => CurrentPage > 1;
        public bool HasNext => CurrentPage < TotalPages;
    }
}
```

### Model Validation

#### Data Annotations
```csharp
using System.ComponentModel.DataAnnotations;

public class UserRegistrationModel
{
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;
    
    [Required(ErrorMessage = "Password is required")]
    [StringLength(100, MinimumLength = 6, ErrorMessage = "Password must be at least 6 characters")]
    [DataType(DataType.Password)]
    public string Password { get; set; } = string.Empty;
    
    [Compare("Password", ErrorMessage = "Passwords do not match")]
    [DataType(DataType.Password)]
    public string ConfirmPassword { get; set; } = string.Empty;
    
    [Range(18, 120, ErrorMessage = "Age must be between 18 and 120")]
    public int Age { get; set; }
    
    [Phone(ErrorMessage = "Invalid phone number")]
    public string? PhoneNumber { get; set; }
    
    [Url(ErrorMessage = "Invalid URL format")]
    public string? Website { get; set; }
}
```

#### Custom Validation Attributes
```csharp
// Models/Validation/FutureDateAttribute.cs
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models.Validation
{
    public class FutureDateAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value is DateTime date)
            {
                return date > DateTime.Now;
            }
            return true; // Let Required attribute handle null values
        }
        
        public override string FormatErrorMessage(string name)
        {
            return $"{name} must be a future date.";
        }
    }
}

// Usage
public class EventModel
{
    [Required]
    [FutureDate(ErrorMessage = "Event date must be in the future")]
    public DateTime EventDate { get; set; }
}
```

### Model Mapping

#### AutoMapper Configuration
```csharp
// Add to Program.cs
builder.Services.AddAutoMapper(typeof(Program));

// Profiles/MappingProfile.cs
using AutoMapper;
using WebApplication1.Models;
using WebApplication1.Models.DTOs;

namespace WebApplication1.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name));
                
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
        }
    }
}
```

#### Manual Mapping Extensions
```csharp
// Extensions/ModelExtensions.cs
namespace WebApplication1.Extensions
{
    public static class ModelExtensions
    {
        public static ProductDto ToDto(this Product product)
        {
            return new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Description = product.Description,
                CategoryName = product.Category?.Name ?? "",
                CreatedAt = product.CreatedAt,
                IsActive = product.IsActive
            };
        }
        
        public static Product ToEntity(this ProductCreateDto dto)
        {
            return new Product
            {
                Name = dto.Name,
                Price = dto.Price,
                Description = dto.Description,
                CategoryId = dto.CategoryId
            };
        }
    }
}
```

### Model Best Practices

#### 1. **Separation of Concerns**
```
Models/
├── Entities/           # Database entities
├── ViewModels/         # MVC view models
├── DTOs/              # API data transfer objects
├── Requests/          # API request models
├── Responses/         # API response models
└── Validation/        # Custom validation attributes
```

#### 2. **Naming Conventions**
- **Entities**: `Product`, `Category`, `User`
- **View Models**: `ProductListViewModel`, `ProductCreateViewModel`
- **DTOs**: `ProductDto`, `ProductCreateDto`
- **Requests**: `ProductSearchRequest`, `CreateProductRequest`
- **Responses**: `ProductResponse`, `ApiResponse<T>`

#### 3. **Nullable Reference Types**
```csharp
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;  // Required, default value
    public string? Description { get; set; }          // Optional, can be null
}
```

#### 4. **Entity Framework Configuration**
Update `ApplicationDbContext.cs`:
```csharp
public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }
    
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        // Configure entity relationships
        modelBuilder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(c => c.Products)
            .HasForeignKey(p => p.CategoryId);
            
        // Seed data
        modelBuilder.Entity<Category>().HasData(
            new Category { Id = 1, Name = "Electronics" },
            new Category { Id = 2, Name = "Books" }
        );
    }
}
```

### Model Testing

```csharp
// Tests/Models/ProductTests.cs
using Xunit;
using WebApplication1.Models;

namespace WebApplication1.Tests.Models
{
    public class ProductTests
    {
        [Fact]
        public void Product_ShouldHaveDefaultValues()
        {
            // Arrange & Act
            var product = new Product();
            
            // Assert
            Assert.True(product.IsActive);
            Assert.True(product.CreatedAt <= DateTime.UtcNow);
        }
        
        [Theory]
        [InlineData("Test Product", 19.99, true)]
        [InlineData("Another Product", 0.01, true)]
        public void Product_ShouldSetPropertiesCorrectly(string name, decimal price, bool isActive)
        {
            // Arrange & Act
            var product = new Product
            {
                Name = name,
                Price = price,
                IsActive = isActive
            };
            
            // Assert
            Assert.Equal(name, product.Name);
            Assert.Equal(price, product.Price);
            Assert.Equal(isActive, product.IsActive);
        }
    }
}
```

## 🔐 Authentication & Security

### ASP.NET Core Identity
- User registration and login
- Password requirements and validation
- Email confirmation support
- Role-based authorization ready

### Configuration
Identity is configured in `Program.cs`:
```csharp
builder.Services.AddDefaultIdentity<IdentityUser>(options => 
    options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
```

## 🗄️ Database

### Entity Framework Core
- **Provider**: SQL Server
- **Context**: `ApplicationDbContext`
- **Migrations**: Automatic database schema management

### Connection String
Update in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=aspnet-WebApplication1-{guid};Trusted_Connection=True;MultipleActiveResultSets=true"
  }
}
```

### Common EF Commands
```bash
# Add a new migration
dotnet ef migrations add MigrationName

# Update database
dotnet ef database update

# Remove last migration
dotnet ef migrations remove
```

## 🔧 Configuration

### Development Environment
- **Hot Reload**: Enabled for rapid development
- **Developer Exception Page**: Detailed error information
- **Database Error Page**: EF Core-specific error handling

### Production Environment
- **Exception Handling**: Custom error pages
- **HSTS**: HTTP Strict Transport Security enabled
- **Static File Caching**: Optimized asset delivery

## 📝 API Documentation

### Endpoints

#### MVC Routes
- `GET /` - Home page
- `GET /Home/Privacy` - Privacy page
- `GET /Home/Error` - Error page

#### Identity Routes
- `GET /Identity/Account/Register` - User registration
- `GET /Identity/Account/Login` - User login
- `POST /Identity/Account/Logout` - User logout

#### API Endpoints (Add as needed)
```csharp
// Example Minimal API endpoints
app.MapGet("/api/health", () => Results.Ok(new { status = "healthy" }));
app.MapGet("/api/version", () => Results.Ok(new { version = "1.0.0" }));

// Example API Controller endpoints
[Route("api/[controller]")]
public class WeatherController : ControllerBase
{
    [HttpGet]
    public IActionResult GetWeather() => Ok(/* weather data */);
}
```

## 🧪 Testing

### Unit Testing Setup
```bash
# Create test project
dotnet new xunit -n WebApplication1.Tests

# Add reference to main project
cd WebApplication1.Tests
dotnet add reference ../WebApplication1/WebApplication1.csproj

# Run tests
dotnet test
```

## 🚀 Deployment

### Local Deployment
```bash
dotnet publish -c Release -o ./publish
```

### Docker Deployment
```bash
# Build and tag
docker build -t webapplication1:latest .

# Run container
docker run -d -p 8080:8080 --name myapp webapplication1:latest
```

### Azure Deployment
- Use Visual Studio publish profiles
- Azure App Service compatible
- Supports SQL Azure databases

## 🔧 Development Tips

### Adding New Features

#### 1. Add a new MVC Controller
```bash
# Using CLI
dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
dotnet aspnet-codegenerator controller -name ProductsController -m Product -dc ApplicationDbContext --relativeFolderPath Controllers --useDefaultLayout
```

#### 2. Add a new Razor Page
```bash
dotnet aspnet-codegenerator razorpage -m Product -dc ApplicationDbContext -udl -outDir Pages/Products --referenceScriptLibraries
```

#### 3. Add a new API Controller
```csharp
[ApiController]
[Route("api/[controller]")]
public class ProductsApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    
    public ProductsApiController(ApplicationDbContext context)
    {
        _context = context;
    }
    
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
    {
        return await _context.Products.ToListAsync();
    }
}
```

## 📚 Learning Resources

- [ASP.NET Core Documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- [Entity Framework Core](https://docs.microsoft.com/en-us/ef/core/)
- [ASP.NET Core Identity](https://docs.microsoft.com/en-us/aspnet/core/security/authentication/identity)
- [Minimal APIs](https://docs.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis)

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## 🐛 Troubleshooting

### Common Issues

#### Database Connection Issues
```bash
# Reset database
dotnet ef database drop
dotnet ef database update
```

#### Package Restore Issues
```bash
# Clear NuGet cache
dotnet nuget locals all --clear
dotnet restore
```

#### Docker Build Issues
```bash
# Clean Docker cache
docker system prune -f
docker build --no-cache -t webapplication1 .
```

## 📞 Support

For support and questions:
- Create an issue in this repository
- Check the [ASP.NET Core documentation](https://docs.microsoft.com/en-us/aspnet/core/)
- Visit [Stack Overflow](https://stackoverflow.com/questions/tagged/asp.net-core)

## 🎨 Frontend Enhancement with jQuery

### jQuery Integration

jQuery is integrated into the application to provide enhanced user interactions and AJAX functionality. The library is typically included via CDN or bundled with the application.

### Adding jQuery to Your Views

#### Option 1: CDN Integration (Recommended for development)
Add to your layout file (`Views/Shared/_Layout.cshtml`):
```html
<!-- jQuery CDN -->
<script src="https://code.jquery.com/jquery-3.7.1.min.js" 
        integrity="sha256-/JqT3SQfawRcv/BIHPThkBvs0OEvtFFmqPF/lYI/Cxo=" 
        crossorigin="anonymous"></script>

<!-- Your custom jQuery scripts -->
<script src="~/js/site.js"></script>
```

#### Option 2: Local Files
Download jQuery and place in `wwwroot/lib/jquery/` directory:
```html
<script src="~/lib/jquery/jquery.min.js"></script>
<script src="~/js/site.js"></script>
```

### jQuery Usage Examples

#### 1. DOM Manipulation
```javascript
// Document ready
$(document).ready(function() {
    // Hide/Show elements
    $('#toggleButton').click(function() {
        $('#content').toggle();
    });
    
    // Add CSS classes
    $('.highlight').addClass('active');
    
    // Event handling
    $('.btn-custom').on('click', function(e) {
        e.preventDefault();
        $(this).addClass('clicked');
    });
});
```

#### 2. AJAX API Calls
```javascript
// GET request to API endpoint
function loadData() {
    $.ajax({
        url: '/api/data',
        type: 'GET',
        dataType: 'json',
        success: function(data) {
            $('#dataContainer').html(JSON.stringify(data));
        },
        error: function(xhr, status, error) {
            console.error('Error loading data:', error);
        }
    });
}

// POST request with JSON data
function saveData(formData) {
    $.ajax({
        url: '/api/save',
        type: 'POST',
        contentType: 'application/json',
        data: JSON.stringify(formData),
        success: function(response) {
            alert('Data saved successfully!');
        },
        error: function(xhr, status, error) {
            alert('Error saving data: ' + error);
        }
    });
}
```

#### 3. Form Enhancement
```javascript
// Form validation and submission
$('#myForm').on('submit', function(e) {
    e.preventDefault();
    
    // Collect form data
    var formData = {
        name: $('#name').val(),
        email: $('#email').val(),
        message: $('#message').val()
    };
    
    // Validate
    if (!formData.name || !formData.email) {
        $('.error-message').show().text('Please fill in all required fields');
        return;
    }
    
    // Submit via AJAX
    $.post('/api/contact', formData)
        .done(function(response) {
            $('.success-message').show().text('Message sent successfully!');
            $('#myForm')[0].reset();
        })
        .fail(function() {
            $('.error-message').show().text('Error sending message');
        });
});
```

#### 4. Dynamic Content Loading
```javascript
// Load content dynamically
function loadPartialView(url, container) {
    $(container).html('<div class="spinner">Loading...</div>');
    
    $.get(url)
        .done(function(data) {
            $(container).html(data);
        })
        .fail(function() {
            $(container).html('<p class="error">Failed to load content</p>');
        });
}

// Usage
$('#loadContentBtn').click(function() {
    loadPartialView('/Home/PartialView', '#contentArea');
});
```

### jQuery with ASP.NET Core Features

#### 1. Anti-Forgery Token Support
```javascript
// Get anti-forgery token
function getAntiForgeryToken() {
    return $('input[name="__RequestVerificationToken"]').val();
}

// Include in AJAX requests
$.ajaxSetup({
    beforeSend: function(xhr, settings) {
        if (settings.type === 'POST' || settings.type === 'PUT' || settings.type === 'DELETE') {
            xhr.setRequestHeader('RequestVerificationToken', getAntiForgeryToken());
        }
    }
});
```

#### 2. Working with Razor Pages
```javascript
// Handle Razor Page forms with jQuery
$('form[data-ajax="true"]').on('submit', function(e) {
    e.preventDefault();
    
    var form = $(this);
    var url = form.attr('action');
    var data = form.serialize();
    
    $.post(url, data)
        .done(function(response) {
            if (response.success) {
                location.reload();
            } else {
                $('.validation-summary').html(response.errors);
            }
        });
});
```

### Best Practices

#### 1. Organize jQuery Code
Create separate JavaScript files for different functionalities:
```
wwwroot/js/
├── site.js              # Main site-wide jQuery code
├── modules/
│   ├── forms.js         # Form handling
│   ├── ajax.js          # AJAX utilities
│   └── ui-enhancements.js # UI improvements
```

#### 2. Use Namespacing
```javascript
// Create application namespace
var MyApp = MyApp || {};

MyApp.Forms = {
    init: function() {
        this.bindEvents();
    },
    
    bindEvents: function() {
        $('form.ajax-form').on('submit', this.handleSubmit);
    },
    
    handleSubmit: function(e) {
        // Handle form submission
    }
};

// Initialize on document ready
$(document).ready(function() {
    MyApp.Forms.init();
});
```

#### 3. Error Handling
```javascript
// Global AJAX error handler
$(document).ajaxError(function(event, xhr, options, error) {
    console.error('AJAX Error:', error);
    
    // Show user-friendly error message
    if (xhr.status === 401) {
        window.location.href = '/Account/Login';
    } else {
        alert('An error occurred. Please try again.');
    }
});
```

### Common jQuery Plugins Integration

#### 1. jQuery Validation
```html
<script src="https://cdn.jsdelivr.net/npm/jquery-validation@1.19.5/dist/jquery.validate.min.js"></script>
<script src="https://cdn.jsdelivr.net/npm/jquery-validation-unobtrusive@3.2.12/dist/jquery.validate.unobtrusive.min.js"></script>
```

#### 2. DataTables for Enhanced Tables
```html
<script src="https://cdn.datatables.net/1.13.7/js/jquery.dataTables.min.js"></script>
<link rel="stylesheet" href="https://cdn.datatables.net/1.13.7/css/jquery.dataTables.min.css">

<script>
$(document).ready(function() {
    $('#dataTable').DataTable({
        ajax: '/api/data',
        columns: [
            { data: 'id' },
            { data: 'name' },
            { data: 'email' }
        ]
    });
});
</script>
```

---

**Happy Coding! 🎉** 
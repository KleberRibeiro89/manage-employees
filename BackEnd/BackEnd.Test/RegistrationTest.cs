using BackEnd.AppService.Application;
using BackEnd.AppService.Domain.Registration.Models.Requests;
using BackEnd.AppService.Domain.Registration.Validator;
using BackEnd.Repository;
using BackEnd.Repository.Entities;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BackEnd.Test;

public class RegistrationTest
{
    private IQueryable<Employee> qryEmployees = new List<Employee>()
    {
        new Employee 
        { 
            Id = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"), 
            FirstName = "admin", 
            LastName = "admin", 
            PositionEmployeeId = Guid.Parse("c3d4c221-c289-4100-8f8b-23e3ca578328"),
            DocNumer = "000000000"
        },
        new Employee
        {
            Id = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc7"),
            FirstName = "Leader",
            LastName = "Leader",
            PositionEmployeeId = Guid.Parse("8aa13f02-fedc-4bdc-8a61-6bd583086332"),
            DocNumer = "111111111111"
        },
        new Employee
        {
            Id = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc6"),
            FirstName = "Employee",
            LastName = "Employee",
            PositionEmployeeId = Guid.Parse("1f563b65-4ee8-4595-aef8-9f00a50a2899"),
            DocNumer = "222222222222"
        },
        new Employee { Id = Guid.NewGuid(), FirstName = "Jane", LastName = "Smith" }
    }.AsQueryable();


    [Fact]
    public async Task CreateEmployeeAsync_ShouldAddToDataBase()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "kleber",
            LastName = "Ribeiro",
            Email = "kleber.ribeiro89@gmail.com",
            DocNumer = "32576251897",
            PositionEmployeeId = Guid.Parse("c3d4c221-c289-4100-8f8b-23e3ca578328"),
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"),
            DateOfBirth = DateTime.Now.AddYears(-35),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);

        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Act
        var service = new RegistrationService(mockDbContext.Object);
        await service.CreateAsync(addEmployeeRequest);

        // Assert
        Assert.True(validate.IsValid);
    }

    [Fact]
    public async Task CreateEmployeeAsync_WithoutName()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "",
            LastName = "Ribeiro",
            Email = "kleber.ribeiro89@gmail.com",
            DocNumer = "32576251897",
            PositionEmployeeId = Guid.Parse("c3d4c221-c289-4100-8f8b-23e3ca578328"),
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"),
            DateOfBirth = DateTime.Now.AddYears(-35),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("'First Name' must not be empty.", validate.Errors.Select(x=>x.ErrorMessage));
    }

    [Fact]
    public async Task CreateEmployeeAsync_WithoutEmail()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "Kleber",
            LastName = "Ribeiro",
            Email = "",
            DocNumer = "32576251897",
            PositionEmployeeId = Guid.Parse("c3d4c221-c289-4100-8f8b-23e3ca578328"),
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"),
            DateOfBirth = DateTime.Now.AddYears(-35),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("'Email' must not be empty.", validate.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public async Task CreateEmployeeAsync_WithoutLastName()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "Kleber",
            LastName = "",
            Email = "kleber.ribeiro89@gmail.com",
            DocNumer = "32576251897",
            PositionEmployeeId = Guid.Parse("c3d4c221-c289-4100-8f8b-23e3ca578328"),
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"),
            DateOfBirth = DateTime.Now.AddYears(-35),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("'Last Name' must not be empty.", validate.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public async Task CreateEmployeeAsync_WithoutDocNumber()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "",
            LastName = "",
            Email = "",
            DocNumer = "",
            PositionEmployeeId = Guid.Parse("c3d4c221-c289-4100-8f8b-23e3ca578328"),
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"),
            DateOfBirth = DateTime.Now.AddYears(-35),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("'Doc Numer' must not be empty.", validate.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public async Task CreateEmployeeAsync_WithoutPosition()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "",
            LastName = "",
            Email = "",
            DocNumer = "",
            PositionEmployeeId = Guid.Empty,
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"),
            DateOfBirth = DateTime.Now.AddYears(-35),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("Position is required", validate.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public async Task CreateEmployeeAsync_Without17YearsOld()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "Kleber",
            LastName = "Ribeiro",
            Email = "kleber.ribeiro89@gmail.com",
            DocNumer = "32576251897",
            PositionEmployeeId = Guid.Empty,
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"),
            DateOfBirth = DateTime.Now.AddYears(-17),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("The employee must be at least 18 years old.", validate.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public async Task CreateEmployeeAsync_WithoutDateOfBirth()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "Kleber",
            LastName = "Ribeiro",
            Email = "kleber.ribeiro89@gmail.com",
            DocNumer = "32576251897",
            PositionEmployeeId = Guid.Empty,
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"),
            DateOfBirth = DateTime.MinValue,
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("A Date of birth is required.", validate.Errors.Select(x => x.ErrorMessage));
    }


    [Fact]
    public async Task CreateEmployeeAsync_DocExisting()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "Kleber",
            LastName = "Ribeiro",
            Email = "kleber.ribeiro89@gmail.com",
            DocNumer = "000000000",
            PositionEmployeeId = Guid.Empty,
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc8"),
            DateOfBirth = DateTime.Now.AddYears(-35),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("existing document.", validate.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public async Task CreateEmployeeAsync_LeaderCreatingDirector()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "Kleber",
            LastName = "Ribeiro",
            Email = "kleber.ribeiro89@gmail.com",
            DocNumer = "000000000",
            PositionEmployeeId = Guid.Parse("c3d4c221-c289-4100-8f8b-23e3ca578328"),
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc7"),
            DateOfBirth = DateTime.Now.AddYears(-35),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("your position does not allow you to create an employee of that position.", validate.Errors.Select(x => x.ErrorMessage));
    }

    [Fact]
    public async Task CreateEmployeeAsync_EmployeeCreatingDirector()
    {
        // Arrange
        var addEmployeeRequest = new AddEmployeeRequest
        {
            FirstName = "Kleber",
            LastName = "Ribeiro",
            Email = "kleber.ribeiro89@gmail.com",
            DocNumer = "000000000",
            PositionEmployeeId = Guid.Parse("c3d4c221-c289-4100-8f8b-23e3ca578328"),
            ManagerId = Guid.Parse("0dc478ff-dc5d-4e6a-9b9e-fbb96a359fc6"),
            DateOfBirth = DateTime.Now.AddYears(-35),
            Phones = new List<string> { "11983322703" }
        };

        var mockDbSetEmployee = CreateMockDbSet(qryEmployees);
        var mockDbSetPhone = CreateMockDbSet(new List<PhoneEmployee>().AsQueryable());

        var mockDbContext = new Mock<AppDbContext>(new DbContextOptions<AppDbContext>());
        mockDbContext.Setup(c => c.Employee).Returns(mockDbSetEmployee.Object);
        mockDbContext.Setup(c => c.PhoneEmployee).Returns(mockDbSetPhone.Object);



        // Act
        var validate = new AddEmployeeValidator(mockDbContext.Object).Validate(addEmployeeRequest);


        // Assert
        Assert.False(validate.IsValid);
        Assert.Contains("your position does not allow you to create an employee of that position.", validate.Errors.Select(x => x.ErrorMessage));
    }




    // Método auxiliar para criar um mock de DbSet
    private Mock<DbSet<T>> CreateMockDbSet<T>(IQueryable<T> data) where T : class
    {
        var mockSet = new Mock<DbSet<T>>();
        mockSet.As<IQueryable<T>>().Setup(m => m.Provider).Returns(data.Provider);
        mockSet.As<IQueryable<T>>().Setup(m => m.Expression).Returns(data.Expression);
        mockSet.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(data.ElementType);
        mockSet.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(data.GetEnumerator());
        return mockSet;
    }

}
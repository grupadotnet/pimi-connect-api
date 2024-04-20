using Microsoft.AspNetCore.Http;
using pimi_connect_api.Exceptions;
using pimi_connect_api.UnitTests.Base;

namespace pimi_connect_api.UnitTests.ControllersTests;

[Collection("TestsWhichUseDatabase_pimi-connect-test")]
public class UserControllerTests : ControllerUnitTestsBase<UserDto>
{
    private UserController _userController;
    private List<string> ExistingDomains;
    private string ExistingDomain;
    private string NotExistingDomain;
    private new Guid ExistingId { get; set; }

    private List<string> emailDomains = new List<string>
        {
            "gmail.com",
            "yahoo.com",
            "hotmail.com",
            "outlook.com",
            "example.com",
            "aol.com",
            "icloud.com",
            "protonmail.com",
            "mail.com",
            "live.com",
            "msn.com",
            "yandex.com",
            "zoho.com",
            "rocketmail.com",
            "gmx.com",
            "inbox.com",
            "me.com",
            "fastmail.com",
            "yahoo.co.uk",
            "ymail.com",
            "att.net",
            "cox.net",
            "comcast.net",
            "sbcglobal.net",
            "roadrunner.com",
            "verizon.net",
            "earthlink.net",
            "mac.com",
            "qq.com",
            "163.com",
            "126.com",
            "sina.com",
            "sohu.com",
            "hotmail.co.uk",
            "btinternet.com",
            "ntlworld.com",
            "blueyonder.co.uk",
            "talktalk.net",
            "virginmedia.com",
            // Add more domains as needed
        };

    #region Setup methods

    private void SetDomains()
    {
        SetExistingDomains();
        SetExistingDomain();
        SetNotExistingDomain();
    }

    private void SetExistingDomains()
    {
        
        var r = new Random();

        ExistingDomains = new List<string>();

        for (var i = 0; i < Settings.EntitiesCount; i++)
        {
            ExistingDomains.Add(emailDomains[r.Next(emailDomains.Count)]);
        }
    }

    private void SetExistingDomain()
    {
        var r = new Random();
        int randomNumber = r.Next(0, ExistingIds.Count);

        ExistingDomain = ExistingDomains[randomNumber];
        ExistingId = ExistingIds[randomNumber];
    }

    private void SetNotExistingDomain()
    {
        var r = new Random();
        var notExistingDomains = emailDomains.Where(d => !ExistingDomains.Contains(d)).ToList();
        var newDomain = notExistingDomains[r.Next(notExistingDomains.Count)];

        NotExistingDomain = newDomain;
    }

    #endregion

    private new async Task Arrange(bool fillDb = true)
    {
        await base.Arrange();
        SetDomains();

        if (fillDb)
        {
            //await Helper.FillAttachmentsTable(ExistingIds);
            await Helper.FillUsersTable(ExistingIds, ExistingDomains);
        }
        
        // initialize Controller
        var userService = new UserService(TestDbContext, Mapper);
        _userController = new UserController(userService);
    }
    
    [Theory]
    [InlineData(true)] // should successfully return user with the given id
    [InlineData(false)] // should return status 404
    public async void GetAsyncTest(bool idExists)
    {
        // Arrange
        await Arrange();
    
        // Act && Assert
        if (idExists)
        {
            var result = await _userController.GetAsync(ExistingId);
            var resultTuple = GetStatusAndContentFromResult(result);
            
            Assert.NotNull(resultTuple.Content);
            Assert.Equal(ExistingId, resultTuple.Content.Id);
            Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<NotFound404Exception>(() => 
                _userController.GetAsync(NotExistingId));
        }
    }
    
    [Theory]
    [InlineData(true)] // should successfully return the user with given email
    [InlineData(false)] // should return status 404
    public async void GetByEmailAsyncTest(bool emailExists)
    {
        // Arrange
        await Arrange();

        var email = emailExists ? 
            GenerateEmail(ExistingDomain,ExistingId) : 
            GenerateEmail(NotExistingDomain,NotExistingId);
        
        // Act && Assert
        if (emailExists)
        {
            var result = await _userController.GetByEmailAsync(email);
            var resultTuple = GetStatusAndContentFromResult(result);
            
            Assert.NotNull(resultTuple.Content);
            Assert.Equal(email, resultTuple.Content.Email);
            Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<NotFound404Exception>(() => 
                _userController.GetByEmailAsync(email));
        }
    }
    
    [Theory]
    [InlineData(true)] // should return all dtos from db
    [InlineData(false)] // should return an empty list
    public async void GetAllAsyncTest(bool hasUsersInDb)
    {
        // Arrange
        await Arrange(hasUsersInDb);

        // Act && Assert
        var result = await _userController.GetAllAsync();
        var resultTuple = GetStatusAndListFromResult(result);
        
        Assert.NotNull(resultTuple.List);
        
        if (hasUsersInDb)
        {
            Assert.Equal(Settings.EntitiesCount, resultTuple.List.Count);
        }
        else
        {
            Assert.Equal(new List<UserDto>(), resultTuple.List);
        }
    }
    
    [Theory]
    [InlineData(true)] // should return successfully updated UserDto
    [InlineData(false)] // should return status 404
    public async void UpdateAsyncTest(bool userExists)
    {
        // Arrange
        await Arrange();

        var newUserName = "updated";

        var userToUpdate = GenerateUserDto(Mapper, userExists ? ExistingDomain : NotExistingDomain ,userExists ? ExistingId : NotExistingId);
        userToUpdate.UserName = newUserName;

        // Act && Assert
        if (userExists)
        {
            var result = await _userController.UpdateAsync(userToUpdate);
            var resultTuple = GetStatusAndContentFromResult(result);
            
            var userInDb = await TestDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userToUpdate.Id);
            
            Assert.NotNull(resultTuple.Content);
            Assert.NotNull(userInDb);
            
            Assert.Equal(userToUpdate.UserName, resultTuple.Content.UserName);
            Assert.Equal(userToUpdate.UserName, userInDb.UserName);
            
            Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<NotFound404Exception>(() => 
                _userController.UpdateAsync(userToUpdate));
        }
        
    }
    
    [Theory]
    [InlineData(false, false)] // should successfully add user into the db
    [InlineData(true, false)] // should return status 400
    [InlineData(false , true)] // should return status 400
    [InlineData(true , true)] // should return status 400
    public async void AddAsyncTest(bool idAlreadyExists, bool emailAlreadyExists)
    {
        // Arrange
        await Arrange();

        var userToAdd = new UserDto()
        {
            Id = idAlreadyExists ? ExistingId : NotExistingId,
            UserName = GenerateUserName(),
            Email = GenerateEmail(emailAlreadyExists ? ExistingDomain : NotExistingDomain,emailAlreadyExists ? ExistingId : NotExistingId),
            Status = UserStatus.Accessible,
            ProfilePictureId = ExistingId
        };
        
        // Act && Assert
        if (!idAlreadyExists && !emailAlreadyExists)
        {
            var result = await _userController.AddAsync(userToAdd);
            var resultTuple = GetStatusAndContentFromResult(result);

            var userInDb = await TestDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == userToAdd.Id);
            
            Assert.NotNull(resultTuple.Content);
            Assert.NotNull(userInDb);
            
            Assert.Equal(userToAdd.UserName, resultTuple.Content.UserName);
            Assert.Equal(userToAdd.UserName, userInDb.UserName);
            
            Assert.Equal(userToAdd.Email, resultTuple.Content.Email);
            Assert.Equal(userToAdd.Email, userInDb.Email);
            
            Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<BadRequest400Exception>(() => 
                _userController.AddAsync(userToAdd));
        }
    }
    
    [Theory]
    [InlineData(true)] // should successfully delete user from the db and return status 204
    [InlineData(false)] // should return status 404
    public async void DeleteAsyncTest(bool userExists)
    {
        // Arrange
        await Arrange();

        // Act && Assert
        if (userExists)
        {
            ;
            var result = await _userController.DeleteAsync(ExistingId);
            var resultTuple = GetStatusAndContentFromResult(result);

            var userInDb = await TestDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == ExistingId);
            
            Assert.Equal(null, userInDb);
            Assert.Equal(StatusCodes.Status204NoContent, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<NotFound404Exception>(() => 
                _userController.DeleteAsync(NotExistingId));
        }
    }
}
using Microsoft.AspNetCore.Http;
using pimi_connect_api.Exceptions;
using pimi_connect_api.UnitTests.Base;
using pimi_connect_app.Data.Enums;
using pimi_connect_app.Data.Models;

namespace pimi_connect_api.UnitTests.ControllersTests;

public class UserControllerTests : ControllerUnitTestsBase<UserDto>
{
    private UserController _userController;
    
    private new async Task Arrange(bool fillDb = true)
    {
        await base.Arrange();

        if (fillDb)
        {
            await Helper.FillUsersTable(EntitiesCount);
        }
        
        // initialize Controller
        var userService = new UserService(TestDbContext, Mapper);
        _userController = new UserController(userService);
    }
    
    protected override UserDto CreateDto(int i)
    {
        return new UserDto()
        {
            UserName = GenerateUserName(i),
            Email = GenerateEmail(i),
            Status = UserStatus.Accessible,
            ProfilePictureKey = GenerateGuid()
        };
    }
    
    // [Theory]
    // [InlineData(false, false)] 
    // public async void GetAsyncTest()
    // {
    //     // Arrange
    //     await Arrange();
    //
    //     // Act && Assert
    //     
    //     
    // }
    
    [Theory]
    [InlineData(true)] 
    [InlineData(false)] 
    public async void GetByEmailAsyncTest(bool emailExists)
    {
        // Arrange
        await Arrange();

        var email = emailExists ? GenerateEmail(ExistingIndex) : GenerateEmail(NotExistingIndex);
        
        // Act && Assert
        if (emailExists)
        {
            var result = await _userController.GetByEmailAsync(email);
            var resultTuple = GetStatusAndContentFromResult(result);
            
            Assert.NotNull(resultTuple.Content);
            Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<NotFound404Exception>(() => _userController.GetByEmailAsync(email));
        }
    }
    
    [Theory]
    [InlineData(false, false)] 
    public async void GetAllAsyncTest()
    {
        // Arrange
        
        // Act && Assert
    }
    
    // [Theory]
    // [InlineData(false, false)] 
    // public async void UpdateAsyncTest()
    // {
    //     // Arrange
    //     
    //     // Act && Assert
    // }
    
    [Theory]
    [InlineData(false)]
    [InlineData(true)]
    public async void AddAsyncTest(bool emailAlreadyExists)
    {
        // Arrange
        await Arrange();

        var userToAdd = emailAlreadyExists ? CreateDto(ExistingIndex) : CreateDto(NotExistingIndex);
        
        // Act && Assert
        if (!emailAlreadyExists)
        {
            var result = await _userController.AddAsync(userToAdd);
            var resultTuple = GetStatusAndContentFromResult(result);

            Assert.NotNull(resultTuple.Content);

            var addedUser = await TestDbContext
                .Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == resultTuple.Content.Id);

            Assert.NotNull(addedUser);
            Assert.Equal(userToAdd.Email, addedUser.Email);
            Assert.Equal(StatusCodes.Status200OK, resultTuple.Status);
        }
        else
        {
            await Assert.ThrowsAsync<BadRequest400Exception>(() => _userController.AddAsync(userToAdd));
        }
    }
    
    // [Theory]
    // [InlineData(false, false)] 
    // public async void DeleteAsyncTest()
    // {
    //     // Arrange
    //     
    //     // Act && Assert
    // }
}
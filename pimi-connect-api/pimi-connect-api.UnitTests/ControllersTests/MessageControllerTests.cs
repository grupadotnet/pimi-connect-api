using pimi_connect_api.UnitTests.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pimi_connect_api.UnitTests.ControllersTests
{
    public class MessageControllerTests : ControllerUnitTestsBase<MessageDto>
    {
        private MessageController _messageController;

        private new async Task Arrange(bool fillDb = true)
        {
            await base.Arrange();

            if (fillDb)
            {
                await Helper.FillUsersTable(ExistingIds);
            }

            // initialize Controller
            var messageService = new MessageService(TestDbContext, Mapper);
            _messageController = new MessageController(messageService);
        }
    }
}

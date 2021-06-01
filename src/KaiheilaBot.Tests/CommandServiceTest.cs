using System.Collections.Generic;
using KaiheilaBot.Core.Models.Events;
using KaiheilaBot.Core.Models.Events.MessageRelatedEvents;
using KaiheilaBot.Core.Models.Objects;
using KaiheilaBot.Core.Models.Service;
using KaiheilaBot.Core.Services;
using Microsoft.Extensions.Logging;
using Xunit;
using Xunit.Abstractions;

namespace KaiheilaBot.Tests
{
    
    public class CommandServiceTest
    {
        private readonly CommandService _commandService;

        /*
         *      Command Permission
         *  test = Roles [123, 234] | Channel [123456789] | Users [987654321]
         *  test sub1 = Roles [12345]
         *
         *      Commands
         *  test sub1
         *  test sub2 sub21
         *  test sub2 sub22  -- return error
         */

        public CommandServiceTest(ITestOutputHelper output)
        {
            ILogger<CommandService> logger1 = output.BuildLoggerFor<CommandService>();
            
            _commandService = new CommandService(logger1);

            var testCommand = new CommandNode("test")
                .AddAllowedRoles(123)
                .AddAllowedRoles(234)
                .AddAllowedChannel("123456789")
                .AddAllowedUsers("987654321")
                .AddChildNode(
                    new CommandNode("sub1")
                        .AddAllowedRoles(12345)
                        .SetFunction((args, logger) =>
                        {
                            logger.LogInformation("RUN - test sub1");
                            logger.LogInformation("test sub1 ARGS - ");
                            foreach (var arg in args)
                            {
                                logger.LogInformation("\t" + arg);
                            }
                            logger.LogInformation("FIN");
                            return 0;
                        })
                )
                .AddChildNode(
                    new CommandNode("sub2")
                        .AddChildNode(new CommandNode("sub21")
                            .SetFunction((args, logger) =>
                            {
                                logger.LogInformation("RUN - test sub2 sub21");
                                logger.LogInformation("test sub1 ARGS - ");
                                foreach (var arg in args)
                                {
                                    logger.LogInformation("\t" + arg);
                                }
                                logger.LogInformation("FIN");
                                return 0;
                            })
                        )
                        .AddChildNode(new CommandNode("sub22")
                            .SetFunction((args, logger) =>
                            {
                                logger.LogInformation("RUN - test sub2 sub22");
                                logger.LogInformation("test sub1 ARGS - ");
                                foreach (var arg in args)
                                {
                                    logger.LogInformation("\t" + arg);
                                }
                                logger.LogInformation("FIN");
                                return 1;
                            })
                        )
                    );
            
            _commandService.AddCommand(testCommand);
        }

        private BaseMessageEvent<TextMessageEvent> GetMessage(
            string command, string channel, string author, IEnumerable<long> roles)
        {
            var e = new BaseMessageEvent<TextMessageEvent>
            {
                Data = new BaseMessageEventData<TextMessageEvent>
                {
                    Extra = new TextMessageEvent
                    {
                        Author = new User()
                    }
                }
            };

            e.Data.AuthorId = author;
            e.Data.Content = command;
            e.Data.TargetId = channel;
            e.Data.Extra.Author = new User()
            {
                Roles = roles
            };

            return e;
        }
        
        
        [Fact]
        public void Command_Parser_NoUserPermission()
        {
            var e =
                GetMessage("test sub1", 
                    "123456789", 
                    "123", 
                    new List<long>(1234));
            
            var status = _commandService.Parse(e);
            
            Assert.Equal(CommandParserStatus.NoPermission, status);
        }

        [Fact]
        public void Command_Parser_NoRolePermission()
        {
            var e =
                GetMessage("test sub1 arg1", 
                    "123456789", 
                    "1234", 
                    new List<long>(){1234});
            
            var status = _commandService.Parse(e);
            
            Assert.Equal(CommandParserStatus.NoPermission, status);
        }
        
        [Fact]
        public void Command_Parser_ChannelNotAllowed()
        {
            var e =
                GetMessage("test sub2 sub21",
                    "132",
                    "987654321",
                    new List<long>() {123});

            var status = _commandService.Parse(e);
            
            Assert.Equal(CommandParserStatus.ChannelNotAllowed, status);
        }

        [Fact]
        public void Command_Parser_CommandNotFound()
        {
            var e =
                GetMessage("test2",
                    "123456789",
                    "987654321",
                    new List<long>() {123});

            var status = _commandService.Parse(e);
            
            Assert.Equal(CommandParserStatus.NoMatchCommand, status);
        }
        
        [Fact]
        public void Command_Parser_ChildCommandNotFound()
        {
            var e =
                GetMessage("test sub2 sub33",
                    "123456789",
                    "987654321",
                    new List<long>() {123});

            var status = _commandService.Parse(e);
            
            Assert.Equal(CommandParserStatus.NoMatchCommand, status);
        }
        
        [Fact]
        public void Command_Parser_OnlyUserPermission()
        {
            var e =
                GetMessage("test sub2 sub21 arg1 arg2",
                    "123456789",
                    "987654321",
                    new List<long>() {215654});

            var status = _commandService.Parse(e);
            
            Assert.Equal(CommandParserStatus.Success, status);
        }

        [Fact]
        public void Command_Parser_OnlyRolePermission()
        {
            var e =
                GetMessage("test sub2 sub21 arg1 arg2",
                    "123456789",
                    "987654321",
                    new List<long>() {123});

            var status = _commandService.Parse(e);
            
            Assert.Equal(CommandParserStatus.Success, status);
        }
        
        [Fact]
        public void Command_Parser_BothPermission()
        {
            var e =
                GetMessage("test sub2 sub21",
                    "123456789",
                    "987654321",
                    new List<long>() {123});

            var status = _commandService.Parse(e);
            
            Assert.Equal(CommandParserStatus.Success, status);
        }
        
        [Fact]
        public void Command_Parser_FunctionError()
        {
            var e =
                GetMessage("test sub2 sub22",
                    "123456789",
                    "987654321",
                    new List<long>() {123});

            var status = _commandService.Parse(e);
            
            Assert.Equal(CommandParserStatus.FunctionError, status);
        }
    }
}
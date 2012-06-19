using Xunit;
using Xunit.Extensions;
using NSubstitute;
using Shouldly;

namespace nTools.nSoS
{
    public class ShowOrStartAppTest
    {
        readonly ShowOrStartApp SUT;
        readonly IInformationHerald MockHerald = Substitute.For<IInformationHerald>();
        
        public ShowOrStartAppTest()
        {
            SUT = new ShowOrStartApp(MockHerald);
        }

        [Fact]
        public void Run_CalledWithAQuestionMark_DisplaysHelpAndReturnsOK()
        {
            int retVal = SUT.Run(new[] { "?" });

            // assert
            MockHerald.Received().ShowHelp();
            retVal.ShouldBe(ShowOrStartApp.EXIT_OK);
        }

        [Theory,
        InlineData("-?"),
        InlineData("-h"),
        InlineData("-H"),
        InlineData("--help")]
        public void Run_CalledWithAValidHelpOption_DisplaysHelpAndReturnsOK(string option)
        {
            int retVal = SUT.Run(new [] { option });

            // assert
            MockHerald.Received().ShowHelp();
            retVal.ShouldBe(ShowOrStartApp.EXIT_OK);
        }
        
        [Theory,
        InlineData("-l"),
        InlineData("-L"),
        InlineData("--list")]
        public void Run_CalledWithAValidListOption_DisplaysWindowedProcessListAndReturnsOK(string option)
        {
            int retVal = SUT.Run(new [] { option });

            // assert
            MockHerald.Received().ShowWindowedProcessList();
            retVal.ShouldBe(ShowOrStartApp.EXIT_OK);
        }

        [Theory,
        InlineData("-v"),
        InlineData("-V"),
        InlineData("--version")]
        public void Run_CalledWithAValidVersionOption_DisplaysBannerAndReturnsOK(string option)
        {
            int retVal = SUT.Run(new [] { option });

            // assert
            MockHerald.Received().ShowBanner();
            retVal.ShouldBe(ShowOrStartApp.EXIT_OK);
        }

        [Fact]
        public void Run_CalledWithNoArgs_DisplaysErrorMessageAndReturnsErrorCode()
        {
            int retVal = SUT.Run(new string[0]);

            // assert
            MockHerald.Received().ShowHelp(Arg.Any<string>());
            retVal.ShouldNotBe(ShowOrStartApp.EXIT_OK);
        }

        [Fact]
        public void Run_CalledWithInvalidOption_DisplaysErrorMessageAndReturnsErrorCode()
        {
            int retVal = SUT.Run(new string[] { "--xyz" });

            // assert
            MockHerald.Received().ShowHelp(Arg.Is<string>(a => a.StartsWith("Unrecognized option")));
            retVal.ShouldNotBe(ShowOrStartApp.EXIT_OK);
        }

        [Fact]
        public void Run_CalledWithNameThatCannotBeFoundAndNoSpecifiedCommand_DisplaysHelpWithErrorMessageAndReturnsErrorCode()
        {
            int retVal = SUT.Run(new string[] { "!@#$%^&*()" });

            // assert
            MockHerald.Received().ShowHelp(Arg.Is<string>(a => a.Contains("was not found and no command was specified")));
            retVal.ShouldNotBe(ShowOrStartApp.EXIT_OK);
        }
    }
}

using System.Threading.Tasks;

namespace AndroidServer.Domain.Listeners.Commands
{
    /// <summary>
    /// Commands die alleen jij kan uitvoeren
    /// </summary>
    public class ZooiCommandListener : CommandContainerListener
    {
        public ZooiCommandListener(AndroidInstance android, ulong channelID) : base(android, channelID) { }

        [Command(CommandAccessLevel.Level4, "mail", "email", "e-mail")]
        public async Task SendEmail(CommandParameters parameters)
        {
            if (string.IsNullOrWhiteSpace(parameters.ContentWithoutTriggerAndCommand))
            {
                await parameters.Reply("no input...");
                return;
            }

            await parameters.Reply("sending...");
            AndroidService.Instance.Mail.SendEmail(
                "Android command",
                "Target",
                "mestiez@studiominus.nl",
                "From bot command system",
                parameters.ContentWithoutTriggerAndCommand);
            await parameters.Reply("...sent");
        }
    }
}

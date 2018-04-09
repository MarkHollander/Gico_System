namespace Gico.SystemCommands
{
    public class MenuChangeCommand : MenuAddCommand
    {
        public MenuChangeCommand()
        {
        }

        public MenuChangeCommand(int version) : base(version)
        {
        }

        public string Id { get; set; }

       
    }
}
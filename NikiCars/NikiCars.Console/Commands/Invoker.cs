using NikiCars.Console.Input;

namespace NikiCars.Console.Commands
{
    public class Invoker
    {
        private Parser parser;
        private CarCommands commands;

        public Invoker()
        {
            this.parser = new Parser();
            this.commands = new CarCommands();
        }

        public void AddCommand(string input)
        {
            
            this.commands.AddContext(this.parser.Parse(input));
            this.commands.AddCommand();
            
        }

        public void ExecuteAllCommands()
        {
            foreach (var comm in this.commands.commands)
            {
                comm.Execute();
            }
        }
    }
}

using System.Collections.Generic;
using Commands.Models;

namespace Commands.Data
{
    public class MockCommandsRepo : ICommandsRepo
    {
        public void CreateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public void DeleteCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<Command> GetAllCommands()
        {
            var commands = new List<Command>
            {
                new Command{Id=0, HowTo="Boil an egg.", Line="Boil water.", Platform="Pan."},
                new Command{Id=1, HowTo="Fry potatoes.", Line="Hot oil.", Platform="Big pan."},
                new Command{Id=2, HowTo="Bake a cake.", Line="Magic.", Platform="Oven."}
            };

            return commands;
        }

        public Command GetCommandById(int id)
        {
            return new Command{Id=0, HowTo="Boil an egg.", Line="Boil water.", Platform="Pan."};
        }

        public bool SaveChanges()
        {
            throw new System.NotImplementedException();
        }

        public void UpdateCommand(Command cmd)
        {
            throw new System.NotImplementedException();
        }
    }
}
using Commands.Models;
using Microsoft.EntityFrameworkCore;

namespace Commands.Data
{
    public class CommandsContext : DbContext
    {
        public CommandsContext(DbContextOptions<CommandsContext> opt) : base(opt)
        {
            
        }

        public DbSet<Command> Commands { get; set; }
    }
}
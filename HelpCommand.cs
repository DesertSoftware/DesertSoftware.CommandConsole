using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesertSoftware.CommandConsole
{
    public class HelpCommand : ConsoleCommand
    {
        public HelpCommand() {
            Description = "show help for a command";
            Usage = "help [<command>]";
        }

        public override void Execute(CommandSet context, string[] args) {
            if (args.Length < 2) {
                if (!string.IsNullOrEmpty(context.Description)) 
                    Console.WriteLine(context.Description);

                Console.WriteLine();
                Console.WriteLine("Commands:");
                context.WriteCommandDescriptions(Console.Out);
                Console.WriteLine("\nSee 'help <command>' for more information on a specific command"); 

                return;
            }

            if (context.ContainsKey(args[1].ToLower()))
                context[args[1].ToLower()].WriteOptionSet(Console.Out);
        }

        public override void Execute(string[] args) {
            throw new NotSupportedException();
        }
    }
}

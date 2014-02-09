using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesertSoftware.CommandConsole
{
    public class CommandShell
    {
        public virtual void WritePrompt() {
            Console.Write("\n$ ");
        }

        public virtual void OnNoSuchCommand(NoSuchCommandException noSuchCommand) {
            ConsoleColorManager.SetForegroundColor(ConsoleColor.DarkRed);
            Console.WriteLine("{0}: command not found.", noSuchCommand.CommandName);
            ConsoleColorManager.RestoreForegroundColor();
        }

        public virtual void OnError(Exception ex) {
            Console.WriteLine(ex.Message);
        }

        public void Run(CommandSet commands) {
            string[] args = { "" };

            do {
                try {
                    commands.Execute(args);
                } catch(NoSuchCommandException noSuchCommand) {
                    OnNoSuchCommand(noSuchCommand);
                } catch (Exception ex) {
                    OnError(ex);
                }

                if (!commands.TimeToQuit) {
                    string commandline;

                    WritePrompt();
                    commandline = Console.ReadLine();
                    args = CommandLineParser.ReadArguments(commandline);
//                    if (Console.IsInputRedirected)
//                        Console.Write("->{0}", commandline);
                }

            } while (!commands.TimeToQuit);
        }
    }
}

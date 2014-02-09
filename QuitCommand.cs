using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesertSoftware.CommandConsole
{
    public class QuitCommand : ConsoleCommand
    {
        public override void Execute(CommandSet context, string[] args) {
            context.TimeToQuit = true;
        }

        public override void Execute(string[] args) {
            throw new NotSupportedException();
        }
    }
}

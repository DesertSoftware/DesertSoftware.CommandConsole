using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace DesertSoftware.CommandConsole
{
    public class CommandSet : Dictionary<string, ConsoleCommand>
    {
        protected bool timeToQuit = false;

        public string Description { get; set; }
        public object Session { get; set; }

        public virtual bool TimeToQuit {
            get { return this.timeToQuit; }
            internal set { this.timeToQuit = value; }
        }

        public virtual void WriteCommandDescriptions(TextWriter writer) {
            foreach (var command in this.Keys) {
                var name = string.Format("  {0}", command).PadRight(29);

                writer.Write(name);
                if (name.EndsWith(" ")) {
                    writer.WriteLine("{0}", this[command].Description);
                    continue;
                }

                writer.WriteLine("\n{0}{1}", "".PadRight(29), this[command].Description);
            }
        }

        public virtual void Execute(string[] args) {

            // if no arguments presented, nothing to do
            if (args.Length == 0)
                return;

            if (args.Length == 1 && string.IsNullOrEmpty(args[0]))
                return;

            // if the command does not exist in this command set raise an exception
            if (!this.ContainsKey(args[0].ToLower()))
                throw new NoSuchCommandException() { CommandName = args[0] };

            // found command in this command set. execute it
            this[args[0].ToLower()].Execute(this, args);
        }
    }
}

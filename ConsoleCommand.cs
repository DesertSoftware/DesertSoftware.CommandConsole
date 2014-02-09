using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NDesk.Options;

namespace DesertSoftware.CommandConsole
{
    public abstract class ConsoleCommand
    {
        private bool showInHelp = true;
        protected OptionSet optionSet = null;

        public virtual string Description { get; set; }
        public virtual string Usage { get; set; }
        public virtual bool ShowInHelp { get { return this.showInHelp; } set { this.showInHelp = value; } }

        public virtual void Execute(string[] args) {
            if (this.optionSet != null)
                optionSet.Parse(args);
        }

        public virtual void Execute(CommandSet context, string[] args) {
            Execute(args);
        }

        public virtual void WriteUsage(TextWriter writer) {
            if (!string.IsNullOrEmpty(Usage))
                writer.WriteLine("usage: {0}", Usage);

            if (this.optionSet != null) {
                writer.WriteLine(); // ("Options:");
                this.optionSet.WriteOptionDescriptions(writer);
            }
        }

        public virtual void WriteOptionSet(TextWriter writer) {
            writer.WriteLine(Description);
            writer.WriteLine();

            if (!string.IsNullOrEmpty(Usage))
                writer.WriteLine("  Usage: {0}\n", Usage);

            if (this.optionSet != null) {
                writer.WriteLine("Options:");
                this.optionSet.WriteOptionDescriptions(writer);
            }
        }
    }
 }

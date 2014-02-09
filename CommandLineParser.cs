using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesertSoftware.CommandConsole
{
    internal class CommandLineParser
    {
        private string commandLine = "";
        private int offset = 0;

        static private CommandLineParser parser = new CommandLineParser("");

        static public string[] ReadArguments(string commandLine) {
            string arg = "";
            var args = new List<string>();

            parser.commandLine = commandLine;
            parser.offset = 0;

            while ((arg = parser.ReadArgument()) != null)
                args.Add(arg);

            return args.ToArray();
        }

        internal CommandLineParser(string commandLine) {
            this.commandLine = commandLine;
        }

        private void ScanWhile(char c) {
            while (this.offset < this.commandLine.Length && this.commandLine[this.offset] == c)
                this.offset++;
        }

        private void ScanToFirst(char c) {
            while (this.offset < this.commandLine.Length && this.commandLine[this.offset] != c)
                this.offset++;
        }

        private void ScanToFirstNonBlank() {
            ScanWhile(' ');
        }

        private void ScanToNextDoubleQuote() {
            ScanToFirst('"');
        }

        private void ScanToNextBlank() {
            ScanToFirst(' ');
        }

        private bool EndOfLine {
            get { return this.offset >= this.commandLine.Length; }
        }

        public string ReadArgument() {
            int start = this.offset;

            ScanToFirstNonBlank();

            if (EndOfLine)
                return null;

            if (this.commandLine[this.offset] == '"') {
                this.offset++;
                start = this.offset;
                ScanToNextDoubleQuote();
            } else {
                start = this.offset;
                ScanToNextBlank();
            }

            if (this.offset - start > 0) {
                return this.commandLine.Substring(start, this.offset++ - start);
            }

            return null;
        }
    }
}

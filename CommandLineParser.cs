//
//  Command Console Library
//    https://github.com/DesertSoftware/DesertSoftware.CommandConsole
//
//  Copyright (c) Desert Software Solutions, Inc. All rights reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//      http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//  limitations under the License.
//

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

        /// <summary>
        /// Reads the arguments.
        /// </summary>
        /// <param name="commandLine">The command line.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Reads the next argument.
        /// </summary>
        /// <returns>Returns a null value when no further arguments remain</returns>
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

/* 
//  Copyright Desert Software Solutions, Inc 2013
//  Command Console Library

// Copyright (c) 2013 Desert Software Solutions Inc. All rights reserved.

// THIS SOFTWARE IS PROVIDED BY THE AUTHOR ``AS IS'' AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, 
// BUT NOT LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY, NON-INFRINGEMENT AND FITNESS FOR A 
// PARTICULAR PURPOSE ARE DISCLAIMED.  

// IN NO EVENT SHALL THE AUTHOR BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY, OR 
// CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; 
// LOSS OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY, 
// WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT 
// OF THE USE OF THIS SOFTWARE, WHETHER OR NOT SUCH DAMAGES WERE FORESEEABLE AND EVEN IF THE AUTHOR IS ADVISED 
// OF THE POSSIBILITY OF SUCH DAMAGES. 
*/

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

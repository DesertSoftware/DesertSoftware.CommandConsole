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

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
using System.IO;
using System.Linq;
using System.Text;

namespace DesertSoftware.CommandConsole
{
    public class CommandSet : Dictionary<string, ConsoleCommand>
    {
        protected bool timeToQuit = false;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the session.
        /// </summary>
        /// <value>
        /// The session.
        /// </value>
        public object Session { get; set; }

        /// <summary>
        /// Gets a value indicating whether [time to quit].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [time to quit]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool TimeToQuit {
            get { return this.timeToQuit; }
            internal set { this.timeToQuit = value; }
        }

        /// <summary>
        /// Writes the command descriptions.
        /// </summary>
        /// <param name="writer">The writer.</param>
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

        /// <summary>
        /// Executes with the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="NoSuchCommandException"></exception>
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

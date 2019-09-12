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
using NDesk.Options;

namespace DesertSoftware.CommandConsole
{
    public abstract class ConsoleCommand
    {
        private bool showInHelp = true;
        protected OptionSet optionSet = null;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the usage description.
        /// </summary>
        /// <value>
        /// The usage.
        /// </value>
        public virtual string Usage { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether [show in help].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [show in help]; otherwise, <c>false</c>.
        /// </value>
        public virtual bool ShowInHelp { get { return this.showInHelp; } set { this.showInHelp = value; } }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        public virtual void Execute(string[] args) {
            if (this.optionSet != null)
                optionSet.Parse(args);
        }

        /// <summary>
        /// Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="args">The arguments.</param>
        public virtual void Execute(CommandSet context, string[] args) {
            Execute(args);
        }

        /// <summary>
        /// Writes the usage.
        /// </summary>
        /// <param name="writer">The writer.</param>
        public virtual void WriteUsage(TextWriter writer) {
            if (!string.IsNullOrEmpty(Usage))
                writer.WriteLine("usage: {0}", Usage);

            if (this.optionSet != null) {
                writer.WriteLine(); // ("Options:");
                this.optionSet.WriteOptionDescriptions(writer);
            }
        }

        /// <summary>
        /// Writes the option set.
        /// </summary>
        /// <param name="writer">The writer.</param>
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

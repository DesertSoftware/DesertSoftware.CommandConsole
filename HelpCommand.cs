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
    public class HelpCommand : ConsoleCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="HelpCommand"/> class.
        /// </summary>
        public HelpCommand() {
            Description = "show help for a command";
            Usage = "help [<command>]";
        }

        /// <summary>
        /// Executes the specified context.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="args">The arguments.</param>
        public override void Execute(CommandSet context, string[] args) {
            if (args.Length < 2) {
                if (!string.IsNullOrEmpty(context.Description)) 
                    Console.WriteLine(context.Description);

                Console.WriteLine();
                Console.WriteLine("Commands:");
                context.WriteCommandDescriptions(Console.Out);
                Console.WriteLine("\nSee 'help <command>' for more information on a specific command"); 

                return;
            }

            if (context.ContainsKey(args[1].ToLower()))
                context[args[1].ToLower()].WriteOptionSet(Console.Out);
        }

        /// <summary>
        /// Executes the specified arguments.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <exception cref="NotSupportedException"></exception>
        public override void Execute(string[] args) {
            throw new NotSupportedException();
        }
    }
}

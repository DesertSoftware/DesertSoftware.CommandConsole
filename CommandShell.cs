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
    public class CommandShell
    {
        /// <summary>
        /// Writes the prompt.
        /// </summary>
        public virtual void WritePrompt() {
            Console.Write("\n$ ");
        }

        /// <summary>
        /// Called when [no such command].
        /// </summary>
        /// <param name="noSuchCommand">The no such command.</param>
        public virtual void OnNoSuchCommand(NoSuchCommandException noSuchCommand) {
            ConsoleColorManager.SetForegroundColor(ConsoleColor.DarkRed);
            Console.WriteLine("{0}: command not found.", noSuchCommand.CommandName);
            ConsoleColorManager.RestoreForegroundColor();
        }

        /// <summary>
        /// Called when [error].
        /// </summary>
        /// <param name="ex">The error exception.</param>
        public virtual void OnError(Exception ex) {
            ConsoleColorManager.SetForegroundColor(ConsoleColor.DarkRed);

            // show all of the exception messages including all internal exceptions
            try {
                while (ex != null) {
                    Console.WriteLine(ex.Message);
                    ex = ex.InnerException;
                }
            } finally {
                ConsoleColorManager.RestoreForegroundColor();
            }
        }

        /// <summary>
        /// Runs the specified commands.
        /// </summary>
        /// <param name="commands">The commands.</param>
        public void Run(CommandSet commands) {
            string[] args = { "" };

            do {
                try {
                    commands.Execute(args);
                } catch(NoSuchCommandException noSuchCommand) {
                    OnNoSuchCommand(noSuchCommand);
                } catch (Exception ex) {
                    OnError(ex);
                }

                if (!commands.TimeToQuit) {
                    string commandline;

                    WritePrompt();
                    commandline = Console.ReadLine();
                    args = CommandLineParser.ReadArguments(commandline);
//                    if (Console.IsInputRedirected)
//                        Console.Write("->{0}", commandline);
                }

            } while (!commands.TimeToQuit);
        }
    }
}

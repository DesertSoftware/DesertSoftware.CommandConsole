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
    public class CommandShell
    {
        public virtual void WritePrompt() {
            Console.Write("\n$ ");
        }

        public virtual void OnNoSuchCommand(NoSuchCommandException noSuchCommand) {
            ConsoleColorManager.SetForegroundColor(ConsoleColor.DarkRed);
            Console.WriteLine("{0}: command not found.", noSuchCommand.CommandName);
            ConsoleColorManager.RestoreForegroundColor();
        }

        public virtual void OnError(Exception ex) {
            Console.WriteLine(ex.Message);
        }

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

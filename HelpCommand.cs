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
    public class HelpCommand : ConsoleCommand
    {
        public HelpCommand() {
            Description = "show help for a command";
            Usage = "help [<command>]";
        }

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

        public override void Execute(string[] args) {
            throw new NotSupportedException();
        }
    }
}

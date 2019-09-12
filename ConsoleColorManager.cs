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
    public static class ConsoleColorManager
    {
        static Stack<ConsoleColor> foregroundColorStack = new Stack<ConsoleColor>();

        /// <summary>
        /// Sets the color of the foreground.
        /// </summary>
        /// <param name="color">The color.</param>
        static public void SetForegroundColor(ConsoleColor color) {
            foregroundColorStack.Push(Console.ForegroundColor);
            Console.ForegroundColor = color;
        }

        /// <summary>
        /// Restores the color of the foreground.
        /// </summary>
        static public void RestoreForegroundColor() {
            if (foregroundColorStack.Count > 0)
                Console.ForegroundColor = foregroundColorStack.Pop();
        }
    }
}

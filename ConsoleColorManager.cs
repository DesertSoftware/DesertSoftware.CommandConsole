using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesertSoftware.CommandConsole
{
    public static class ConsoleColorManager
    {
        static Stack<ConsoleColor> foregroundColorStack = new Stack<ConsoleColor>();

        static public void SetForegroundColor(ConsoleColor color) {
            foregroundColorStack.Push(Console.ForegroundColor);
            Console.ForegroundColor = color;
        }

        static public void RestoreForegroundColor() {
            if (foregroundColorStack.Count > 0)
                Console.ForegroundColor = foregroundColorStack.Pop();
        }
    }
}

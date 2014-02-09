using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesertSoftware.CommandConsole
{
    public class NoSuchCommandException : Exception
    {
        public string CommandName { get; internal set; }
    }
}

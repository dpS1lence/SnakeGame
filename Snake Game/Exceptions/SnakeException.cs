using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Exceptions
{
    public class SnakeException : Exception
    {
        public SnakeException() { }

        public SnakeException(string message)
            : base(message) { }
    }
}
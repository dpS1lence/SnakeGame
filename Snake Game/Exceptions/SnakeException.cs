using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snake_Game.Exceptions
{
    public class SnakeRangeException : Exception
    {
        public SnakeRangeException() { }

        public SnakeRangeException(string message)
            : base(message) { }
    }
}
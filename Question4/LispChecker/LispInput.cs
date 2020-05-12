using System;
using System.Collections.Generic;
using System.Text;

namespace LispChecker
{
    class LispInput
    {
        private string source;
        private int index;
        private int length;
        public LispInput(string source)
        {
            this.source = source;
            index = 0;
            length = source.Length;
        }

        public bool EndOfString()
        {
            return (index < length) ? false : true;
        }

        public char ReadChar()
        {
            return source[index++];
        }
    }
}

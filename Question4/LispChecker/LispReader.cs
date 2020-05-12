using System;
using System.Collections.Generic;
using System.Text;

namespace LispChecker
{
    class LispReader
    {
        int listLevel;
        LispInput lispInput;

        public LispReader(string inputString)
        {
            listLevel = 0;
            lispInput = new LispInput(inputString);
        }

        public bool CheckParentheses()
        {
            char nextChar;
            bool valid = true;
            while (!lispInput.EndOfString())
            {
                nextChar = lispInput.ReadChar();
                switch (nextChar)
                {
                    case '(':
                        listLevel++;
                        break;
                    case ')':
                        listLevel--;
                        break;
                    case '"':
                        valid &= ReadString();
                        break;
                    case ';':
                        valid &= ReadComment();
                        break;
                    default:
                        break;
                }
                // immediate exit if we drop below 0
                if (listLevel < 0)
                {
                    break;
                }
            }

            if (listLevel != 0)
            {
                valid = false;
            }
            return valid;
        }

        public bool ReadString()
        {
            char nextChar;
            bool valid = false;

            // read until closing ", checking for \'s
            while (!lispInput.EndOfString())
            {
                nextChar = lispInput.ReadChar();

                // escape situation: eat it and continue, or break with syntax==true
                if (nextChar == '\\')
                {
                    if (!lispInput.EndOfString())
                    {
                        nextChar = lispInput.ReadChar();
                        continue;
                    }
                    else
                    {
                        break;
                    }
                }

                if (nextChar == '"')
                {
                    valid = true;
                    break;
                }
            }
            return valid;
        }

        public bool ReadComment()
        {
            char nextChar;

            // read until end of string or a \n, if any
            while (!lispInput.EndOfString())
            {
                nextChar = lispInput.ReadChar();
                if (nextChar == '\n')
                {
                    break;
                }
            }
            return true;
        }
    }
}

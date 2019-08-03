using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CsvParser.ExceptionHandler
{
    internal class DuplicateColumnException : Exception
    {
        internal DuplicateColumnException()
        {

        }
        internal DuplicateColumnException(string message) : base(message)
        {

        }
        internal DuplicateColumnException(string message,Exception inner) : base(message,inner)
        {

        }

    }
    internal class IncorrectFilePathException : Exception
    {
        internal IncorrectFilePathException()
        {

        }
        internal IncorrectFilePathException(string message) : base(message)
        {

        }
        internal IncorrectFilePathException(string message, Exception inner) : base(message, inner)
        {

        }

    }
    internal class IncompatibleFileDataException : Exception
    {
        internal IncompatibleFileDataException()
        {

        }
        internal IncompatibleFileDataException(string message) : base(message)
        {

        }
        internal IncompatibleFileDataException(string message, Exception inner) : base(message, inner)
        {

        }

    }
}

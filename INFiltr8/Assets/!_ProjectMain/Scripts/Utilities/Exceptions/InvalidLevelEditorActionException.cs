using System;

namespace __ProjectMain.Scripts.Utilities.Exceptions
{
    public class InvalidLevelEditorActionException : Exception
    {
        public InvalidLevelEditorActionException(string message) : base(message) { }
        public InvalidLevelEditorActionException(string message, Exception inner) : base(message, inner) { }
    }
}
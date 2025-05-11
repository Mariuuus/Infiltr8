using System;

namespace __ProjectMain.Scripts.Utilities.Exceptions
{
    public class InvalidLevelEditorException : Exception
    {
        public InvalidLevelEditorException(string message) : base(message) { }
        public InvalidLevelEditorException(string message, Exception inner) : base(message, inner) { }
    }
}
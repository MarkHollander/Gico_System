using System;

namespace Gico.ExceptionDefine
{
    public class MessageException : Exception
    {
        public MessageException(string resourceName)
        {
            ResourceName = resourceName;
        }

        public string ResourceName { get; private set; }
    }
}

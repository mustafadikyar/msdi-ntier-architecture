using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Exceptions.Abstract
{
    public interface IResult
    {
        bool Success { get; }
        string Message { get; }
    }
}

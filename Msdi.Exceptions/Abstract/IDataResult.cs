using System;
using System.Collections.Generic;
using System.Text;

namespace Msdi.Exceptions.Abstract
{
    public interface IDataResult<out T> : IResult
    {
        T Data { get; }
    }
}

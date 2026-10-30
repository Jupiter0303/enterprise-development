using System;
using System.Collections.Generic;
using System.Text;

namespace Museum.Application.Contracts.Common;

public interface IHasId
{
    int Id { get; }
}
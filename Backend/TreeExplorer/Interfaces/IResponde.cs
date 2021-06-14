using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TreeExplorer.Interfaces
{
    interface IResponde
    {
        string Message { get; set; }
        bool Error { get; set; }
    }
}

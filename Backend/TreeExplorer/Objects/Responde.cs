using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TreeExplorer.Interfaces;

namespace TreeExplorer.Objects
{
    public class Responde : IResponde
    {
        public string Message { get; set; }
        public bool Error { get; set; }
    }
}

using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TreeExplorer.Models;

namespace TreeExplorer.Interfaces
{
    interface ITree
    {
        static bool Add(int id, string name, string type, int idW) => throw new NotImplementedException();
        bool Edit(int id);
        bool Delete(int id);
        bool Move(int id, int idW);
        List<Element> Sort(int idW, string type);
        List<Element> Show();
    }
}

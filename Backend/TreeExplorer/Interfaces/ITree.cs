using System;
using System.Collections.Generic;
using TreeExplorer.Models;

namespace TreeExplorer.Interfaces
{
    interface ITree
    {
        virtual bool Add(int id, string name, string type, int idW) => throw new NotImplementedException();
        virtual bool Edit(Element element) => throw new NotImplementedException();
        virtual bool Delete(int id) => throw new NotImplementedException();
        virtual bool Move(int id, int idW) => throw new NotImplementedException();
        virtual IEnumerable<Element> Sort(int idW, string type) => throw new NotImplementedException();
        List<Element> Set();

        virtual List<Element> Show() => throw new NotImplementedException();
    }
}

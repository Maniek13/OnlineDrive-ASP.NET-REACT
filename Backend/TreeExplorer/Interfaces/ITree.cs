using System;
using System.Collections.Generic;
using TreeExplorer.Models;

namespace TreeExplorer.Interfaces
{
    interface ITree
    {
        virtual IList<Element> Branch(int id) => throw new NotImplementedException();
        virtual IResponde Add(int id, string name, string type, int idW) => throw new NotImplementedException();
        virtual IResponde Edit(Element element) => throw new NotImplementedException();
        virtual IResponde Delete(int id) => throw new NotImplementedException();
        virtual IResponde Move(int id, int idW) => throw new NotImplementedException();
        virtual IEnumerable<Element> Sort(int idW, string type) => throw new NotImplementedException();
        virtual List<Element> Set() => throw new NotImplementedException();
        virtual List<Element> Show() => throw new NotImplementedException();
    }
}

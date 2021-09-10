using System;
using System.Collections.Generic;
using TreeExplorer.Models;

#nullable enable

namespace TreeExplorer.Interfaces
{
    interface ITree
    {
        virtual IList<Element> Branch(int id) => throw new NotImplementedException();
        virtual IResponde Add(int id, string name, string type, int idW, int usserId) => throw new NotImplementedException();
        virtual IResponde Edit(Element element) => throw new NotImplementedException();
        virtual IResponde Delete(int id) => throw new NotImplementedException();
        virtual IResponde Move(int id, int idW) => throw new NotImplementedException();
        virtual IEnumerable<Element> Sort(int idW, string type, int usserId) => throw new NotImplementedException();
        virtual void Set() => throw new NotImplementedException();
        virtual List<Element> Get() => throw new NotImplementedException();
        virtual List<Element>? Get(int userId) => throw new NotImplementedException();
        virtual string FindPath(int idW) => throw new NotImplementedException();
    }
}

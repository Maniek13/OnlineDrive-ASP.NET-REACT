using System;
using System.Collections.Generic;
using TreeExplorer.Models;
using TreeExplorer.Objects;

namespace TreeExplorer.VirtualClasses
{
    public class VirtulTree
    {
        public HashSet<Element> _list;

        public virtual HashSet<Element> Branch(int id) => throw new NotImplementedException();
        public virtual Responde Add(int id, string name, string type, int idW, int usserId) => throw new NotImplementedException();
        public virtual Responde Edit(Element element) => throw new NotImplementedException();
        public virtual Responde Delete(int id) => throw new NotImplementedException();
        public virtual Responde Move(int id, int idW) => throw new NotImplementedException();
        public virtual IEnumerable<Element> Sort(int idW, string type, int usserId) => throw new NotImplementedException();
        public virtual void Set() => throw new NotImplementedException();
        public virtual void Set(List<Element> list) => throw new NotImplementedException();
        public virtual HashSet<Element> Get() => throw new NotImplementedException();
        public virtual HashSet<Element> Get(int userId) => throw new NotImplementedException();
        public virtual List<string> FindPath(int idW) => throw new NotImplementedException();
        public virtual HashSet<Element> Folder(int idW, string type) => throw new NotImplementedException();
    }
}

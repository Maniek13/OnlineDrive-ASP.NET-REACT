using System;
using System.Collections.Generic;
using TreeExplorer.Models;
using TreeExplorer.Objects;

namespace TreeExplorer.Interfaces
{
    interface ITree
    {
        public HashSet<Element> Branch(int id);
        public Responde Add(int id, string name, string type, int idW, int usserId);
        public Responde Edit(Element element);
        public Responde Delete(int id);
        public Responde Move(int id, int idW);
        public IEnumerable<Element> Sort(int idW, string type, int usserId);
        public void Set(List<Element> list);
        public HashSet<Element> Get();
        public HashSet<Element> Get(int userId);
        public List<string> FindPath(int idW);
        public HashSet<Element> Folder(int idW, string type);
    }
}

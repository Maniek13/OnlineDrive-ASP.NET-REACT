using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TreeExplorer.Interfaces;
using TreeExplorer.Models;
using TreeExplorer.VirtualClasses;

namespace TreeExplorer.Classes
{
    public class Tree : BasseTree, ITree
    {
        public void Set(List<Element> list)
        {
            IEnumerable<Element> query = from el in list
                                         orderby el.Type descending
                                         select el;

            _list = query.ToHashSet();
        }

        public HashSet<Element> Get()
        {
            IEnumerable<Element> query = from el in _list
                                         orderby el.Type descending
                                         select el;

            return query.ToHashSet();
        }

        public HashSet<Element> Get(int usserId)
        {
            HashSet<Element> list = new();
            try
            {
                list = _list.Where(el => el.UsserId == usserId).ToHashSet();

                IEnumerable<Element> query = from el in list
                                             orderby el.Type descending
                                             select el;
                list = query.ToHashSet();
            }
            catch
            {
                list = null;
            }

            return list;
        }

        public Responde Add(int id, string name, string type, int idW, int usserId)
        {
            Responde responde = new();
            if (_list is null)
            {
                responde.Message = "No list (Server error)";
                responde.Error = true;
            }
            else
            {
                Element element = new() { Id = id, Name = name, Type = type, IdW = idW, UsserId = usserId };

                HashSet<Element> els = Folder(idW, type);

                if (els.FirstOrDefault(el => el.Name == name && el.IdW == idW && el.UsserId == usserId) == null)
                {
                    _list.Add(element);
                    responde.Message = "Ok";
                    responde.Error = false;
                }
                else
                {
                    responde.Message = "Object alredy exist";
                    responde.Error = true;
                }
            }

            return responde;
        }

        public HashSet<Element> Branch(int id)
        {
            IEnumerable<Element> query = from el in _list
                                         where el.Id == id
                                         select el;

            List<Element> branch = new();
            branch.Add(query.First());

            HashSet<Element> list = new();


            foreach (Element el in _list)
            {
                list.Add(el);
            }

            list.Remove(query.First());

            bool end = false;

            while (end == false)
            {
                int next = 0;

                int i = 0;
                List<Element> temp = new();

                foreach (Element elem in list)
                {
                    List<Element> branchEl = new();
                    branch.ForEach(el =>
                    {
                        if (list.ElementAt(i).IdW == el.Id)
                        {
                            branchEl.Add(list.ElementAt(i));
                            next++;
                        }
                    });
                    i++;

                    branchEl.ForEach(element =>
                    {
                        temp.Add(element);
                        branch.Add(element);
                    });
                }

                temp.ForEach(e =>
                {
                    list.Remove(e);
                });


                if (next == 0)
                {
                    end = true;
                }
            }

            return branch.ToHashSet();
        }

        public HashSet<Element> Folder(int idW, string type)
        {
            IEnumerable<Element> query = from el in _list
                                         where el.IdW == idW && el.Type == type
                                         select el;

            return query.ToHashSet<Element>();
        }

        public List<string> FindPath(int idW)
        {
            List<string> path = new();
            bool stop = false;

            while (stop == false)
            {

                Element el = _list.FirstOrDefault(el => el.Id == idW);
                if (el != null)
                {
                    path.Add(el.Name);
                    idW = el.IdW;

                    if (idW == 0)
                    {
                        stop = true;
                    }
                }
                else
                {
                    stop = true;
                }
            }
            path.Reverse();
            return path;
        }

        public Responde Edit(Element element)
        {
            Responde responde = new();

            if (_list is null)
            {
                responde.Message = "No list (Server error)";
                responde.Error = true;
            }
            else
            {
                Element toChange = _list.FirstOrDefault(el => el.Id == element.Id);

                HashSet<Element> els = Folder(element.IdW, element.Type);

                if (els.FirstOrDefault(el => el.Name == element.Name) == null)
                {
                    toChange.Name = element.Name;
                    toChange.Type = element.Type;
                    toChange.IdW = element.IdW;

                    responde.Message = "Ok";
                    responde.Error = false;
                }
                else
                {
                    responde.Message = "Element alredy exist";
                    responde.Error = true;
                }
            }
            return responde;
        }
        public Responde Delete(int id)
        {
            Responde responde = new();
            if (_list is null)
            {
                responde.Message = "No list (Server error)";
                responde.Error = true;
            }
            else
            {
                try
                {
                    HashSet<Element> toDel = Branch(id);

                    foreach (Element el in toDel)
                    {
                        _list.Remove(el);
                    }

                    responde.Message = JsonSerializer.Serialize(toDel);
                    responde.Error = false;
                }
                catch
                {
                    Console.WriteLine("Remove err");

                    responde.Message = "Remove err";
                    responde.Error = true;
                }
            }
            return responde;
        }
        public Responde Move(int id, int idW)
        {
            Responde responde = new();
            if (_list is null)
            {
                responde.Message = "No list (Server error)";
                responde.Error = true;
            }
            else
            {
                Element toChange = _list.FirstOrDefault(el => el.Id == id);

                HashSet<Element> branch = Branch(id);

                bool ok = branch.FirstOrDefault(el => el != toChange && el.IdW == toChange.Id) == null;

                if (ok == true)
                {
                    HashSet<Element> els = Folder(idW, toChange.Type);

                    if (els.FirstOrDefault(el => el.Name == toChange.Name) == null)
                    {
                        toChange.IdW = idW;
                        responde.Message = "Ok";
                        responde.Error = false;
                    }
                    else
                    {
                        responde.Message = "Element alredy exist in this folder";
                        responde.Error = true;
                    }
                }
                else
                {
                    responde.Message = "New node is in branch";
                    responde.Error = true;
                }
            }
            return responde;
        }
        public IEnumerable<Element> Sort(int idW, string type, int usserId)
        {
            IEnumerable<Element> query = Array.Empty<Element>();
            switch (type)
            {
                case "ASC":
                    query = from el in _list
                            where el.IdW == idW && el.UsserId == usserId
                            orderby el.Name ascending
                            select el;
                    break;
                case "DESC":
                    query = from el in _list
                            where el.IdW == idW && el.UsserId == usserId
                            orderby el.Name descending
                            select el;
                    break;
            }

            IEnumerable<Element> list;

            list = from el in _list
                   where el.IdW != idW && el.UsserId == usserId
                   orderby el.Type descending
                   select el;

            query = from el in query
                    orderby el.Type descending
                    select el;

            list = list.Concat(query);

            return list;
        }
    }
}
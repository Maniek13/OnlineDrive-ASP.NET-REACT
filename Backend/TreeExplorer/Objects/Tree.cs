using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using TreeExplorer.Interfaces;
using TreeExplorer.Models;

namespace TreeExplorer.Objects
{
    public class Tree : ITree
    {

        private static List<Element> _list;

        public static void Set(List<Element> list)
        {
            IEnumerable<Element> query = from el in list
                                         orderby el.Type descending
                                         select el;

            _list = query.ToList();
        }

        public static List<Element> Get()
        {
            return _list;
        }

        public static List<Element>? Get(int usserId)
        {
            List<Element> list = new();
            try
            {
                list =  _list.Where(el => el.UsserId == usserId).ToList();
            }
            catch
            {
                list = null;
            }

            return list;
        }

        public static Responde Add(int id, string name, string type, int idW, int userId)
        {
            Responde responde = new();
            if (_list is null)
            {
                responde.Message = "No list (Server error)";
                responde.Error = true;
            }
            else
            {
                Element element = new() { Id = id, Name = name, Type = type, IdW = idW, UsserId = userId };

                List<Element> els = Folder(idW, type);

                if(els.Find(el => el.Name == name && el.IdW == idW) == null)
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

        public static List<Element> Branch(int id)
        {
            IEnumerable<Element> query = from el in _list
                                         where el.Id == id
                                         select el;

            List<Element> branch = new();
            branch.Add(query.First());

            List<Element> list = new();
            _list.ForEach(el =>
            {
                list.Add(el);
            });
            list.Remove(query.First());

            bool end = false;

            while (end == false)
            {
                int next = 0;

                int i = 0;
                List<Element> temp = new();
                list.ForEach(elem =>
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
                });

                temp.ForEach(e =>
                {
                    list.Remove(e);
                });


                if (next == 0)
                {
                    end = true;
                }
            }

            return branch;
        }

        private static List<Element> Folder(int idW, string type)
        {
            IEnumerable<Element> query = from el in _list
                                         where el.IdW == idW && el.Type == type
                                         select el;

            return query.ToList<Element>();
        }

        public static Responde Edit(Element element) 
        {
            Responde responde = new();
            if (_list is null)
            {
                responde.Message = "No list (Server error)";
                responde.Error = true;
            }
            else
            {
                Element toChange = _list.Find(el => el.Id == element.Id);

                List<Element> els = Folder(element.IdW, element.Type);

                if (els.Find(el => el.Name == element.Name) == null)
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
        public static Responde Delete(int id)
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
                    List<Element> toDel = Branch(id);

                    toDel.ForEach(el =>
                    {
                        _list.Remove(el);
                    });


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
        public static Responde Move(int id, int idW)
        {
            Responde responde = new();
            if (_list is null)
            {
                responde.Message = "No list (Server error)";
                responde.Error = true;
            }
            else
            {
               

                Element toChange = _list.Find(el => el.Id == id);

                List<Element> branch = Branch(id);


                bool ok = true;
                branch.ForEach(el =>
                {
                    if (el != toChange)
                    {
                        if (el.IdW == toChange.Id)
                        {
                            ok = false;
                        }
                    }
                });

                if(ok == true)
                {
                    List<Element> els = Folder(idW, toChange.Type);

                    if (els.Find(el => el.Name == toChange.Name) == null)
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
        public static IEnumerable<Element> Sort(int idW, string type, int usserId)
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

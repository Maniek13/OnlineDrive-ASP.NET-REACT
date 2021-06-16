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

        public Tree(List<Element> list)
        {
            IEnumerable<Element> query = from el in list
                                         orderby el.Type descending
                                         select el;

            _list = query.ToList();
        }

        public List<Element> Set()
        {
            return _list;
        }

        public static List<Element> Show()
        {
            return _list;
        }

        public static Responde Add(int id, string name, string type, int idW)
        {
            Responde responde = new();
            if (_list is null)
            {
                responde.Message = "No list (Server error)";
                responde.Error = true;
            }
            else
            {
                Element element = new() { Id = id, Name = name, Type = type, IdW = idW };
                _list.Add(element);
                responde.Message = "Ok";
                responde.Error = false;
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
                toChange.Name = element.Name;
                toChange.Type = element.Type;
                toChange.IdW = element.IdW;

                responde.Message = "Ok";
                responde.Error = false;
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
                    toChange.IdW = idW;
                    responde.Message = "Ok";
                    responde.Error = false;
                }
                else
                {
                    responde.Message = "New node is in branch";
                    responde.Error = true;
                }

               
            }
            return responde;
        }
        public static IEnumerable<Element> Sort(int idW, string type)
        {
            IEnumerable<Element> query = Array.Empty<Element>();
            switch (type)
            {
                case "ASC":
                    query = from el in _list
                                where el.IdW == idW
                                orderby el.Name ascending
                                select el;
                    break;
                case "DESC":
                    query = from el in _list
                                where el.IdW == idW
                                orderby el.Name descending
                                select el;
                    break;
             
            }


            IEnumerable<Element> list;

            list = from el in _list
                   where el.IdW != idW
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

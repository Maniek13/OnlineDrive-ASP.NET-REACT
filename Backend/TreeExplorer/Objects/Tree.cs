using System;
using System.Collections.Generic;
using System.Linq;
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
                IEnumerable<Element> query = from el in _list
                                             where el.Id == id
                                             select el;

                try
                {
                    _list.Remove(query.First());
                }
                catch
                {
                    Console.WriteLine("Remove err");

                    responde.Message = "Remove err";
                    responde.Error = true;
                }

                responde.Message = "Ok";
                responde.Error = false;
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
                toChange.IdW = idW;
                responde.Message = "Ok";
                responde.Error = false;
            }
            return responde;
        }
        public static IEnumerable<Element> Sort(int idW, string type)
        {
            IEnumerable<Element> query;
            switch (type)
            {
                case "ASC":
                    query = from el in _list
                                where el.IdW == idW
                                orderby el.Name ascending
                                select el;
                    return query;
                case "DESC":
                    query = from el in _list
                                where el.IdW == idW
                                orderby el.Name descending
                                select el;
                    return query;
            }

            return Array.Empty<Element>();
        }


    }
}

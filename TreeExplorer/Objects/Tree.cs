using Newtonsoft.Json;
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
            _list = list;
        }

        public List<Element> Show()
        {
            return _list;
        }

        public static bool Add(int id, string name, string type, int idW)
        {
            if(_list is null)
                return false;
            else
            {
                Element element = new() { Id = id, Name = name, Type = type, IdW = idW };
                _list.Add(element);
                return true;
            }
           
        }

        public static bool Edit(int id) {
            return false;
        }
        public static bool Delete(int id)
        {
            if (_list is null)
                return false;
            else
            {
                IEnumerable<Element> query = from el in _list
                                             where el.Id == id
                                             select el;

                try
                {
                    _list.Remove(query.First());
                }
                catch(Exception e)
                {
                    Console.WriteLine("Remove err");
                    return false;
                }
                
                return true;
            }
            
        }
        public static bool Move(int id, int idW)
        {
            return false;
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

            return new Element[] { };
        }


    }
}

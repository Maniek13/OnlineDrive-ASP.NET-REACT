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
            {
                return false;
            }
            else
            {
                Element element = new() { Id = id, Name = name, Type = type, IdW = idW };
                _list.Add(element);
                return true;
            }
           
        }

        public bool Edit(int id) {
            return false;
        }
        public bool Delete(int id)
        {
            return false;
        }
        public bool Move(int id, int idW)
        {
            return false;
        }
        public List<Element> Sort(int idW, string type)
        {
            return _list;
        }


    }
}

﻿using System;
using System.Collections.Generic;
using TreeExplorer.Models;

namespace TreeExplorer.Interfaces
{
    interface ITree
    {
        static bool Add(int id, string name, string type, int idW) => throw new NotImplementedException();
        static bool Edit(Element element) => throw new NotImplementedException();
        static bool Delete(int id) => throw new NotImplementedException();
        static bool Move(int id, int idW) => throw new NotImplementedException();
        static IEnumerable<Element> Sort(int idW, string type) => throw new NotImplementedException();
        List<Element> Show();
    }
}

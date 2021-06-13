using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TreeExplorer.Data;
using TreeExplorer.Models;
using TreeExplorer.Objects;

namespace TreeExplorer.Controllers
{
    [Produces("application/json")]
    public class ElementsController : Controller
    {
        private readonly TreeExplorerContext _context;

        public ElementsController(TreeExplorerContext context)
        {
            _context = context;
        }

        // GET: Elements/Show
        public async Task<JsonResult> Show()
        {
            List<Element> list = new List<Element> { };
            try
            {
                list = _context.Element.ToListAsync().Result;
            }
            catch (Exception e)
            {
                Console.WriteLine("SQL err");
                return Json(new { Error = "SQl"});
            }

            Tree tree = new Tree(list);
            return Json(tree.Show());
        }



        // GET: Elements/Add
        [HttpGet]
        public async Task<Boolean> Add(string name, string type, int idW)
        {   
            Element element = new() { Name = name, Type = type, IdW = idW };

            if (TryValidateModel(element, nameof(element)))
            {
                int id = _context.Element.ToListAsync().Result.Last().Id + 2;
                if (Tree.Add(id, name, type, idW))
                {
                    try
                    {
                        _context.Add(element);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Query err");

                        Tree.Delete(id);

                        return false;
                    }
               
                }
                return false;
               
            }
            else
            {
                return false;
            }
        }


        // GET: Elements/Delete?id=1
        [HttpGet]
        public async Task<Boolean> Delete(int id)
        {
            if (Tree.Delete(id))
            {
                try
                {
                    _context.Remove(_context.Element.SingleOrDefault(x => x.Id == id));
                    _context.SaveChanges();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Query err");

                    var el = _context.Element.ElementAt(id);
                    Tree.Add(el.Id, el.Name, el.Type, el.IdW);
                    
                    return false;
                }
                
            }
            return false;
        }

        // GET: Elements/Edit
        [HttpGet]
        public async Task<Boolean> Edit(int id)
        {
            return false;
        }


        // GET: Elements/Move
        [HttpGet]
        public async Task<Boolean> Move(int id, int idW)
        {
            return false;
        }

        // GET: Elements/Sort?IdW=2&Type=ASC
        [HttpGet]
        public async Task<JsonResult> Sort(int idW, string type)
        {
            return Json(Tree.Sort(idW, type));
        }
    }
}



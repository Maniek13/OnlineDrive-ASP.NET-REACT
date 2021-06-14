using System;
using System.Collections.Generic;
using System.Linq;
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
        public JsonResult Show()
        {
            List<Element> list;
            try
            {
                list = _context.Element.ToListAsync().Result;
            }
            catch (Exception e)
            {
                Console.WriteLine("Show err");
                Console.WriteLine(e.Message);

                return Json(new { Error = e.Message });
            }

            Tree tree = new(list);
            return Json(tree.Set());
        }


        // Post: Elements/Add
        [HttpPost]
        public async Task<Boolean> Add([Bind("Name,Type,IdW")] Element element)
        {
            if (TryValidateModel(element, nameof(element)))
            {
                int id;
                if(Tree.Show().Count == 0)
                {
                    id = 0;
                }
                else
                {
                    id = _context.Element.ToListAsync().Result.Last().Id+1;
                }

                if (Tree.Add(id, element.Name, element.Type, element.IdW))
                {
                    try
                    {
                        _context.Add(element);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Add err");
                        Console.WriteLine(e.Message);

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


        // Post: Elements/Delete
        [HttpPost]
        public async Task<Boolean> Delete([Bind("Id")] int id)
        {
            if (Tree.Delete(id))
            {
                try
                {
                    _context.Remove(_context.Element.SingleOrDefault(x => x.Id == id));
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch(Exception e)
                {
                    Console.WriteLine("Delete err");
                    Console.WriteLine(e.Message);


                    Element el = _context.Element.ElementAt(id);
                    Tree.Add(el.Id, el.Name, el.Type, el.IdW);
                    
                    return false;
                }
                
            }
            return false;
        }

        // Post: Elements/Edit
        [HttpPost]
        public async Task<Boolean> Edit([Bind("Id,Name,Type,IdW")] Element elementNew)
        {
          

            if (elementNew.Name != null)
            {
                if (Tree.Edit(elementNew))
                {
                    var element = _context.Element.SingleOrDefault(x => x.Id == elementNew.Id);

                    try
                    {
                        element.Name = elementNew.Name;
                        element.Type = elementNew.Type;
                        element.IdW = elementNew.IdW;

                        _context.Update(element);
                        await _context.SaveChangesAsync();
                        return true;
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Edit err");
                        Console.WriteLine(e.Message);

                        element = _context.Element.SingleOrDefault(x => x.Id == elementNew.Id);

                        Tree.Edit(element);

                        return false;
                    }
                }
                else
                    return false;
            }
            return false;


            
        }


        // Post: Elements/Move
        [HttpPost]
        public async Task<Boolean> Move([Bind("Id")] int id, [Bind("IdW")] int idW)
        {
            if (Tree.Move(id, idW))
            {
                Element element = _context.Element.SingleOrDefault(x => x.Id == id);
                int idWOld = element.IdW;
                try
                {
                    element.IdW = idW;
                    _context.Update(element);             
                    await _context.SaveChangesAsync();
                    return true;
                }
                catch (Exception e)
                {
                    Console.WriteLine("Move err");
                    Console.WriteLine(e.Message);

                    Tree.Move(id, idWOld);

                    return false;
                }
            }
            else
                return false;
        }

        // Post: Elements/Sort
        [HttpPost]
        public JsonResult Sort([Bind("IdW")] int idW, [Bind("Type")] string type)
        {
            return Json(Tree.Sort(idW, type));
        }
    }
}



using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
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
        public async Task<JsonResult> Add([Bind("Name,Type,IdW")] Element element)
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

                Responde responde = Tree.Add(id, element.Name, element.Type, element.IdW);
                if (responde.Error == false)
                {
                    try
                    {
                        _context.Add(element);
                        await _context.SaveChangesAsync();
                        return Json(new { Ok = true });
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("Add err");
                        Console.WriteLine(e.Message);

                        Tree.Delete(id);

                        return Json(new { Error = e.Message });
                    }
               
                }
                return Json(new { Error = responde.Message }); 
               
            }
            else
            {
                return Json(new { Ok = false });
            }
        }


        // Post: Elements/Delete
        [HttpPost]
        public async Task<JsonResult> Delete([Bind("Id")] int id)
        {
            Responde responde = Tree.Delete(id);
            if (responde.Error == false )
            {
                try
                {
                    List<Element> listToDel = JsonConvert.DeserializeObject<List<Element>>(responde.Message);

                    listToDel.ForEach(el =>
                    {
                        _context.Element.RemoveRange(listToDel);
                        _context.SaveChangesAsync();

                    });

               
                    return Json(new { Ok = true });
                }
                catch(Exception e)
                {
                    Console.WriteLine("Delete err");
                    Console.WriteLine(e.Message);


                    Element el = _context.Element.ElementAt(id);
                    Tree.Add(el.Id, el.Name, el.Type, el.IdW);

                    return Json(new { Error = "Delete err" });
                    }
                
            }
            return Json(new { Error  = responde.Message });
        }

        // Post: Elements/Edit
        [HttpPost]
        public async Task<JsonResult> Edit([Bind("Id,Name,Type,IdW")] Element elementNew)
        {
            if (elementNew.Name != null)
            {
                Responde responde = Tree.Edit(elementNew);
                if (responde.Error == false)
                {
                    Element element = _context.Element.SingleOrDefault(x => x.Id == elementNew.Id);

                    try
                    {
                        element.Name = elementNew.Name;
                        element.Type = elementNew.Type;
                        element.IdW = elementNew.IdW;

                        _context.Update(element);
                        await _context.SaveChangesAsync();
                        return Json(new { Ok = true });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Edit err");
                        Console.WriteLine(e.Message);

                        element = _context.Element.SingleOrDefault(x => x.Id == elementNew.Id);

                        Tree.Edit(element);

                        return Json(new { Error = "Edit err" });
                    }
                }
                else
                    return Json(new { Error = responde.Message });
            }
            return Json(new { Ok = false });
        }


        // Post: Elements/Move
        [HttpPost]
        public async Task<JsonResult> Move([Bind("Id")] int id, [Bind("IdW")] int idW)
        {
            Responde responde = Tree.Move(id, idW);
            if (responde.Error == false)
            {
                Element element = _context.Element.SingleOrDefault(x => x.Id == id);
                int idWOld = element.IdW;
                try
                {
                    element.IdW = idW;
                    _context.Update(element);             
                    await _context.SaveChangesAsync();
                    return Json(new { Ok = true });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Move err");
                    Console.WriteLine(e.Message);

                    Tree.Move(id, idWOld);

                    return Json(new { Error = "Move err" });
                }
            }
            else
                return Json(new { Error = responde.Message });
        }

        // Post: Elements/Sort
        [HttpPost]
        public async Task<JsonResult> Sort([Bind("Id")] int id, [Bind("Type")] string type)
        {
            return Json(new { Ok = Tree.Sort(id, type)});
        }
    }
}



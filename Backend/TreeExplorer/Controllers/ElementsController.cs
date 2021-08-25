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

        public IActionResult Index()
        {
            return View("Views/Server/Index.cshtml");
        }

        // GET: Elements/Set
        public JsonResult Set()
        {
            List<Element> list;
            try
            {
                list = _context.Element.ToListAsync().Result;
                Tree.Set(new(list));
            }
            catch (Exception e)
            {
                Console.WriteLine("Show err");
                Console.WriteLine(e.Message);
                return Json(new { Error = e.Message });
            }
            
            return Json(new { Ok = true });
        }

        // GET: Elements/Show
        public JsonResult Show()
        {
            return Json(Tree.Get());
        }

        [HttpPost]
        public JsonResult Show([Bind("UsserId")] int usserId)
        {
            if (usserId != 0)
            {
                List<Element> list = Tree.Get(usserId);

                if(list != null)
                {
                    return Json(new { Tree = list});
                }
                else
                {
                    return Json(new { Error = "Server not responde. If you are administrator please set the data first" });
                }
              
            }
            else
            {
                return Json(new { Error = "Wrong index of usser" });
            }
        }


        // Post: Elements/Add
        [HttpPost]
        public async Task<JsonResult> Add([Bind("Name,Type,IdW, UsserId")] Element element)
        {
            if (TryValidateModel(element, nameof(element)))
            {
                int id;
                if(Tree.Get().Count == 0)
                {
                    id = 0;
                }
                else
                {
                    id = _context.Element.ToListAsync().Result.Last().Id+1;
                }

                Responde responde = Tree.Add(id, element.Name, element.Type, element.IdW, element.UsserId);
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

                    _context.Element.RemoveRange(listToDel);
                    await _context.SaveChangesAsync();

                    return Json(new { Ok = true });
                }
                catch(Exception e)
                {
                    Console.WriteLine("Delete err");
                    Console.WriteLine(e.Message);


                    Element el = _context.Element.ElementAt(id);
                    Tree.Add(el.Id, el.Name, el.Type, el.IdW, el.UsserId);

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
                Element element = _context.Element.SingleOrDefault(x => x.Id == elementNew.Id);

                List<Element> branch = Tree.Branch(elementNew.Id);


                bool ok = true;

                if(elementNew.IdW != element.IdW)
                {
                   branch.ForEach(el =>
                    {
                        if (el.Id == elementNew.IdW)
                        {
                            ok =  false;
                        }
                    });
                }
                

                if(ok == true)
                {
                    Responde responde = Tree.Edit(elementNew);
                    if (responde.Error == false)
                    {
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

                List<Element> branch = Tree.Branch(id);


                bool ok = true;
                branch.ForEach(el =>
                {
                    if (el.Id == idW)
                    {
                        ok = false;
                    }
                });
                
                if(ok == true)
                {
                    try
                    {
                        element.IdW = idW;
                        _context.Element.Update(element);
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
                return Json(new { Ok = false });

            }
            else
                return Json(new { Error = responde.Message });
        }

        // Post: Elements/Sort
        [HttpPost]
        public JsonResult Sort([Bind("Id")] int id, [Bind("Type")] string type)
        {
            return Json(new { Ok = Tree.Sort(id, type)});
        }

    }
}



using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
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
        private readonly ElementContext _context;
        private readonly string path = @System.IO.Directory.GetCurrentDirectory().ToString() + "\\Disk\\UssersFiles\\";

        public ElementsController(ElementContext context)
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

                return Json(new { e.Message, Status = 500 });
            }
            
            return Json(new { Message = true, Status = 200 });
        }

        // GET: Elements/Show
        public JsonResult Show()
        {
            return Json(new { Message = Tree.Get(), Status = 200});
        }

        [HttpPost]
        public JsonResult Show([Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {

            var els = UsserQuery(usserId, password);


            if (els != 0)
            {
                if (usserId != 0)
                {
                    List<Element> list = Tree.Get(usserId);

                    if (list != null)
                    {
                        return Json(new { Message = list, Status = 200 });
                    }
                    else
                    {
                        return Json(new { Message = "Server not responde. If you are administrator please set the data first", Status = 500 });
                    }

                }
                else
                {
                    return Json(new { Message = "Wrong index of usser", Status = 400 });
                }
            }
            else
            {
                return Json(new { Message = "Wrong data", Status = 400 });
            }
        }


        // Post: Elements/Add
        [HttpPost]
        public async Task<JsonResult> Add([Bind("Name,Type,IdW,UsserId")] Element element, [Bind("File")] IFormFile file, [Bind("Password")] string password)
        {

            var els = UsserQuery(element.UsserId, password);

            if (els != 0)
            {
                if (TryValidateModel(element, nameof(element)))
                {
                    if(element.Name != "" || element.Type == "file")
                    {
                        try
                        {
                            string path = this.path + element.UsserId + "\\";

                            List<string> fileStructure = Tree.FindPath(element.IdW);

                            foreach (string folder in fileStructure)
                            {
                                path += folder + "\\";
                            }


                            if (element.Type == "file")
                            {
                                if (file != null)
                                {
                                    path += file.FileName;
                                    element.Path = path;

                                    if (file.Length > 0)
                                    {
                                        using var stream = System.IO.File.Create(path);
                                        await file.CopyToAsync(stream);
                                    }
                                }
                                else
                                {
                                    return Json(new { Message = "Please chose a file", Status = 400 });
                                }

                            }
                            else
                            {
                                Directory.CreateDirectory(path + element.Name);
                            }

                            _context.Add(element);
                            await _context.SaveChangesAsync();

                            Responde responde = Tree.Add(element.Id, element.Name, element.Type, element.IdW, element.UsserId);

                            if (responde.Error == false)
                            {
                                return Json(new { Message = true, Status = 200 });
                            }
                            else
                            {
                                _context.Element.Remove(element);
                                await _context.SaveChangesAsync();
                                return Json(new { responde.Message, Status = 400 });
                            }



                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Add err");
                            Console.WriteLine(e.Message);

                            return Json(new { e.Message, Status = 500 });
                        }
                    }
                    else
                    {
                        return Json(new { Message = "Folder must hava a name", Status = 400 });
                    }
                    
                }
                else
                {
                    return Json(new { Message = "Wrong data", Status = 400 });
                }
            }
            else
            {
                return Json(new { Message = "Wrong data", Status = 400 });
            }
           
        }


        // Post: Elements/Delete
        [HttpPost]
        public async Task<JsonResult> Delete([Bind("Id")] int id, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            var els = UsserElementsQuery(id, usserId, password);

            if (els != 0)
            {
                Responde responde = Tree.Delete(id);

                if (responde.Error == false)
                {
                    try
                    {
                        List<Element> listToDel = JsonConvert.DeserializeObject<List<Element>>(responde.Message);

                        _context.Element.RemoveRange(listToDel);
                        await _context.SaveChangesAsync();

                        return Json(new { Message = true, Status = 200 });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Delete err");
                        Console.WriteLine(e.Message);


                        Element el = _context.Element.ElementAt(id);
                        Tree.Add(el.Id, el.Name, el.Type, el.IdW, el.UsserId);

                        return Json(new { Message = "Delete err", Status = 500 });
                    }

                }
                return Json(new { responde.Message, Status = 400 });
            }
            else
            {
                return Json(new { Message = "Wrong data", Status = 400 });
            }
           
        }

        // Post: Elements/Edit
        [HttpPost]
        public async Task<JsonResult> Edit([Bind("Id,Name,Type,IdW")] Element elementNew, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            var els = UsserElementsQuery(elementNew.Id, usserId, password);

            if (els != 0)
            {
                if (elementNew.Name != null)
                {
                    Element element = _context.Element.SingleOrDefault(x => x.Id == elementNew.Id);

                    List<Element> branch = Tree.Branch(elementNew.Id);


                    bool ok = true;

                    if (elementNew.IdW != element.IdW)
                    {
                        branch.ForEach(el =>
                        {
                            if (el.Id == elementNew.IdW)
                            {
                                ok = false;
                            }
                        });
                    }


                    if (ok == true)
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

                                return Json(new { Message = true, Status = 200 });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Edit err");
                                Console.WriteLine(e.Message);

                                element = _context.Element.SingleOrDefault(x => x.Id == elementNew.Id);

                                Tree.Edit(element);

                                return Json(new { Message = "Edit err", Status = 500 });
                            }
                        }
                        else
                        {
                            return Json(new { responde.Message, Status = 400 });
                        }

                    }
                    return Json(new { Message = "Can't move folder to child", Status = 400 });

                }
                return Json(new { Message = "Element must be a name", Status = 400 });
            }
            else
            {
                return Json(new { Message = "Wrong data", Status = 400 });
            }
        }


        // Post: Elements/Move
        [HttpPost]
        public async Task<JsonResult> Move([Bind("Id")] int id, [Bind("IdW")] int idW, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            Responde responde = Tree.Move(id, idW);
            if (responde.Error == false)
            {
                var els = UsserElementsQuery(id, usserId, password);

                if (els != 0)
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

                    if (ok == true)
                    {
                        try
                        {
                            element.IdW = idW;
                            _context.Element.Update(element);
                            await _context.SaveChangesAsync();

                            return Json(new { Message = true, Status = 200 });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Move err");
                            Console.WriteLine(e.Message);

                            Tree.Move(id, idWOld);

                            return Json(new { Message = "Move err", Status = 500 });
                        }
                    }
                    return Json(new { Message = false, Status = 400 });
                }
                else
                {
                    return Json(new { Message = "Wrong data", Status = 400 });
                }
            }
            else
            {
                return Json(new { responde.Message, Status = 400 });
            }
               
        }

        // Post: Elements/Sort
        [HttpPost]
        public JsonResult Sort([Bind("Id")] int id, [Bind("Type")] string type, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            var els = UsserElementsQuery(id, usserId, password);

            if (els != 0)
            {
                return Json(new { Message = Tree.Sort(id, type, usserId), Status = 200 });
            }
            else
            {
                return Json(new { Message = "Wrong data", Status = 400 });
            }


        }

        [HttpPost]
        public JsonResult NameOfFile([Bind("Id")] int id, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            var els = UsserElementsQuery(id, usserId, password);

            if (els != 0)
            {
                try
                {
                    string path = _context.Element.SingleOrDefault(el => el.Id == id).Path;
                    int index = path.LastIndexOf("\\");
                    string fileName = path[(index + 1)..];

                    return Json(new { Message = fileName, Status = 200 });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Name of file err");
                    Console.WriteLine(e.Message);

                    return Json(new { Message = "Name of file err", Status = 500 });
                }
            }
            else
            {
                return Json(new { Message = "Wrong data", Status = 400 });
            }

            
        }

        [HttpPost]
        public async Task<ActionResult> GetFile([Bind("Id")] int id, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            string path = _context.Element.SingleOrDefault(el => el.Id == id).Path;

            var els = UsserElementsQuery(id, usserId, password);

            if(els != 0)
            {

                var provider = new FileExtensionContentTypeProvider();
                if (!provider.TryGetContentType(path, out var contentType))
                {
                    contentType = "application/octet-stream";
                }

                var bytes = await System.IO.File.ReadAllBytesAsync(path);

                return File(bytes, contentType, Path.GetFileName(path));
            }

            return Json(new { Message = "Not found", Status = 500 });
        

        }

        private int UsserElementsQuery(int id, int usserId, string password)
        {
            var query = from el in _context.Set<Element>()
            join usser in _context.Set<Usser>()
             on el.UsserId equals usser.Id
            where el.Id == id && el.UsserId == usserId && usser.Password == password
            select new { el.Id };

            if (query.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        private int UsserQuery(int usserId, string password)
        {
            var query = from usser in _context.Set<Usser>()
                        where usser.Id == usserId && usser.Password == password
                        select new { usser.Id };

            if(query.Any())
            {
                return 1;
            }
            else
            {
                return 0;
            }
           
        }
    }
}



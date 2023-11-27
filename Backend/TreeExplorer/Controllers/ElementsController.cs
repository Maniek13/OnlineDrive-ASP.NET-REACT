using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TreeExplorer.Classes;
using TreeExplorer.Data;
using TreeExplorer.Models;

namespace TreeExplorer.Controllers
{
    [EnableCors("AllowSpecificOrigin")]
    [Produces("application/json")]
    public class ElementsController : Controller
    {
        private readonly TreeContext _context;
        private readonly string path = @System.IO.Directory.GetCurrentDirectory().ToString() + "\\Disk\\UssersFiles\\";
        private readonly Tree _tree;

        public ElementsController(TreeContext context)
        {
            _context = context;
            _tree = new();
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
                list = _context.Elements.ToListAsync().Result;
                _tree.Set(new(list));
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
            return Json(new { Message = _tree.Get(), Status = 200 });
        }

        [HttpPost]
        public JsonResult Show([Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            try
            {
                var els = UsserQuery(usserId, password);

                if (els != 0)
                {
                    if (usserId != 0)
                    {
                        HashSet<Element> list = _tree.Get(usserId);

                        return list != null ?
                            Json(new { Message = list, Status = 200 }) :
                            Json(new { Message = "Server not responde. If you are administrator please set the data first", Status = 500 });
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
            catch (Exception e)
            {
                return Json(new { e.Message, Status = 500 });
            }
        }

        // Post: Elements/Add
        [HttpPost]
        public async Task<JsonResult> Add([Bind("Name,Type,IdW,UsserId")] Element element, [Bind("File")] IFormFile file, [Bind("Password")] string password)
        {
            try
            {
                var els = UsserQuery(element.UsserId, password);

                if (els != 0)
                {
                    if (TryValidateModel(element, nameof(element)))
                    {
                        if (element.Name != "" || element.Type == "file")
                        {
                            try
                            {
                                string path = this.path + element.UsserId + "\\";
                                List<string> fileStructure = _tree.FindPath(element.IdW);

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
                                            stream.Dispose();
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

                                Responde responde = _tree.Add(element.Id, element.Name, element.Type, element.IdW, element.UsserId);

                                if (responde.Error == false)
                                {
                                    return Json(new { Message = true, Status = 200 });
                                }
                                else
                                {
                                    _context.Elements.Remove(element);
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
            catch (Exception e)
            {
                return Json(new { e.Message, Status = 500 });
            }
        }

        // Post: Elements/Delete
        [HttpPost]
        public async Task<JsonResult> Delete([Bind("Id")] int id, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            try
            {
                var els = UsserElementsQuery(id, usserId, password);

                if (els != 0)
                {
                    Responde responde = _tree.Delete(id);

                    if (responde.Error == false)
                    {
                        try
                        {
                            List<Element> listToDel = JsonConvert.DeserializeObject<List<Element>>(responde.Message);
                            var el = listToDel.Find(el => el.Id == id);
                            string name = el.Name;
                            string path = this.path + el.UsserId + "\\";

                            List<string> fileStructure = _tree.FindPath(el.IdW);

                            foreach (string folder in fileStructure)
                            {
                                path += folder + "\\";
                            }

                            if (el.Type == "file")
                            {
                                path += name;
                                System.IO.File.Delete(path);
                            }
                            else
                            {
                                Directory.Delete(path + name, true);
                            }

                            _context.Elements.RemoveRange(listToDel);
                            await _context.SaveChangesAsync();

                            return Json(new { Message = true, Status = 200 });
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine("Delete err");
                            Console.WriteLine(e.Message);

                            Element el = _context.Elements.ElementAt(id);
                            _tree.Add(el.Id, el.Name, el.Type, el.IdW, el.UsserId);

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
            catch (Exception e)
            {
                return Json(new { e.Message, Status = 500 });
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
                    Element element = _context.Elements.SingleOrDefault(x => x.Id == elementNew.Id);

                    HashSet<Element> branch = _tree.Branch(elementNew.Id);

                    bool ok = elementNew.IdW == element.IdW || branch.FirstOrDefault(el => el.Id == elementNew.IdW) == null;

                    if (ok == true)
                    {
                        Responde responde = _tree.Edit(elementNew);
                        if (responde.Error == false)
                        {
                            try
                            {
                                string name = element.Name;

                                List<string> fileStructureOld = _tree.FindPath(element.IdW);
                                string oldPath = this.path + element.UsserId + "\\";

                                foreach (string folder in fileStructureOld)
                                {
                                    oldPath += folder + "\\";
                                }

                                element.Name = elementNew.Name;
                                element.Type = elementNew.Type;
                                element.IdW = elementNew.IdW;

                                string path = this.path + element.UsserId + "\\";

                                List<string> fileStructure = _tree.FindPath(element.IdW);

                                foreach (string folder in fileStructure)
                                {
                                    path += folder + "\\";
                                }

                                if (element.Type == "file")
                                {
                                    oldPath += name;
                                    path += element.Name;

                                    System.IO.File.Copy(oldPath, path);
                                    System.IO.File.Delete(oldPath);
                                }
                                else
                                {
                                    Directory.Move(oldPath + name, path + elementNew.Name);
                                }

                                _context.Update(element);
                                await _context.SaveChangesAsync();

                                return Json(new { Message = true, Status = 200 });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Edit err");
                                Console.WriteLine(e.Message);

                                element = _context.Elements.SingleOrDefault(x => x.Id == elementNew.Id);

                                _tree.Edit(element);

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
            try
            {
                Responde responde = _tree.Move(id, idW);
                if (responde.Error == false)
                {
                    var els = UsserElementsQuery(id, usserId, password);

                    if (els != 0)
                    {
                        Element element = _context.Elements.SingleOrDefault(x => x.Id == id);
                        int idWOld = element.IdW;

                        HashSet<Element> branch = _tree.Branch(id);
                        bool ok = branch.FirstOrDefault(el => el.Id == idW) == null;

                        if (ok == true)
                        {
                            try
                            {
                                string name = element.Name;

                                List<string> fileStructureOld = _tree.FindPath(element.IdW);

                                string oldPath = this.path + element.UsserId + "\\";

                                foreach (string folder in fileStructureOld)
                                {
                                    oldPath += folder + "\\";
                                }

                                element.IdW = idW;

                                string path = this.path + element.UsserId + "\\";

                                List<string> fileStructure = _tree.FindPath(idW);

                                foreach (string folder in fileStructure)
                                {
                                    path += folder + "\\";
                                }

                                if (element.Type == "file")
                                {
                                    oldPath += name;
                                    path += name;

                                    System.IO.File.Copy(oldPath, path);
                                    System.IO.File.Delete(oldPath);
                                }
                                else
                                {
                                    Directory.Move(oldPath + name, path + name);
                                }

                                _context.Elements.Update(element);
                                await _context.SaveChangesAsync();

                                return Json(new { Message = true, Status = 200 });
                            }
                            catch (Exception e)
                            {
                                Console.WriteLine("Move err");
                                Console.WriteLine(e.Message);

                                _tree.Move(id, idWOld);

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
            catch (Exception e)
            {
                return Json(new { e.Message, Status = 500 });
            }
        }

        // Post: Elements/Sort
        [HttpPost]
        public JsonResult Sort([Bind("Id")] int id, [Bind("Type")] string type, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            return UsserQuery(usserId, password) != 0 ?
                Json(new { Message = _tree.Sort(id, type, usserId), Status = 200 }) :
                Json(new { Message = "Wrong data", Status = 400 });
        }

        [HttpPost]
        public JsonResult NameOfFile([Bind("Id")] int id, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            try
            {
                var els = UsserElementsQuery(id, usserId, password);

                if (els != 0)
                {
                    try
                    {
                        string path = _context.Elements.SingleOrDefault(el => el.Id == id).Path;
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
            catch (Exception e)
            {
                return Json(new { e.Message, Status = 500 });
            }
        }

        [HttpPost]
        public async Task<ActionResult> GetFile([Bind("Id")] int id, [Bind("UsserId")] int usserId, [Bind("Password")] string password)
        {
            try
            {
                string path = _context.Elements.SingleOrDefault(el => el.Id == id).Path;
                var els = UsserElementsQuery(id, usserId, password);

                if (els != 0)
                {
                    var provider = new FileExtensionContentTypeProvider();
                    if (!provider.TryGetContentType(path, out string contentType))
                    {
                        contentType = "application/octet-stream";
                    }

                    var bytes = await System.IO.File.ReadAllBytesAsync(path);

                    return File(bytes, contentType, Path.GetFileName(path));
                }

                return Json(new { Message = $"Error: file path{path} {els}", Status = 500 });
            }
            catch (Exception e)
            {
                return Json(new { e.Message, Status = 500 });
            }
        }

        private int UsserElementsQuery(int id, int usserId, string password)
        {
            var query = from el in _context.Set<Element>()
                        join usser in _context.Set<Usser>()
                        on el.UsserId equals usser.Id
                        where el.Id == id && el.UsserId == usserId && usser.Password == password
                        select new { el.Id };

            return query.Any() ? 1 : 0;
        }

        private int UsserQuery(int usserId, string password)
        {
            var query = from usser in _context.Set<Usser>()
                        where usser.Id == usserId && usser.Password == password
                        select new { usser.Id };

            return query.Any() ? 1 : 0;
        }
    }
}



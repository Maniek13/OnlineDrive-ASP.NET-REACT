using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using TreeExplorer.Data;
using TreeExplorer.Models;

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
            return Json(await _context.Element.ToListAsync());
        }



        // GET: Elements/Create
        [HttpGet]
        public async Task<Boolean> Add(string Name, string Type, int IdW)
        {
            Element element = new Element();

            element.Name = Name;
            element.Type = Type;
            element.IdW = IdW;
            

            if (ModelState.IsValid)
            {
                _context.Add(element);
                await _context.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
        }


        // GET: Elements/Delete
        [HttpGet]
        public async Task<Boolean> Delete(int Id)
        {
                return false;   
        }

        // GET: Elements/Edit
        [HttpGet]
        public async Task<Boolean> Edit(int Id)
        {
            return false;
        }


        // GET: Elements/Move
        [HttpGet]
        public async Task<Boolean> Move(int Id, int IdW)
        {
            return false;
        }

        // GET: Elements/Sort
        [HttpGet]
        public async Task<JsonResult> Sort(int IdW, string Type)
        {
            return Json(new List<Element>());
        }
    }
}



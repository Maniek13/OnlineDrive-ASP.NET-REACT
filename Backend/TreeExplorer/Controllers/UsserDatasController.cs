using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using TreeExplorer.Data;
using TreeExplorer.Models;

namespace TreeExplorer.Controllers
{

    namespace TreeExplorer.Controllers
    {
        [Produces("application/json")]
        public class UsserDatasController : Controller
        {
            private readonly UsserDataContext _context;

            public UsserDatasController(UsserDataContext context)
            {
                _context = context;
            }

            [HttpPost]
            public async Task<JsonResult> SaveUsserData([Bind("Name,UsserId,IpV4,Browser")] UsserData usserData)
            {
                if (TryValidateModel(usserData, nameof(usserData)))
                {
                    try
                    {
                        _context.Add(usserData);
                        await _context.SaveChangesAsync();


                        return Json(new { Message = "Data is saved", Status = 200 });
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
                    return Json(new { Message = "Usser no valid", Status = 400 });
                }
            }


            [HttpPost]
            public JsonResult IsSaved([Bind("IpV4")] string ipV4, [Bind("Browser")] string browser)
            {
                try
                {
                    UsserData? data = _context.UsserData.SingleOrDefault(el => el.IpV4 == ipV4 && el.Browser == browser);

                    if(data != null)
                    {
                        return Json(new { Message  = new { Id = data.UsserId, Name = data.Name}, Status = 200 });
                    }

                    return Json(new { Message = 0, Status = 200 });
                }
                catch (Exception e)
                {
                    Console.WriteLine("Add err");
                    Console.WriteLine(e.Message);

                    return Json(new { e.Message, Status = 500 });
                }
            }

            [HttpPost]
            public async Task<JsonResult> RemoveData([Bind("IpV4")] string ipV4, [Bind("Browser")] string browser)
            {
                try
                {
                    UsserData data = _context.UsserData.SingleOrDefault(el => el.IpV4 == ipV4 && el.Browser == browser);
                    _context.UsserData.RemoveRange(data);
                    await _context.SaveChangesAsync();

                    return Json(new { Message = "Usser wad succesfully deleted", Status = 200 });

                }
                catch (Exception e)
                {
                    Console.WriteLine("Add err");
                    Console.WriteLine(e.Message);

                    return Json(new { e.Message, Status = 500 });
                }
            
            }
        }
    }

}

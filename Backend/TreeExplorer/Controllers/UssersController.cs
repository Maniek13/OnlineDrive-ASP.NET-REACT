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
        public class UssersController : Controller
        {
            private readonly UsserContext _context;

            public UssersController(UsserContext context)
            {
                _context = context;
            }

            [HttpPost]
            public async Task<JsonResult> Add([Bind("Name,Password")] Usser usser)
            {
                if (TryValidateModel(usser, nameof(usser)))
                {
                    try
                    {
                        Usser finded = _context.Usser.Where(el => el.Name == usser.Name && el.Password == usser.Password).First();

                        if (finded == null)
                        {
                            _context.Add(usser);
                            await _context.SaveChangesAsync();
                            return Json(new { Ok = true });
                        }
                        else
                        {
                            return Json(new { Ok = false, message = "Usser alredy exist" });
                        }


                       
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Add err");
                        Console.WriteLine(e.Message);
                        return Json(new { Error = e.Message });
                    }
                }
                else
                {
                    return Json(new { Ok = false });
                }
            }

            [HttpPost]
            public JsonResult Confirm([Bind("Name,Password")] Usser usser)
            {
                if (TryValidateModel(usser, nameof(usser)))
                {
                    try
                    {
                        Usser finded = _context.Usser.Where(el => el.Name == usser.Name && el.Password == usser.Password).First();
                        
                        if(finded != null)
                        {
                            return Json(new { Ok = true });
                        }

                        return Json(new { Ok = false });
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("Add err");
                        Console.WriteLine(e.Message);
                        return Json(new { Error = e.Message });
                    }
                }
                else
                {
                    return Json(new { Ok = false });
                }
            }
        }
    }

}

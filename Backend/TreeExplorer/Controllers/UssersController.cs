using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TreeExplorer.Data;
using TreeExplorer.Models;
using TreeExplorer.Objects;

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
                        Usser finded = _context.Usser.Where(el => el.Name == usser.Name).FirstOrDefault();

                        if (finded == null)
                        {
                            string password = Crypto.EncryptSha256(usser.Password);
                            usser.Password = password;
                            _context.Add(usser);
                            await _context.SaveChangesAsync();
                            return Json(new { Usser = usser.Id });
                        }
                        else
                        {
                            return Json(new { Usser = false, Message = "Usser alredy exist" });
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
                    return Json(new { Usser = false, Message = "Usser no valid" });
                }
            }

            [HttpPost]
            public JsonResult Confirm([Bind("Name,Password")] Usser usser)
            {
                if (TryValidateModel(usser, nameof(usser)))
                {
                    try
                    {
                        string password = Crypto.EncryptSha256(usser.Password);
                        Usser finded = _context.Usser.Where(el => el.Name == usser.Name && el.Password == password).FirstOrDefault();
                        
                        if(finded != null)
                        {
                            return Json(new { Usser = finded.Id });
                        }

                        return Json(new { Usser = false, Message = "Usser no exist" });
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
                    return Json(new { Usser = false, Message = "Usser no valid" });
                }
            }
        }
    }

}

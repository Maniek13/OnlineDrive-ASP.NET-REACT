using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
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
            private readonly string path = System.IO.Directory.GetCurrentDirectory();

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
                            
                            Directory.CreateDirectory(this.path + "\\Disk\\UssersFiles\\" + usser.Id);

                            return Json(new { Message = usser.Id, Status = 200 });
                        }
                        else
                        {
                            return Json(new { Message = "Usser alredy exist", Status = 400 });
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
                    return Json(new { Message = "Usser no valid", Status = 400 });
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
                            return Json(new { Message = finded.Id, Status = 200 });
                        }

                        return Json(new {  Message = "Usser no exist", Status = 400 });
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
        }
    }

}

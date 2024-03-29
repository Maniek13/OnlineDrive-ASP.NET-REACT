﻿using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TreeExplorer.Classes;
using TreeExplorer.Data;
using TreeExplorer.Models;

namespace TreeExplorer.Controllers
{
    namespace TreeExplorer.Controllers
    {
        [EnableCors("AllowSpecificOrigin")]
        [Produces("application/json")]
        public class UssersController : Controller
        {
            private readonly TreeContext _context;
            private readonly string path = System.IO.Directory.GetCurrentDirectory();

            public UssersController(TreeContext context)
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
                        Usser finded = _context.Ussers.Where(el => el.Name == usser.Name).FirstOrDefault();

                        if (finded == null)
                        {
                            Crypto crypto = new();
                            string password = crypto.EncryptSha256(usser.Password);
                            usser.Password = password;
                            _context.Add(usser);
                            await _context.SaveChangesAsync();

                            Directory.CreateDirectory(this.path + "\\Disk\\UssersFiles\\" + usser.Id);

                            return Json(new { Message = new { usser.Id, password }, Status = 200 });
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
                        Crypto crypto = new();
                        string password = crypto.EncryptSha256(usser.Password);
                        Usser finded = _context.Ussers.Where(el => el.Name == usser.Name && el.Password == password).FirstOrDefault();

                        return finded != null ?
                            Json(new { Message = new { finded.Id, password }, Status = 200 }) :
                            Json(new { Message = "Usser no exist", Status = 400 });
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

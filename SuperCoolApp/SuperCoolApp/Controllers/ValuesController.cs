using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Hosting;
using System.Net;
using SuperCoolApp.Models;
using Microsoft.AspNetCore.Http;
using System.IO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SuperCoolApp
{
  [Route("api/[controller]")]
  public class ValuesController : Controller
  {
    StoreContext _context;
    IHostingEnvironment _appEnvironment;

    public ValuesController(IHostingEnvironment appEnvironment, StoreContext context)//StoreContext context)//, IHostingEnvironment appEnvironment)
    {
      _context = context;
      _appEnvironment = appEnvironment;
    }

    // GET: api/values
    //[HttpGet]
    //public IEnumerable<string> Init()
    //{
    //  if (!Request.Cookies.ContainsKey("UserCookies"))
    //  {
    //    Response.Cookies.Append("UserCookies", new Random().Next(0, 10000000).ToString(), new CookieOptions() { Expires = DateTime.MaxValue });
    //  }
    //  return new string[] { "Ok" };
    //}

    [HttpPost]
    public async Task<IActionResult> AddFile(IFormFile uploadedFile)
    {
      if (uploadedFile != null)
      {
        // путь к папке Files
        string path = "/Files/" + uploadedFile.FileName;
        // сохраняем файл в папку Files в каталоге wwwroot
        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
        {
          await uploadedFile.CopyToAsync(fileStream);
        }
        Image file = new Image { Name = uploadedFile.FileName, Path = path, User = Request.Cookies["UserCookies"] };
        _context.Images.Add(file);
        _context.SaveChanges();
        return View("About", file);
      }
      return RedirectToAction("Index");

    }

  }
}

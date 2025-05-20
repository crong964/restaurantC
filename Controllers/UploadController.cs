using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using _netmvc.Data;
using _netmvc.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using NuGet.Protocol;

namespace netmvc.Controllers
{
    [Authorize]
    public class UploadController : Controller
    {

        private readonly MvcMovieContext _context;

        public UploadController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: Movies
        public IActionResult Index()
        {
            return View();
        }
        [RequestSizeLimit(100_000_000_000_000)]
        public async Task<IActionResult> Uploadfile(List<IFormFile> files)
        {
           
            List<string> ls = [];
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // var filePath = Path.GetTempFileName();
                    var slip = formFile.FileName.Split(".");
                    if (slip != null && slip.Length >= 2)
                    {
                        var filename = DateTime.Now.Ticks +"."+ slip[slip.Length - 1];
                        var filePath = System.IO.Directory.GetCurrentDirectory() + "/StaticFiles/" + filename;
                        ls.Add(filename);

                        using (var stream = System.IO.File.Create(filePath))
                        {
                            await formFile.CopyToAsync(stream);
                        }
                    }
                }
            }
            ViewData["ls"] = ls;
            return View();
        }

    }
}

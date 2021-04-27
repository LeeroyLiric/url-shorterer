using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Short_links.App;
using Short_links.Models;

namespace Short_links.Controllers
{
    public class HomeController : Controller
    {
    
        [HttpGet, Route("/")]
        public IActionResult Index()
        {   

            return View();
        }

        [HttpPost, Route("/")]
        public IActionResult Index(string url)
        {
            using (var context = new MyDbContext())
            {
                if (context.Urls.Any(u => u.source_url == url))
                {
                    string result = string.Empty;
                    var urls = context.Urls.Where(u => u.source_url ==url).ToList();
                    foreach (var s in urls)
                    {
                       result = s.shortened_url;
                    }
                    var temp = "Link is already in the database";
                    var link = $"https://localhost:44393/{result}";
                    ViewData["Result"] = temp;
                    ViewData["Url"] = link;
                    return View();
                }
                else
                {
                    Shortener shortURL = new Shortener(url);
                    var temp = "Shorted URL:";
                    var link = $"https://localhost:44393/{shortURL.Token}";
                    ViewData["Result"] = temp;
                    ViewData["Url"] = link;
                    return View();
                    
                }
            }
                      
        }

        [HttpGet, Route("/{token}")]
        public IActionResult NixRedirect([FromRoute] string token)
        {
            string result = string.Empty;
            using (var context = new MyDbContext())
            {
                var urls = context.Urls.Where(u=> u.shortened_url == token).ToList();
                foreach(var url in urls)
                {
                    result = url.source_url;
                }
                
            }
            return Redirect(result);
        }


    }
}

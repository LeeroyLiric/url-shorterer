using LiteDB;
using Short_links.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Short_links.App
{
		public class Shortener
		{
			public string Token { get; set; }
			private  Urls biturl;

			private Shortener GenerateToken()
			{
				string urlsafe = string.Empty;
				Enumerable.Range(48, 75).Where(i => i < 58 || i > 64 && i < 91 || i > 96).OrderBy(o => new Random().Next()).ToList().ForEach(i => urlsafe += Convert.ToChar(i));
				Token = urlsafe.Substring(new Random().Next(0, urlsafe.Length), new Random().Next(2, 6));
				return this;
			}
			public Shortener(string url)
			{
				biturl = new Urls() { source_url = url, shortened_url = GenerateToken().Token };
				using (var context = new MyDbContext())
				{				
						context.Urls.Add(biturl);
						context.SaveChanges();
				}
			}
		}
	
}

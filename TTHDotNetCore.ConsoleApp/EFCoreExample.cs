﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TTHDotNetCore.ConsoleApp.Models;

namespace TTHDotNetCore.ConsoleApp
{
    public class EFCoreExample
    {
        public void Read()
        {
            AppDbContext db = new AppDbContext();
            var lst = db.Blogs.Where(x => x.DeleteFlag == false).ToList();
            foreach (var item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }

        public void Create(string title,string author,string content) { 
            BlogDataModel blog = new BlogDataModel { 
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };
            AppDbContext db = new AppDbContext();
            db.Blogs.Add(blog);
            var result =db.SaveChanges();
            Console.WriteLine( result == 1 ? "Saving Successful" : "Saving Fail");

        }

        public void Edit(int id)
        {
            AppDbContext db = new AppDbContext();
            //var item = db.Blogs.Where( x=> x.BlogId == id ).FirstOrDefault();
            var item = db.Blogs.FirstOrDefault( x=> x.BlogId == id );


            if (item == null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);

        }

        public void Update(int id, string title , string author , string content)
        {
            AppDbContext db = new AppDbContext();
            var item = db.Blogs
                .AsNoTracking()
                .FirstOrDefault( x => x.BlogId == id );

            if (item == null) {
                Console.WriteLine("No Data Found");
                return;
            }

            if (!string.IsNullOrEmpty(title))
            {
                item.BlogTitle = title;
            }
            if (!string.IsNullOrEmpty(author))
            {
                item.BlogAuthor = author;
            }
            if (!string.IsNullOrEmpty(content))
            {
                item.BlogContent = content;
            }

            db.Entry(item).State = EntityState.Modified;
            var result = db.SaveChanges();

            Console.WriteLine( result == 1 ? "Updating Successful" : "Updating Successful");

        }
    }
}
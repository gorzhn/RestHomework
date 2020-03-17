using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace RestHomework.Models
{
    public static class HelperClass
    {


        public static List<Author> GetAllAuthors(string id = "")
        {
            List<Author> authors = new List<Author>();
            using (StreamReader r = new StreamReader(GetFilePath()))
            {
                string json = r.ReadToEnd();
                authors = JsonConvert.DeserializeObject<List<Author>>(json);
            };
            if (!String.IsNullOrEmpty(id)) {
                authors = authors.Where(x => x.UserId.Equals(id)).ToList();
            }

            return authors;
        }


        public static List<Article> GetAuthorsWithParameters(string id, string title = "", string datePublished = "", string level = "")
        {
            List<Author> authors = new List<Author>();

            using (StreamReader r = new StreamReader(GetFilePath()))
            {
                string json = r.ReadToEnd();
                authors = JsonConvert.DeserializeObject<List<Author>>(json);
            }
            List<Article> articles = authors.Find(x => x.UserId.Equals(id)).Articles;

            if (!String.IsNullOrEmpty(title)) {
                articles = articles.Where(x => x.Title.Contains(title)).ToList();
            }
            if (!String.IsNullOrEmpty(datePublished))
            {
                articles = articles.Where(x => x.DatePublished.Contains(datePublished)).ToList();
            }
            if (!String.IsNullOrEmpty(level))
            {
                articles = articles.Where(x => x.Level.Contains(level)).ToList();
            }
            return articles;



        }
        public static void PostAuthor(Author author) {
            List<Author> authors = new List<Author>();
            using (StreamReader r = new StreamReader(GetFilePath())) {
                string json = r.ReadToEnd();
                authors = JsonConvert.DeserializeObject<List<Author>>(json);
            }
            if (authors.Any(e => e.UserId == author.UserId)) {
                
                throw new Exception();
            }
            authors.Add(author);
            File.WriteAllText(GetFilePath(), JsonConvert.SerializeObject(authors));


        }


        public static void PostArticle(Article article, string id) {
            List<Author> authors = new List<Author>();
            using (StreamReader r = new StreamReader(GetFilePath())) {
                string json = r.ReadToEnd();
                authors = JsonConvert.DeserializeObject<List<Author>>(json);
            }
            Author a = authors.Find(x => x.UserId == id);
            a.Articles.Add(article);
            File.WriteAllText(GetFilePath(), JsonConvert.SerializeObject(authors));


        }
        //update article
        //ova e uzas
        public static void UpdateArticle(Article article, string id)
        {
            List<Author> authors = new List<Author>();
            using (StreamReader r = new StreamReader(GetFilePath()))
            {
                string json = r.ReadToEnd();
                authors = JsonConvert.DeserializeObject<List<Author>>(json);
            }
            Author a = authors.Find(x => x.UserId == id);
            Article editable = a.Articles.Where(x => x.Id == article.Id).FirstOrDefault();
            if (editable.DatePublished.Equals(article.DatePublished))
            {
                a.Articles.Remove(editable);
                a.Articles.Add(article);
            }

            else {
                throw new NotImplementedException();
            }

            
             File.WriteAllText(GetFilePath(), JsonConvert.SerializeObject(authors));

        }
        public static string GetFilePath()
        {
            return Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "Data", "Authors.json");
         }
    }

  
}
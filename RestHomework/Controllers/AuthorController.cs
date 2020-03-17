using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using RestHomework.Models;


namespace RestHomework.Controllers
{
    public class AuthorController : ApiController
    {
        // GET: api/Author
        [Route("api/Authors")]
        public List<Author> Get()
        {

            return HelperClass.GetAllAuthors();
        }

        // GET: api/Author/5/articles   
        [Route("api/Authors/{id}/articles")]
        public List<Article> GetWithParameters(string id, string title = "", string datePublished = "", string level = "" )
        {
            return HelperClass.GetAuthorsWithParameters(id,title,datePublished,level);
        }

        //GET: api/Authors/5
        [Route("api/Authors/{id}")]
        public List<Author> GetAuthorsById(string id)
        {
            return HelperClass.GetAllAuthors(id);
        }



        // POST: api/Author
        [HttpPost]
        [Route("api/Authors")]
        public IHttpActionResult Post([FromBody]Author value)
        {

            bool conf;
            Boolean.TryParse(Request.Headers.GetValues("auth").First(), out conf);
            if (conf)
            {
                HelperClass.PostAuthor(value);
               
                return StatusCode(System.Net.HttpStatusCode.Created);
            }
            else {
                HttpContext.Current.Response.StatusCode = 404;
                return StatusCode(System.Net.HttpStatusCode.Unauthorized);
            }
          
          
        }

        // POST api/Articles/id
        [Route("api/Authors/{id}/articles")]
        public IHttpActionResult CreateArticle([FromBody] Article article, string id)
        {
            
            bool conf;
            Boolean.TryParse(Request.Headers.GetValues("auth").First(),out conf);


            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789-";
            var stringChars = new char[10];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }
            string s = new string(stringChars);
            article.Id = s;
            if (conf) {
            

                HelperClass.PostArticle(article, id);
                return StatusCode(System.Net.HttpStatusCode.Created);

            }
            else return StatusCode(System.Net.HttpStatusCode.Unauthorized);
        }


        [HttpPut]
        [Route("api/Articles/{id}")]
        public IHttpActionResult EditArticle([FromBody] Article article, string id) {


            bool conf;
            Boolean.TryParse(Request.Headers.GetValues("auth").First(), out conf);
            if (conf)
            {

                HelperClass.UpdateArticle(article, id);
                return StatusCode(System.Net.HttpStatusCode.Created);
            }
            else return  StatusCode(System.Net.HttpStatusCode.Unauthorized);
        }

        //PUT api/Articles/5
        [HttpPost]
        [Route("api/Articles/{id}")]
        public IHttpActionResult PostArticle([FromBody] Article article, string id) 
        {

            HelperClass.PostArticle(article, id);
            return StatusCode(System.Net.HttpStatusCode.Created);

        }

    }
}

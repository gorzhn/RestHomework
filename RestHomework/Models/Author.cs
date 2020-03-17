using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RestHomework.Models
{
    public class Author
    {

    public string UserId { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public List<Article> Articles { get; set; }   
    }
}
using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace Haneef_Uploadify.Models
    {
    public class News
        {
        [Key]
        public int NewsID { get; set; }
        public string Title { get; set; }
        [AllowHtml]
        public string Content { get; set; }
        public HttpPostedFileBase Image { get; set; }
        }
    }
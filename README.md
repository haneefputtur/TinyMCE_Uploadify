Integrating TinyMCE Rich text Editor with ASP.NET MVC with Image Upload Feature of Uploadify Jquery Plugin

## Credits to Haneefputur.com

Step 1 : Download tinymce Editor from 
Extract the entire content into a folder called Tinymce
Step 2 : Download Uploadify ::
Extract the content to a folder called Uploadify and make sure it has following files

jquery.uploadify.js
jquery.uploadify.min.js
uploadify-cancel.png
uploadify.css
uploadify.swf

Step 3 : Create an Empty MVC project in Visual Studio 2013
Step 4 : Create a Model Called News.cs and add following 
______________________________________________________________
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
	
________________________________________________________________

Step 5 :
Create a Controller and Name it as NewsController.cs and copy following Code

______________________________________________________________
using System;
using System.IO;
using System.Web.Mvc;
using Haneef_Uploadify.Models;

namespace Haneef_Uploadify.Controllers
{
    public class NewsController : Controller
    {
     
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(News model)
            {
            return View("NewsDisplay", model);
            }
        public ActionResult Upload()
            {
            var file = Request.Files["Filedata"];
            string extension = Path.GetExtension(file.FileName);
            string fileid = Guid.NewGuid().ToString();
            fileid = Path.ChangeExtension(fileid, extension);

            string savePath = Server.MapPath(@"~\Uploads\" + fileid);
            file.SaveAs(savePath);

            return Content(Url.Content(@"~\Uploads\" + fileid));
            }
	}
}
______________________________________________________________

Step 6 :
Create a folder called Uploads in the root of the project
Copy the Folder Tinymce (from Step 1 ) into the Scripts folder
Copy the Folder Uploadify (from Step 2) into the Scripts folder

Step 7: 
Create a view Under Folder News , Name it as Index.cshtml and copy following Contents.

______________________________________________________________
@model Haneef_Uploadify.Models.News
@{
        ViewBag.Title = "Index";
}
<link href="~/Scripts/Uploadify/uploadify.css" rel="stylesheet" />
<script src="~/Scripts/Uploadify/jquery.uploadify.min.js"></script>
<script src="~/Scripts/Tinymce/tinymce.min.js"></script>

 <script>
    tinymce.init({
        selector: '#Content',
        height: 500,
        theme: 'modern',
        plugins: [
          'advlist autolink lists link image charmap print preview hr anchor pagebreak',
          'searchreplace wordcount visualblocks visualchars code fullscreen',
          'insertdatetime media nonbreaking save table contextmenu directionality',
          'emoticons template paste textcolor colorpicker textpattern imagetools'
        ],
        toolbar1: 'insertfile undo redo | styleselect | bold italic | alignleft aligncenter alignright alignjustify | bullist numlist outdent indent | link image',
        toolbar2: 'print preview media | forecolor backcolor emoticons ',
        image_advtab: true,
  

     });
</script>


    <script type="text/javascript">
        $(function () {
            
            $('#file_upload').uploadify({
         
                'swf': '/Scripts/Uploadify/uploadify.swf',
                'uploader': "@Url.Action("Upload", "News")",               
                'cancelImg': "@Url.Content("/Scripts/Uploadify/uploadify-cancel.png")",
                'fileSizeLimit': '1MB', // Add Kb, MB, GB
                'buttonText': 'Insert Images...', //Text for button
                'queueSizeLimit': 10, // Max number of files allowed
                'fileTypeDesc': 'Image Files',
                'fileTypeExts': '*.gif; *.jpg; *.png', // File type allowed
                'onUploadSuccess' : function(file, data, response) {
                 tinyMCE.activeEditor.execCommand("mceInsertContent", true, "<img src='" + data + "' alt='Uploaded Image' class='img-responsive' />");
                }
           
            })
            
        }
);
</script>
  

<div class="panel panel-primary">
    <div class="panel-heading panel-head">Add News</div>
    <div class="panel-body">
        @using (Html.BeginForm("Index", "News", FormMethod.Post, new { enctype = "multipart/form-data" }))
            {
            <div class="form-horizontal">
                <div class="form-group">
                    @Html.LabelFor(model => model.Title, new { @class = "col-lg-2 control-label" })
                    <div class="col-lg-9">
                        @Html.TextBoxFor(model => model.Title, new { @class = "form-control" })
                    </div>
                </div>

                <div class="form-group">
                    @Html.LabelFor(model => model.Image, new { @class = "col-lg-2 control-label" })
                    <div class="col-lg-9">
                        <input type="file" name="Img" id="file_upload" />
                    </div>
                </div>
                <div class="form-group">
                    @Html.LabelFor(model => model.Content, new { @class = "col-lg-2 control-label" })
                    <div class="col-lg-9">
                        @Html.TextAreaFor(model => model.Content, new { @class = "form-control", @row = 5 })
                    </div>
                </div>
       


                    <div class="form-group">
                        <div class="col-lg-9"></div>
                        <div class="col-lg-3">
                            <button class="btn btn-success" id="btnSubmit" type="submit">
                                Submit
                            </button>
                        </div>
                    </div>
                </div>
            }
    </div>
</div>
______________________________________________________________

Step 8 :
Create a view under folder and name it as NewsDisplay.cshtml and copy following content ( This will be used to display the content if you submit the form)

______________________________________________________________
@model Haneef_Uploadify.Models.News
@{
    ViewBag.Title = "News Display";
}

<h2>News Display - View The Submitted Content</h2>

<div class="panel panel-primary">
    <div class="panel-heading panel-head">Add News</div>
    <div class="panel-body">

        <div class="form-horizontal">
            <div class="form-group">
                @Html.LabelFor(model => model.Title, new { @class = "col-lg-2 control-label" })
                <div class="col-lg-9">
                    @Html.DisplayFor(model => model.Title, new { @class = "form-control" })
                </div>
            </div>
            <div class="form-group">
                @Html.LabelFor(model => model.Content, new { @class = "col-lg-2 control-label" })
                <div class="col-lg-9">
                    @Html.Raw(Model.Content)

                </div>
            </div>

        </div>

    </div>
</div>

______________________________________________________________

Step 9 :
Fine tune Uploadify.css
=> Change the location of the uploadify-cancel.png reference in uploadify.css ( Arround Line Number 74)
In my case it will look like this
______________________________________________________________
.uploadify-queue-item .cancel a {
	background: url('/Scripts/Uploadify/uploadify-cancel.png') 0 0 no-repeat;
	float: right;
	height:	16px;
	text-indent: -9999px;
	width: 16px;
}
______________________________________________________________

Step 10 :
Open Views => Shared => _Layout.cshtml file

Move the jquery bundle to header section  ( this will prevent error throwing by TinyMCE as well Uploadify)
In my case it will look as follows .

______________________________________________________________
<!DOCTYPE html>
<html>
<head>
    <meta charset="utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>@ViewBag.Title - My ASP.NET Application</title>
    @Styles.Render("~/Content/css")
    @Scripts.Render("~/bundles/modernizr")
    @Scripts.Render("~/bundles/jquery")
    @Scripts.Render("~/bundles/bootstrap")

</head>
<body>
    <div class="navbar navbar-inverse navbar-fixed-top">
        <div class="container">
            <div class="navbar-header">
                <button type="button" class="navbar-toggle" data-toggle="collapse" data-target=".navbar-collapse">
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                    <span class="icon-bar"></span>
                </button>
                @Html.ActionLink("Application name", "Index", "Home", null, new { @class = "navbar-brand" })
            </div>
            <div class="navbar-collapse collapse">
                <ul class="nav navbar-nav">
                    <li>@Html.ActionLink("Home", "Index", "Home")</li>
                    <li>@Html.ActionLink("About", "About", "Home")</li>
                    <li>@Html.ActionLink("Index", "Index", "News")</li>
                    <li>@Html.ActionLink("Contact", "Contact", "Home")</li>
                </ul>
                @Html.Partial("_LoginPartial")
            </div>
        </div>
    </div>
    <div class="container body-content">
        @RenderBody()
        <hr />
        <footer>
            <p>&copy; @DateTime.Now.Year - My ASP.NET Application</p>
        </footer>
    </div>


    @RenderSection("scripts", required: false)
</body>
</html>


______________________________________________________________


Refer : http://haneefputtur.com/step-step-instruction-integrate-tinymce-uploadify-using-asp-net-mvc-c.html for deatils

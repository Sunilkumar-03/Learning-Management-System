#pragma checksum "C:\Users\sanib\Documents\.Net Framework\Projects\Learning Management System\LearningManagementSystem\LearningManagementSystem.MVC\Views\User\Certificate.cshtml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "275b466ab7f2b9cd2d06b5813ad9b76a35ced59c645f14e8995b1675ef771675"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_User_Certificate), @"mvc.1.0.view", @"/Views/User/Certificate.cshtml")]
namespace AspNetCore
{
    #line hidden
    using global::System;
    using global::System.Collections.Generic;
    using global::System.Linq;
    using global::System.Threading.Tasks;
    using global::Microsoft.AspNetCore.Mvc;
    using global::Microsoft.AspNetCore.Mvc.Rendering;
    using global::Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\sanib\Documents\.Net Framework\Projects\Learning Management System\LearningManagementSystem\LearningManagementSystem.MVC\Views\_ViewImports.cshtml"
using LearningManagementSystem.MVC;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\sanib\Documents\.Net Framework\Projects\Learning Management System\LearningManagementSystem\LearningManagementSystem.MVC\Views\_ViewImports.cshtml"
using LearningManagementSystem.MVC.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"275b466ab7f2b9cd2d06b5813ad9b76a35ced59c645f14e8995b1675ef771675", @"/Views/User/Certificate.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"Sha256", @"1f7a27bb2125c0a4897875270337fed30d9b40aa9805d668d71eb687225e75d5", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_User_Certificate : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\sanib\Documents\.Net Framework\Projects\Learning Management System\LearningManagementSystem\LearningManagementSystem.MVC\Views\User\Certificate.cshtml"
  
    ViewData["Title"] = "Certificate";
    Layout = "~/Views/Shared/DashboardBody.cshtml";

#line default
#line hidden
#nullable disable
#nullable restore
#line 6 "C:\Users\sanib\Documents\.Net Framework\Projects\Learning Management System\LearningManagementSystem\LearningManagementSystem.MVC\Views\User\Certificate.cshtml"
   
    var Date = TempData["Date"];

#line default
#line hidden
#nullable disable
            WriteLiteral(@"<style>
    .hero-image {
        background-image: url(""/Layout/img/frame.png"");
        background-color: #cccccc;
        height: 450px;
        background-position: center;
        background-repeat: no-repeat;
        position: relative;
    }
</style>

<link rel=""preconnect"" href=""https://fonts.googleapis.com"">
<link rel=""preconnect"" href=""https://fonts.gstatic.com"" crossorigin>
<link href=""https://fonts.googleapis.com/css2?family=Parisienne&display=swap"" rel=""stylesheet"">
<title>Cerificate</title>

<script src=""https://ajax.googleapis.com/ajax/libs/jquery/3.3.1/jquery.min.js""></script>

<script src=""https://code.jquery.com/jquery-1.12.4.min.js"" integrity=""sha256-ZosEbRLbNQzLpnKIkEdrPv7lOy9C27hHQ+Xp8a4MxAQ="" crossorigin=""anonymous""></script>
<script src=""https://cdnjs.cloudflare.com/ajax/libs/jspdf/1.3.5/jspdf.min.js""></script>




<div class=""container hero-image"" style=""width: 750px; height: 450px; padding: 20px; text-align: center;"">
    <br /><br /><br />
    <span style=""f");
            WriteLiteral("ont-size:50px; font-weight:bold; font-family:Parisienne\">Certificate of Completion</span>\r\n    <br />\r\n    <span style=\"font-size:25px;font-family:Parisienne\">This is to certify that</span>\r\n    <span style=\"font-size:30px;\"><b>  ");
#nullable restore
#line 38 "C:\Users\sanib\Documents\.Net Framework\Projects\Learning Management System\LearningManagementSystem\LearningManagementSystem.MVC\Views\User\Certificate.cshtml"
                                  Write(ViewBag.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></span><br />\r\n    <span style=\"font-size:25px; font-family:Parisienne\">has completed the course</span> <br />\r\n    <span style=\"font-size:30px\"><b>  ");
#nullable restore
#line 40 "C:\Users\sanib\Documents\.Net Framework\Projects\Learning Management System\LearningManagementSystem\LearningManagementSystem.MVC\Views\User\Certificate.cshtml"
                                 Write(ViewBag.CourseName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b></span>\r\n    <span style=\"font-size:20px;font-family:Parisienne\">With Score Of </span><b>  ");
#nullable restore
#line 41 "C:\Users\sanib\Documents\.Net Framework\Projects\Learning Management System\LearningManagementSystem\LearningManagementSystem.MVC\Views\User\Certificate.cshtml"
                                                                             Write(ViewBag.Score);

#line default
#line hidden
#nullable disable
            WriteLiteral("</b><br />\r\n    <span style=\"font-size:20px;font-family:Parisienne\">Dated</span><b>  ");
#nullable restore
#line 42 "C:\Users\sanib\Documents\.Net Framework\Projects\Learning Management System\LearningManagementSystem\LearningManagementSystem.MVC\Views\User\Certificate.cshtml"
                                                                    Write(Date);

#line default
#line hidden
#nullable disable
            WriteLiteral(@"</b>
</div>



<br>
<br>
<center><input type=""button"" id=""create_pdf"" value=""Generate PDF""></center>

<script>(function () {
        var
            form = $('.container'),
            cache_width = 700, //form.width(),
            a4 = [595.28, 841.89]; // for a4 size paper width and height

        $('#create_pdf').on('click', function () {
            $('body').scrollTop(0);
            createPDF();
        });
        //create pdf  s
        function createPDF() {
            getCanvas().then(function (canvas) {
                var
                    img = canvas.toDataURL(""image/png""),
                    doc = new jsPDF({
                        unit: 'px',
                        format: 'a4'
                    });
                doc.addImage(img, 'JPEG', 1.2, 1);
                doc.save('certificate-creation.pdf');
                form.width(cache_width);
            });
        }

        // create canvas object
        function getCanvas() {
            form.w");
            WriteLiteral("idth((a4[0] * 1.33333) - 80).css(\'max-width\', \'none\');\r\n            return html2canvas(form, {\r\n                imageTimeout: 2000,\r\n                removeContainer: true\r\n            });\r\n        }\r\n\r\n    }());</script>\r\n\r\n");
        }
        #pragma warning restore 1998
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; } = default!;
        #nullable disable
        #nullable restore
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

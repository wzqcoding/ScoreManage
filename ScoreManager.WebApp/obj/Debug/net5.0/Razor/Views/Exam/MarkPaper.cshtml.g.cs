#pragma checksum "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Exam\MarkPaper.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "4c57597c471a3fa26e307e7fe56c0c48fdde9079"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Exam_MarkPaper), @"mvc.1.0.view", @"/Views/Exam/MarkPaper.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
#nullable restore
#line 1 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\_ViewImports.cshtml"
using ScoreManager;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\_ViewImports.cshtml"
using ScoreManager.Models;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Exam\MarkPaper.cshtml"
using Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"4c57597c471a3fa26e307e7fe56c0c48fdde9079", @"/Views/Exam/MarkPaper.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ebc828530f807eb7ccfc8fb1955cabd08d258795", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Exam_MarkPaper : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<List<EDU_EXAMDETIAL>>
    #nullable disable
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
            WriteLiteral("\r\n<div class=\"layui-card-body \">\r\n    <table class=\"layui-table layui-form\">\r\n        <thead>\r\n            <tr>\r\n\r\n                <th>ID</th>\r\n                <th>学生</th>\r\n                <th>操作</th>\r\n        </thead>\r\n        <tbody>\r\n");
#nullable restore
#line 15 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Exam\MarkPaper.cshtml"
             foreach (var item in Model)
            {
                string btnDisabled = item.Status == 1 ? "" : "layui-btn-disabled";

#line default
#line hidden
#nullable disable
            WriteLiteral("                <tr>\r\n\r\n                    <td>");
#nullable restore
#line 20 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Exam\MarkPaper.cshtml"
                   Write(item.ID);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td>");
#nullable restore
#line 21 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Exam\MarkPaper.cshtml"
                   Write(item.Student.NAME);

#line default
#line hidden
#nullable disable
            WriteLiteral("</td>\r\n                    <td class=\"td-manage\">\r\n");
            WriteLiteral("\r\n                       <button type=\"button\"");
            BeginWriteAttribute("onclick", " onclick=\"", 814, "\"", 849, 3);
            WriteAttributeValue("    ", 824, "CheckPaper(", 828, 15, true);
#nullable restore
#line 25 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Exam\MarkPaper.cshtml"
WriteAttributeValue("", 839, item.ID, 839, 8, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 847, ");", 847, 2, true);
            EndWriteAttribute();
            BeginWriteAttribute("class", " class=\"", 850, "\"", 880, 2);
            WriteAttributeValue("", 858, "layui-btn", 858, 9, true);
#nullable restore
#line 25 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Exam\MarkPaper.cshtml"
WriteAttributeValue(" ", 867, btnDisabled, 868, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(">查看试卷</button>\r\n                       \r\n                    </td>\r\n                </tr>\r\n");
#nullable restore
#line 29 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Exam\MarkPaper.cshtml"
            }

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
        </tbody>
    </table>
</div>



<script>
var CheckPaper = function(examDetailId,studentName) {
    //阅卷前再次检查试卷状态
    $.post(""/Exam/CheckExamStatus"", { ""examDetailId"": examDetailId }, function(ret) {
        if (ret.Code == 0) {
            window.open('/Exam/CheckPaper?examDetailId=' + examDetailId + '&studentName=' + studentName, '_blank');
        } else {
            layer.alert(ret.Message, { icon: 5 });
        }
    });
}
</script>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<List<EDU_EXAMDETIAL>> Html { get; private set; } = default!;
        #nullable disable
    }
}
#pragma warning restore 1591

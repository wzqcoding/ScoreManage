#pragma checksum "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Student\StudentList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "0de7cb0c2fa6c8359a3cc0e14e3d48839dddb48d"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Student_StudentList), @"mvc.1.0.view", @"/Views/Student/StudentList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0de7cb0c2fa6c8359a3cc0e14e3d48839dddb48d", @"/Views/Student/StudentList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"ebc828530f807eb7ccfc8fb1955cabd08d258795", @"/Views/_ViewImports.cshtml")]
    #nullable restore
    public class Views_Student_StudentList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    #nullable disable
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("layui-form layui-col-space5"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n");
#nullable restore
#line 2 "C:\Users\25171\Desktop\ScoreManager\ScoreManager.WebApp\Views\Student\StudentList.cshtml"
  
    ViewData["Title"] = "学生列表";
   

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"layui-fluid\">\r\n    <div class=\"layui-row layui-col-space15\">\r\n        <div class=\"layui-col-md12\">\r\n            <div class=\"layui-card\">\r\n                <div class=\"layui-card-body \">\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "0de7cb0c2fa6c8359a3cc0e14e3d48839dddb48d4159", async() => {
                WriteLiteral(@"

                        <div class=""layui-inline layui-show-xs-block"">
                            <input type=""text"" id=""KeyWords"" name=""KeyWords"" placeholder=""请输入学生姓名"" autocomplete=""off"" class=""layui-input"">
                        </div>
                        <div class=""layui-inline layui-show-xs-block"">
                            <button class=""layui-btn""");
                BeginWriteAttribute("lay-submit", " lay-submit=\"", 680, "\"", 693, 0);
                EndWriteAttribute();
                WriteLiteral(" onclick=\"    QueryPageInfo(); return false;\" lay-filter=\"sreach\"><i class=\"layui-icon\">&#xe615;</i></button>\r\n                        </div>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral(@"
                </div>
                <div class=""layui-card-header"">
                    <button class=""layui-btn"" onclick=""xadmin.open('添加学生','/Student/AddStudent',500,600)""><i class=""layui-icon""></i>添加</button>
                </div>
                <div id=""resultList""></div>

                <div class=""layui-card-body "">
                    <div id=""pageBar""></div>
                </div>
            </div>
        </div>
    </div>
</div>

<script>

    $(function() {
        QueryPageInfo();
    })
    var QueryPageInfo = function() {
        $.get(""/Student/StudentListPageInfo"", { ""KeyWords"": $(""#KeyWords"").val() }, function(ret){
            layui.use(['laypage', 'form'], function() {
                var laypage = layui.laypage;
                var layer = layui.layer;
                var form = layui.form;

                //总页数大于页码总数
                laypage.render({
                    elem: 'pageBar'
                    , count: ret.TotalCount //数据总数
             ");
            WriteLiteral(@"       , limit: ret.PageSize
                    , jump: function(obj) {
                        QueryPageList(obj.curr, ret.PageSize);
                    }
                });
            });
        })
    }
    var QueryPageList = function(pageIndex, pageSize) {
        $.post(""/Student/StudentListDetail"", { ""PageSize"": pageSize, ""PageIndex"": pageIndex, ""KeyWords"": $(""#KeyWords"").val() }, function(ret) {
            $(""#resultList"").html(ret);
        });

    }
    
    /*用户-删除*/
    function member_del(obj, id) {
        layer.confirm('确认要删除吗？', function(index) {
            //发异步删除数据
            $.post(""/Student/DeleteFullInfo"", { ""id"": id }, function(ret) {
                if (ret.Code == 0) {
                    $(obj).parents(""tr"").remove();
                    layer.msg('已删除!', { icon: 1, time: 1000 });
                }else {
                    layer.msg(ret.Message, { icon: 5, time: 1000 });
                }
            })
            
        });
    }



</scr");
            WriteLiteral("ipt>\r\n\r\n");
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

#pragma checksum "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "04670fd7d1d3a052a938c2f0212163e0be818507"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_PriceList_PriceList), @"mvc.1.0.view", @"/Views/PriceList/PriceList.cshtml")]
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
#line 1 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\_ViewImports.cshtml"
using BookingApp;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\_ViewImports.cshtml"
using BookingApp.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"04670fd7d1d3a052a938c2f0212163e0be818507", @"/Views/PriceList/PriceList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5dde4c95dde9f526c68e731729958bef7e6435ca", @"/Views/_ViewImports.cshtml")]
    public class Views_PriceList_PriceList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<BookingApp.ViewModels.PriceListViewModel>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("onsubmit", new global::Microsoft.AspNetCore.Html.HtmlString("return jQueryAjaxDelete(this)"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("d-inline"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n<table class=\"table\">\r\n    <thead>\r\n        <tr class=\"table-light\">\r\n            <th>\r\n                ");
#nullable restore
#line 7 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
           Write(Html.DisplayNameFor(model => model.PricePerPeople.People));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 10 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
           Write(Html.DisplayNameFor(model => model.PricePerPeople.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 16 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
         foreach (var item in Model.PriceList)
        {

#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr class=\"table-light\">\r\n                <td class=\"pricePerPeopleId d-none\">\r\n                    ");
#nullable restore
#line 20 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
               Write(Html.DisplayFor(modelItem => item.PricePerPeopleId));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td class=\"pricePerPeoplePeople\">\r\n                    ");
#nullable restore
#line 23 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
               Write(Html.DisplayFor(modelItem => item.People));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td class=\"pricePerPeoplePrice\">\r\n                    ");
#nullable restore
#line 26 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
               Write(Html.DisplayFor(modelItem => item.Price));

#line default
#line hidden
#nullable disable
            WriteLiteral(@"
                </td>
                <td align=""right"">
                    <a class=""pricePerPeopleEdit btn btn-sm btn-outline-warning border-0"">
                        <span class=""material-icons btn-material"">
                            edit
                        </span>
                    </a> |
                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("form", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "04670fd7d1d3a052a938c2f0212163e0be8185077283", async() => {
                WriteLiteral("\r\n                        <input type=\"hidden\" id=\"priceId\" name=\"priceId\"");
                BeginWriteAttribute("value", " value=\"", 1440, "\"", 1470, 1);
#nullable restore
#line 35 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
WriteAttributeValue("", 1448, item.PricePerPeopleId, 1448, 22, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(" />\r\n                        <input type=\"hidden\" id=\"objectId\" name=\"objectId\"");
                BeginWriteAttribute("value", " value=\"", 1550, "\"", 1573, 1);
#nullable restore
#line 36 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
WriteAttributeValue("", 1558, Model.ObjectId, 1558, 15, false);

#line default
#line hidden
#nullable disable
                EndWriteAttribute();
                WriteLiteral(@" />
                        <button type=""submit"" class=""btn btn-sm btn-outline-danger border-0"" value=""Delete"">
                            <span class=""material-icons btn-material"">
                                delete
                            </span>
                        </button>
                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.RenderAtEndOfFormTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_RenderAtEndOfFormTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.FormTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 34 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
                                                WriteLiteral(item.PricePerPeopleId);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_FormTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_2);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </td>\r\n            </tr>\r\n");
#nullable restore
#line 45 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\PriceList\PriceList.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>\r\n");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<BookingApp.ViewModels.PriceListViewModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

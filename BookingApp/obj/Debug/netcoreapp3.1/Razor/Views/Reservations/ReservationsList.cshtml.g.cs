#pragma checksum "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "d2d02872d3fe599233b6398c32b07c0e930de9bd"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Reservations_ReservationsList), @"mvc.1.0.view", @"/Views/Reservations/ReservationsList.cshtml")]
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
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"d2d02872d3fe599233b6398c32b07c0e930de9bd", @"/Views/Reservations/ReservationsList.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"5dde4c95dde9f526c68e731729958bef7e6435ca", @"/Views/_ViewImports.cshtml")]
    public class Views_Reservations_ReservationsList : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<BookingApp.Models.Reservation>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Delete", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
  
    string bg = " ";
    DateTime toDay = DateTime.Now;

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"row\">\r\n");
#nullable restore
#line 7 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
     foreach (var item in Model)
    {
        

#line default
#line hidden
#nullable disable
#nullable restore
#line 9 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
         if (@item.CheckOut < toDay) { bg = "bg-secondary"; }
        else
        {
            if (item.ReservationDepositPayment == true && item.ReservationPricePayment == false) { bg = "bg-primary"; }
            else if (item.ReservationPricePayment == true) { bg = "bg-success"; }
            else { bg = "bg-light"; }
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("        <div class=\"col-12 col-md-3 py-2\">\r\n            <div class=\"card\">\r\n                <div");
            BeginWriteAttribute("class", " class=\"", 616, "\"", 651, 4);
            WriteAttributeValue("", 624, "card-header", 624, 11, true);
            WriteAttributeValue(" ", 635, "p-0", 636, 4, true);
#nullable restore
#line 19 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
WriteAttributeValue(" ", 639, bg, 640, 3, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue(" ", 643, "rounded", 644, 8, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                    <h5 class=\"card-text text-center m-0 p-2 \">");
#nullable restore
#line 20 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
                                                          Write(item.CheckIn.ToString("dd.MM hh:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral(" - ");
#nullable restore
#line 20 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
                                                                                                  Write(item.CheckOut.ToString("dd.MM hh:mm"));

#line default
#line hidden
#nullable disable
            WriteLiteral("</h5>\r\n                </div>\r\n                <div class=\"card-body rounded text-center\">\r\n                    <a");
            BeginWriteAttribute("onclick", " onclick=\"", 910, "\"", 1036, 4);
            WriteAttributeValue("", 920, "showInPopup(\'", 920, 13, true);
#nullable restore
#line 23 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
WriteAttributeValue("", 933, Url.Action("AddOrEdit","Reservations",new {Id = item.Id },Context.Request.Scheme), 933, 82, false);

#line default
#line hidden
#nullable disable
            WriteAttributeValue("", 1015, "\',\'Edit", 1015, 7, true);
            WriteAttributeValue(" ", 1022, "Reservation\')", 1023, 14, true);
            EndWriteAttribute();
            WriteLiteral(">\r\n                        <h2 class=\"card-title text-uppercase\">");
#nullable restore
#line 24 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
                                                         Write(item.ObjectForRent.Name);

#line default
#line hidden
#nullable disable
            WriteLiteral("</h2>\r\n                        <p class=\"card-text\">");
#nullable restore
#line 25 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
                                        Write(item.Customer.FirstName);

#line default
#line hidden
#nullable disable
            WriteLiteral(" ");
#nullable restore
#line 25 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
                                                                 Write(item.Customer.LastName);

#line default
#line hidden
#nullable disable
            WriteLiteral("</p>\r\n                        <p class=\"card-text mb-0\">Zadatek: ");
#nullable restore
#line 26 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
                                                      Write(item.ReservationDeposit);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n                        <p class=\"card-text mb-4\">Całość: ");
#nullable restore
#line 27 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
                                                     Write(item.ReservationPrice);

#line default
#line hidden
#nullable disable
            WriteLiteral(" </p>\r\n                    </a>\r\n                    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "d2d02872d3fe599233b6398c32b07c0e930de9bd9237", async() => {
                WriteLiteral("\r\n                        <button class=\"btn-danger btn-delete\">X</button>\r\n                    ");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            if (__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues == null)
            {
                throw new InvalidOperationException(InvalidTagHelperIndexerAssignment("asp-route-id", "Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper", "RouteValues"));
            }
            BeginWriteTagHelperAttribute();
#nullable restore
#line 29 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
                                             WriteLiteral(item.Id);

#line default
#line hidden
#nullable disable
            __tagHelperStringValueBuffer = EndWriteTagHelperAttribute();
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"] = __tagHelperStringValueBuffer;
            __tagHelperExecutionContext.AddTagHelperAttribute("asp-route-id", __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.RouteValues["id"], global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n                </div>\r\n            </div>\r\n        </div>\r\n");
#nullable restore
#line 35 "C:\Users\matik\Desktop\Semestr 6\PdIwtA - Programowanie dla Internetu\Projekt\BookingApp\BookingApp\Views\Reservations\ReservationsList.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</div>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<BookingApp.Models.Reservation>> Html { get; private set; }
    }
}
#pragma warning restore 1591

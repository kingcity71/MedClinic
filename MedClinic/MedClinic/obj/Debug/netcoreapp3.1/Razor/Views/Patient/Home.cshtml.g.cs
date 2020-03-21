#pragma checksum "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "a264db7f0542bdeaf1ee3026c986d2c5780f392f"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Patient_Home), @"mvc.1.0.view", @"/Views/Patient/Home.cshtml")]
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
#line 1 "D:\repos\MedClinic\MedClinic\MedClinic\Views\_ViewImports.cshtml"
using MedClinic;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "D:\repos\MedClinic\MedClinic\MedClinic\Views\_ViewImports.cshtml"
using MedClinic.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"a264db7f0542bdeaf1ee3026c986d2c5780f392f", @"/Views/Patient/Home.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"27f2191f64b257bf3fd22346d4aebc8794614a8c", @"/Views/_ViewImports.cshtml")]
    public class Views_Patient_Home : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<MedClinic.Model.PatientModel>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
#nullable restore
#line 2 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
  
    ViewData["Title"] = "Home";

#line default
#line hidden
#nullable disable
            WriteLiteral("<div class=\"container\">\r\n    <div class=\"row\">\r\n        <div class=\"col-lg-4\">\r\n            <div class=\"row\">\r\n                <div class=\"col-lg-12\">\r\n                    <img");
            BeginWriteAttribute("src", " src=\"", 253, "\"", 271, 1);
#nullable restore
#line 10 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
WriteAttributeValue("", 259, Model.Photo, 259, 12, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(" class=\"img-thumbnail\" />\r\n                </div>\r\n            </div>\r\n            <div class=\"row\">\r\n                <div class=\"col-lg-12 p-1\">\r\n                    <a");
            BeginWriteAttribute("href", " href=\"", 441, "\"", 466, 2);
            WriteAttributeValue("", 448, "\\patient\\", 448, 9, true);
#nullable restore
#line 15 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
WriteAttributeValue("", 457, Model.Id, 457, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                        <button type=""button"" class=""btn btn-primary w-100"">
                            Профиль
                        </button>
                    </a>
                </div>
                <div class=""col-lg-12 p-1"">
                    <button type=""button"" class=""btn btn-outline-primary w-100"">
                        Запись к специалисту
                    </button>
                </div>
                <div class=""col-lg-12 p-1"">
                    <a");
            BeginWriteAttribute("href", " href=\"", 965, "\"", 994, 2);
            WriteAttributeValue("", 972, "\\patientData\\", 972, 13, true);
#nullable restore
#line 27 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
WriteAttributeValue("", 985, Model.Id, 985, 9, false);

#line default
#line hidden
#nullable disable
            EndWriteAttribute();
            WriteLiteral(@">
                        <button type=""button"" class=""btn btn-outline-primary w-100"">
                            Результаты анализов
                        </button>
                    </a>
                </div>
                <div class=""col-lg-12 p-1"">
                    <button type=""button"" class=""btn btn-outline-primary w-100"">
                        Заключения специалистов
                    </button>
                </div>
            </div>
        </div>
        <div class=""col-lg-8"">
            <div class=""row"">
                <div class=""col-lg-12 profile-wrapper"">
                    <div class=""col-lg-12"">
                        <div class=""text-center"">
                            ");
#nullable restore
#line 45 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
                       Write(Model.FullName);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-lg-12\">\r\n                        <div class=\"text-left\">\r\n                            Дата Рождения : ");
#nullable restore
#line 50 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
                                       Write(Model.BirthDate.ToShortDateString());

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-lg-12\">\r\n                        <div class=\"text-left\">\r\n                            Пол : ");
#nullable restore
#line 55 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
                              Write(Model.IsMan ? "М" : "Ж");

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-lg-12\">\r\n                        <div class=\"text-left\">\r\n                            Полис : ");
#nullable restore
#line 60 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
                               Write(Model.MedData);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-lg-12\">\r\n                        <div class=\"text-left\">\r\n                            Паспорт : ");
#nullable restore
#line 65 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
                                 Write(Model.PassData);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                    <div class=\"col-lg-12\">\r\n                        <div class=\"text-left\">\r\n                            Email : ");
#nullable restore
#line 70 "D:\repos\MedClinic\MedClinic\MedClinic\Views\Patient\Home.cshtml"
                               Write(Model.Email);

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                        </div>\r\n                    </div>\r\n                </div>\r\n            </div>\r\n        </div>\r\n    </div>\r\n</div>\r\n\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<MedClinic.Model.PatientModel> Html { get; private set; }
    }
}
#pragma warning restore 1591

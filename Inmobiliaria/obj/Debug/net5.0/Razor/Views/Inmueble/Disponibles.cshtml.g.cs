#pragma checksum "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7485c01ffeedde69b225a7e7573645b67ba3a4c6"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Inmueble_Disponibles), @"mvc.1.0.view", @"/Views/Inmueble/Disponibles.cshtml")]
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
#line 1 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\_ViewImports.cshtml"
using Inmobiliaria;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\_ViewImports.cshtml"
using Inmobiliaria.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7485c01ffeedde69b225a7e7573645b67ba3a4c6", @"/Views/Inmueble/Disponibles.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"edf381669dde6995b9201d231bb8643b95e5b7cd", @"/Views/_ViewImports.cshtml")]
    public class Views_Inmueble_Disponibles : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<Inmobiliaria.Models.Inmueble>>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Index", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("style", new global::Microsoft.AspNetCore.Html.HtmlString("text-decoration:none"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
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
            WriteLiteral("\r\n");
#nullable restore
#line 3 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
  
    ViewData["Title"] = "Disponibles";
    List<Inmueble> inmuebles = ViewData[nameof(Inmueble)] as List<Inmueble>;
    string dis = "Disponible";
    string nodis = "No disponible";

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n<h1>Inmuebles Disponibles</h1>\r\n<p>\r\n    ");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7485c01ffeedde69b225a7e7573645b67ba3a4c64256", async() => {
                WriteLiteral("<span><i class=\"bi bi-reply\"></i></span>Volver");
            }
            );
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
            __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_0.Value;
            __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_0);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n</p>\r\n<table class=\"table\">\r\n    <thead>\r\n\r\n        <tr>\r\n\r\n            <th>\r\n                ");
#nullable restore
#line 20 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
           Write(Html.DisplayNameFor(model => model.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 23 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
           Write(Html.DisplayNameFor(model => model.Uso));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 26 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
           Write(Html.DisplayNameFor(model => model.Direccion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                Ambientes\r\n            </th>\r\n            <th>\r\n                ");
#nullable restore
#line 32 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
           Write(Html.DisplayNameFor(model => model.Precio));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </th>\r\n            <th>\r\n                Datos Propietario\r\n            </th>\r\n\r\n            <th>\r\n                Estado\r\n            </th>\r\n            <th></th>\r\n        </tr>\r\n    </thead>\r\n    <tbody>\r\n");
#nullable restore
#line 45 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
         foreach (var item in inmuebles)
        {


#line default
#line hidden
#nullable disable
            WriteLiteral("            <tr>\r\n\r\n                <td>\r\n                    ");
#nullable restore
#line 51 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
               Write(Html.DisplayFor(modelItem => item.Tipo));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 54 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
               Write(Html.DisplayFor(modelItem => item.Uso));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 57 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
               Write(Html.DisplayFor(modelItem => item.Direccion));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 60 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
               Write(Html.DisplayFor(modelItem => item.CantAmbientes));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                <td>\r\n                    ");
#nullable restore
#line 63 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
               Write(Html.DisplayFor(modelItem => item.Precio));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n\r\n                <td>\r\n                    ");
#nullable restore
#line 67 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
               Write(Html.DisplayFor(modelItem => item.PropietarioInmueble.Nombre));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                    ");
#nullable restore
#line 68 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
               Write(Html.DisplayFor(modelItem => item.PropietarioInmueble.Apellido));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n\r\n\r\n                <td>\r\n\r\n");
#nullable restore
#line 74 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
                      
                        if (item.Disponible == 1)
                        {
                            

#line default
#line hidden
#nullable disable
#nullable restore
#line 77 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
                       Write(Html.DisplayFor(modelItem => dis));

#line default
#line hidden
#nullable disable
#nullable restore
#line 77 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
                                                              }
                        else
                        { 

#line default
#line hidden
#nullable disable
#nullable restore
#line 79 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
                     Write(Html.DisplayFor(modelItem => nodis));

#line default
#line hidden
#nullable disable
#nullable restore
#line 79 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
                                                              }

                    

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n                </td>\r\n                \r\n            </tr>\r\n");
#nullable restore
#line 86 "C:\Users\Usuario\source\repos\Inmobiliaria\Inmobiliaria\Views\Inmueble\Disponibles.cshtml"
        }

#line default
#line hidden
#nullable disable
            WriteLiteral("    </tbody>\r\n</table>");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<Inmobiliaria.Models.Inmueble>> Html { get; private set; }
    }
}
#pragma warning restore 1591
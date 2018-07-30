using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using VetSoft.Presentation.Models;

namespace VetSoft.Presentation.Helpers
{
    public static class HtmlExtensions
    {
        public static IHtmlString PacienteCuadros(this HtmlHelper<IPagedList<PacienteViewModel>> htmlHelper, IEnumerable<PacienteViewModel> model)
        {
            var macroDiv = new TagBuilder("div");
            macroDiv.AddCssClass("card-columns");
            var macroBuilder = new StringBuilder();
            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);
            foreach (var item in model)
            {

                var outerDiv = new TagBuilder("div");
                var cardBody = new TagBuilder("div");
                var cardUL = new TagBuilder("ul");
                var cardFooter = new TagBuilder("div");
                outerDiv.AddCssClass("card border-info mb-3");
                cardBody.AddCssClass("card-body");
                cardUL.AddCssClass("list-group list-group-flush");
                cardFooter.AddCssClass("card-footer");

                //--Card Body
                var nombreH = new TagBuilder("h5");
                nombreH.AddCssClass("card-title");
                nombreH.InnerHtml = DisplayExtensions.DisplayFor(htmlHelper, m => item.Nombre).ToHtmlString() +
                    (string.IsNullOrEmpty(item.Apellido) ? " " : " " + item.Apellido);
                var subRaza = new TagBuilder("h6");
                subRaza.AddCssClass("card-subtitle mb-2 text-muted");
                subRaza.InnerHtml = $"Raza : {DisplayExtensions.DisplayFor(htmlHelper, m => item.Raza.Nombre).ToHtmlString()}";
                var subChip = new TagBuilder("h6");
                subChip.AddCssClass("card-subtitle mb-2 text-muted");
                StringBuilder subChipText = new StringBuilder();
                subChipText.Append(DisplayNameExtensions.DisplayNameFor(htmlHelper, m => m.First().Microchip_Licencia).ToHtmlString());
                subChipText.Append(" : ");
                subChipText.Append(DisplayExtensions.DisplayFor(htmlHelper, m => item.Microchip_Licencia).ToHtmlString());
                subChip.InnerHtml = subChipText.ToString();
                var pTag = new TagBuilder("p");
                pTag.AddCssClass("card-text");
                pTag.InnerHtml = $"Fecha Registro : {item.FechaIngreso.ToShortDateString()}";
                StringBuilder bodyText = new StringBuilder();
                bodyText.Append(nombreH.ToString(TagRenderMode.Normal));
                bodyText.Append(subRaza.ToString(TagRenderMode.Normal));
                bodyText.Append(subChip.ToString(TagRenderMode.Normal));
                bodyText.Append(pTag.ToString(TagRenderMode.Normal));
                cardBody.InnerHtml = bodyText.ToString();
                //------

                //--Card UL
                var liEdad = new TagBuilder("li");
                var liColor = new TagBuilder("li");
                var liGenero = new TagBuilder("li");
                liEdad.AddCssClass("list-group-item");
                liColor.AddCssClass("list-group-item");
                liGenero.AddCssClass("list-group-item");

                var now = DateTime.Now;
                var nac = item.FechaNac;
                var diff = now - nac;
                var years = (diff.Days / 365);
                var message = $"Edad : {years} años y {diff.Days - (years * 365)} dias.";
                liEdad.InnerHtml = message;
                var colorText = new StringBuilder();
                colorText.Append(DisplayNameExtensions.DisplayNameFor(htmlHelper, m => m.First().Color).ToHtmlString());
                colorText.Append(" : ");
                colorText.Append(DisplayExtensions.DisplayFor(htmlHelper, m => item.Color).ToHtmlString());
                liColor.InnerHtml = colorText.ToString();
                var generoText = new StringBuilder();
                generoText.Append(DisplayNameExtensions.DisplayNameFor(htmlHelper, m => m.First().Genero).ToHtmlString());
                generoText.Append(" : ");
                generoText.Append(DisplayExtensions.DisplayFor(htmlHelper, m => item.Genero).ToHtmlString());
                liGenero.InnerHtml = generoText.ToString();

                var cardUlText = new StringBuilder();
                cardUlText.Append(liEdad.ToString(TagRenderMode.Normal));
                cardUlText.Append(liColor.ToString(TagRenderMode.Normal));
                cardUlText.Append(liGenero.ToString(TagRenderMode.Normal));
                cardUL.InnerHtml = cardUlText.ToString();
                //------

                //--Card Footer

                var editarBtn = new TagBuilder("a");
                editarBtn.AddCssClass("card-link-btn");
                editarBtn.MergeAttribute("title", "Editar Datos Paciente");
                editarBtn.MergeAttribute("href", $"{urlHelper.Action("Editar")}/{item.ID}");
                editarBtn.InnerHtml = " <i class='fas fa-edit'></i> ";
                var deleteBtn = new TagBuilder("span");
                deleteBtn.AddCssClass("card-link-btn");
                deleteBtn.MergeAttribute("onclick", $"DeleteThis({item.ID})");
                deleteBtn.InnerHtml = "Eliminar";
                var linkVer = LinkExtensions.ActionLink(htmlHelper, "Ver Lista de Chequeos", "Listar", "Vet", new { id = 0 }, htmlAttributes: new { @class = "card-link" });
                var linkPro = LinkExtensions.ActionLink(htmlHelper, "Ver Propietario(s)", "VerPropietarios", new { id = item.ID }, htmlAttributes: new { @class = "card-link" });
                var footerText = new StringBuilder();

                footerText.Append(editarBtn.ToString(TagRenderMode.Normal));
                footerText.Append(" | ");
                footerText.Append(linkVer.ToHtmlString());
                footerText.Append(" | ");
                footerText.Append(deleteBtn.ToString(TagRenderMode.Normal));
                footerText.Append(" | ");
                footerText.Append(linkPro.ToHtmlString());

                cardFooter.InnerHtml = footerText.ToString();
                //------

                //Renderizando Outer BODY
                StringBuilder htmlBuilder = new StringBuilder();
                htmlBuilder.Append(cardBody.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(cardUL.ToString(TagRenderMode.Normal));
                htmlBuilder.Append(cardFooter.ToString(TagRenderMode.Normal));
                outerDiv.InnerHtml = htmlBuilder.ToString();

                macroBuilder.Append(outerDiv.ToString(TagRenderMode.Normal));
            }
            macroDiv.InnerHtml = macroBuilder.ToString();
            var html = macroDiv.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(html);
        }

        public static IHtmlString CuadroPaciente(this HtmlHelper<PacienteViewModel> htmlHelper, PacienteViewModel model)
        {

            var urlHelper = new UrlHelper(htmlHelper.ViewContext.RequestContext);

            var outerDiv = new TagBuilder("div");
            var cardBody = new TagBuilder("div");
            var cardUL = new TagBuilder("ul");
            var cardFooter = new TagBuilder("div");
            outerDiv.AddCssClass("card border-info mb-3");
            cardBody.AddCssClass("card-body");
            cardUL.AddCssClass("list-group list-group-flush");
            cardFooter.AddCssClass("card-footer");

            //--Card Body
            var nombreH = new TagBuilder("h5");
            nombreH.AddCssClass("card-title");
            nombreH.InnerHtml = DisplayExtensions.DisplayFor(htmlHelper, m => model.Nombre).ToHtmlString() +
                (string.IsNullOrEmpty(model.Apellido) ? " " : " " + model.Apellido);
            var subRaza = new TagBuilder("h6");
            subRaza.AddCssClass("card-subtitle mb-2 text-muted");
            subRaza.InnerHtml = $"Raza : {DisplayExtensions.DisplayFor(htmlHelper, m => model.Raza.Nombre).ToHtmlString()}";
            var subChip = new TagBuilder("h6");
            subChip.AddCssClass("card-subtitle mb-2 text-muted");
            StringBuilder subChipText = new StringBuilder();
            subChipText.Append(DisplayNameExtensions.DisplayNameFor(htmlHelper, m => m.Microchip_Licencia).ToHtmlString());
            subChipText.Append(" : ");
            subChipText.Append(DisplayExtensions.DisplayFor(htmlHelper, m => model.Microchip_Licencia).ToHtmlString());
            subChip.InnerHtml = subChipText.ToString();
            var pTag = new TagBuilder("p");
            pTag.AddCssClass("card-text");
            pTag.InnerHtml = $"Fecha Registro : {model.FechaIngreso.ToShortDateString()}";
            StringBuilder bodyText = new StringBuilder();
            bodyText.Append(nombreH.ToString(TagRenderMode.Normal));
            bodyText.Append(subRaza.ToString(TagRenderMode.Normal));
            bodyText.Append(subChip.ToString(TagRenderMode.Normal));
            bodyText.Append(pTag.ToString(TagRenderMode.Normal));
            cardBody.InnerHtml = bodyText.ToString();
            //------

            //--Card UL
            var liEdad = new TagBuilder("li");
            var liColor = new TagBuilder("li");
            var liGenero = new TagBuilder("li");
            liEdad.AddCssClass("list-group-item");
            liColor.AddCssClass("list-group-item");
            liGenero.AddCssClass("list-group-item");

            var now = DateTime.Now;
            var nac = model.FechaNac;
            var diff = now - nac;
            var years = (diff.Days / 365);
            var message = $"Edad : {years} años y {diff.Days - (years * 365)} dias.";
            liEdad.InnerHtml = message;
            var colorText = new StringBuilder();
            colorText.Append(DisplayNameExtensions.DisplayNameFor(htmlHelper, m => m.Color).ToHtmlString());
            colorText.Append(" : ");
            colorText.Append(DisplayExtensions.DisplayFor(htmlHelper, m => model.Color).ToHtmlString());
            liColor.InnerHtml = colorText.ToString();
            var generoText = new StringBuilder();
            generoText.Append(DisplayNameExtensions.DisplayNameFor(htmlHelper, m => m.Genero).ToHtmlString());
            generoText.Append(" : ");
            generoText.Append(DisplayExtensions.DisplayFor(htmlHelper, m => model.Genero).ToHtmlString());
            liGenero.InnerHtml = generoText.ToString();

            var cardUlText = new StringBuilder();
            cardUlText.Append(liEdad.ToString(TagRenderMode.Normal));
            cardUlText.Append(liColor.ToString(TagRenderMode.Normal));
            cardUlText.Append(liGenero.ToString(TagRenderMode.Normal));
            cardUL.InnerHtml = cardUlText.ToString();
            //------

            //--Card Footer

            var editarBtn = new TagBuilder("a");
            editarBtn.AddCssClass("card-link-btn");
            editarBtn.MergeAttribute("title", "Editar Datos Paciente");
            editarBtn.MergeAttribute("href", $"{urlHelper.Action("Editar")}/{model.ID}");
            editarBtn.InnerHtml = " <i class='fas fa-edit'></i> ";
            var deleteBtn = new TagBuilder("span");
            deleteBtn.AddCssClass("card-link-btn");
            deleteBtn.MergeAttribute("onclick", $"DeleteThis({model.ID})");
            deleteBtn.InnerHtml = "Eliminar";
            var linkVer = LinkExtensions.ActionLink(htmlHelper, "Ver Lista de Chequeos", "Listar", "Vet", new { id = 0 }, htmlAttributes: new { @class = "card-link" });
            var linkPro = LinkExtensions.ActionLink(htmlHelper, "Ver Propietario(s)", "VerPropietarios", new { id = model.ID }, htmlAttributes: new { @class = "card-link" });
            var footerText = new StringBuilder();

            footerText.Append(editarBtn.ToString(TagRenderMode.Normal));
            footerText.Append(" | ");
            footerText.Append(linkVer.ToHtmlString());
            footerText.Append(" | ");
            footerText.Append(deleteBtn.ToString(TagRenderMode.Normal));
            footerText.Append(" | ");
            footerText.Append(linkPro.ToHtmlString());

            cardFooter.InnerHtml = footerText.ToString();
            //------

            //Renderizando Outer BODY
            StringBuilder htmlBuilder = new StringBuilder();
            htmlBuilder.Append(cardBody.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(cardUL.ToString(TagRenderMode.Normal));
            htmlBuilder.Append(cardFooter.ToString(TagRenderMode.Normal));
            outerDiv.InnerHtml = htmlBuilder.ToString();



            var html = outerDiv.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(html);
        }
    }
}
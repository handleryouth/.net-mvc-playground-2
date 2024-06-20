using System.Linq.Expressions;
using contactForm.Enum;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace contactForm.Helper
{
    public static class CustomHelper {


        #region input
        public static IHtmlContent Input<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression,  InputType type = InputType.text, IDictionary<string, string> htmlAttributes = null)
        {

            var htmlFieldName = html.IdFor(expression).ToString();

            var builder = new TagBuilder("input");

            var inputType = type.ToInputTypeString();
            builder.Attributes.Add("type", inputType);
            builder.Attributes.Add("id", htmlFieldName);
            builder.TagRenderMode = TagRenderMode.SelfClosing;

           
            if(htmlAttributes != null)
            {
                builder.MergeAttributes(htmlAttributes);
            }

            
            if (inputType == "checkbox")
            {
                builder.AddCssClass("form-check-input");
            }  else
            {
                builder.AddCssClass("form-control");
            }


            return builder;
        }

        public static IHtmlContent InputWithLabel<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            var labelInstance = Label(html, expression);
            var inputInstance = Input(html, expression);
            var content = new HtmlContentBuilder()
                .AppendHtml("<div>")
                 .AppendHtml(labelInstance)
                 .AppendHtml(inputInstance)
                 .AppendHtml("</div>");

            return content;
        }


        #endregion

        #region label
        public static IHtmlContent Label<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string customLabelText = null, IDictionary<string, string> htmlAttributes = null)
        {
            var metadata = html.MetadataProvider.GetMetadataForProperty(typeof(TModel), GetPropertyName(expression));
            var htmlFieldName = html.IdFor(expression).ToString();
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            if (string.IsNullOrEmpty(labelText))
            {
                throw new ArgumentNullException();
            }

            var tagBuilder = new TagBuilder("label");
            tagBuilder.Attributes.Add("for", htmlFieldName);

            tagBuilder.InnerHtml.Append(customLabelText ?? labelText);


            if (htmlAttributes != null)
            {
                tagBuilder.MergeAttributes(htmlAttributes);
            }

            tagBuilder.AddCssClass("form-label");

            return tagBuilder;
        }

        #endregion





        #region checkbox


        public static IHtmlContent CheckboxWithLabel<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string labelText, IDictionary<string, string> htmlAttributes = null)
        {
            var checkboxInstance = Input(html, expression, InputType.checkbox, htmlAttributes);
            var labelInstance = Label(html, expression, labelText, new Dictionary<string, string>() {
                {"class", "m-0" }
            });

            var content = new HtmlContentBuilder().AppendHtml("<div class='d-flex align-items-center'>").AppendHtml(checkboxInstance).AppendHtml(labelInstance).AppendHtml("</div>");

            return content;
        }

        #endregion


        #region Text area

        public static IHtmlContent TextArea<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, IDictionary<string, string> htmlAttributes = null)
        {
            var id = html.IdFor(expression).ToString();

            var tagBuilder = new TagBuilder("textarea");
            tagBuilder.Attributes.Add("id", id);
            

            if(htmlAttributes != null)
            {
                tagBuilder.MergeAttributes(htmlAttributes);
            }

            tagBuilder.AddCssClass("form-control");
            return tagBuilder;
        }

        public static IHtmlContent TextAreaWithLabel<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            var labelInstance = Label(html, expression);
            var textAreaInstance = TextArea(html, expression);

            var content = new HtmlContentBuilder()
                .AppendHtml("<div>")
                 .AppendHtml(labelInstance)
                 .AppendHtml(textAreaInstance)
                 .AppendHtml("</div>");

            return content;
        }

        #endregion

        private static string GetPropertyName<TModel, TProperty>(Expression<Func<TModel, TProperty>> expression)
        {
            if (expression.Body is MemberExpression memberExpression)
            {
                return memberExpression.Member.Name;
            }
            throw new InvalidOperationException("Expression is not a member access");
        }
    }

    
}


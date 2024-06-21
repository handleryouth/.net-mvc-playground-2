using System.Linq.Expressions;
using contactForm.Enum;
using contactForm.Models.CommonModel;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

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
            if (htmlAttributes != null && htmlAttributes.ContainsKey("id") == false)
            {
                builder.Attributes.Add("id", htmlFieldName);
            }
            
            builder.Attributes.Add("name", htmlFieldName);
            builder.TagRenderMode = TagRenderMode.SelfClosing;

           
            if(htmlAttributes != null)
            {
                builder.MergeAttributes(htmlAttributes);
            }

            
            if (inputType == "checkbox" || inputType == "radio")
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
            if (htmlAttributes != null && htmlAttributes.ContainsKey("for") == false)
            {
                tagBuilder.Attributes.Add("for", htmlFieldName);
            }

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


        public static IHtmlContent CheckboxWithLabel<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, string labelText, IDictionary<string, string> inputHtmlAttributes = null, string containerClass = "" )
        {
            var checkboxInstance = Input(html, expression, InputType.checkbox, inputHtmlAttributes);
            var labelInstance = Label(html, expression, labelText, new Dictionary<string, string>() {
                {"class", "m-0" }
            });

            string mergedContainerClass = $"d-flex align-items-center gap-2 {containerClass}";

            var content = new HtmlContentBuilder().AppendHtml($"<div class='{mergedContainerClass}'>").AppendHtml(checkboxInstance).AppendHtml(labelInstance).AppendHtml("</div>");

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

        #region Button
        public static IHtmlContent Button(this IHtmlHelper html, string buttonText, IDictionary<string, string> htmlAttributes)
        {
            var tagBuilder = new TagBuilder("button");
            
            if (htmlAttributes != null)
            {
                tagBuilder.MergeAttributes(htmlAttributes);
            }

            tagBuilder.AddCssClass("btn");

            tagBuilder.InnerHtml.Append(buttonText);

            return tagBuilder;   
        }
        #endregion

        #region Radio Button

        public static IHtmlContent RadioBoxWithLabel<TModel, TProperty, K>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, List<RadioPropertiesModel<K>> listValue, IDictionary<string, string> inputHtmlAttributes = null, string radioButtonContainerClass = "", string containerClass = "")
        {
            if(listValue.Count == 0)
            {
                throw new ArgumentException("At least one value provided!");
            }

            string mergeContainerClass = $"d-flex gap-2 align-items-center {containerClass}";
            string mergedRadioButtonContainerClass = $"d-flex align-items-center gap-2 {radioButtonContainerClass}";

            var metadata = html.MetadataProvider.GetMetadataForProperty(typeof(TModel), GetPropertyName(expression));
            var htmlFieldName = html.IdFor(expression).ToString();
            var labelText = metadata.DisplayName ?? metadata.PropertyName ?? htmlFieldName.Split('.').Last();

            var modelExpressionProvider = new ModelExpressionProvider(metadata);
            var modelExpression = modelExpressionProvider.CreateModelExpression(html.ViewData, expression);
            var modelValue = modelExpression.Model?.ToString();

            var content = new HtmlContentBuilder();
            var parentLabelInstance = Label(html, expression, customLabelText: labelText);
            content.AppendHtml(parentLabelInstance);
            content.AppendHtml($"<div class='{mergeContainerClass}'>"); 

            foreach (var radioVariable in listValue)
            {

                IDictionary<string, string> initialHtmlAttributes = inputHtmlAttributes ?? new Dictionary<string, string>();
                if (initialHtmlAttributes.ContainsKey("class") && initialHtmlAttributes["class"] != null)
                {
                    initialHtmlAttributes.Add("class", $"mt-0 {initialHtmlAttributes["class"]}");
                } else
                {
                    initialHtmlAttributes.Add("class", "mt-0");
                }

                initialHtmlAttributes.Add("value", radioVariable.value.ToString());

                initialHtmlAttributes.Add("id", radioVariable.id);

                if(modelValue != null && modelValue == radioVariable.value.ToString())
                {
                    initialHtmlAttributes.Add("checked", null);
                }

                var radioButtonInstance = Input(html, expression, InputType.radiobox, initialHtmlAttributes);
                var labelInstance = Label(html, expression, radioVariable.text, new Dictionary<string, string>() {
                {"class", "m-0" },
                {"for", radioVariable.id }
            });
                content.AppendHtml($"<div class='{mergedRadioButtonContainerClass}'>");
                content.AppendHtml(radioButtonInstance);
                content.AppendHtml(labelInstance);
                content.AppendHtml("</div>");
            }
            content.AppendHtml("</div>");

            return content;
        }

        #endregion


        #region
        public static IHtmlContent CustomErrorMessage<TModel, TProperty>(this IHtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            var tagBuilder = new TagBuilder("p");
            tagBuilder.AddCssClass("invalid-feedback d-block");

            var expressionProvider = new ModelExpressionProvider(html.MetadataProvider);
            var modelExpression = expressionProvider.CreateModelExpression(html.ViewData, expression);
            var fullHtmlFieldName = modelExpression.Name;

            if (!html.ViewData.ModelState.ContainsKey(fullHtmlFieldName))
            {
                return HtmlString.Empty;
            }

            var modelState = html.ViewData.ModelState[fullHtmlFieldName];
            if (modelState == null || modelState.Errors.Count == 0)
            {
                return HtmlString.Empty;
            }

            var error = modelState.Errors.First().ErrorMessage;

            tagBuilder.InnerHtml.Append(error);

            return tagBuilder;
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


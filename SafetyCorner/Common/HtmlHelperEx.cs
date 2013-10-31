using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace SafetyCorner.Common
{
    public static class HtmlHelperEx
    {
        public static MvcHtmlString FileUpload(this HtmlHelper htmlHelper, string id, IDictionary<string, object> htmlAttributes)
        {
            if (string.IsNullOrEmpty(id))
            {
                return MvcHtmlString.Create("請設定ID");
            }
            string htmlFileUpload = GetFileUploadHtmlTag(id, htmlAttributes);

            TagBuilder scriptTag = new TagBuilder("script");
            string scriptContent = GetFileUploadScript();
            scriptContent = scriptContent.Replace("$id", id).Replace("{0}", string.Empty);
            scriptTag.InnerHtml = scriptContent;
            string script = scriptTag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(htmlFileUpload + script);
        }

        public static MvcHtmlString FileUpload(this HtmlHelper htmlHelper, string id, IDictionary<string, object> htmlAttributes, string actionName, string controllerName)
        {
            if (string.IsNullOrEmpty(id))
            {
                return MvcHtmlString.Create("請設定ID");
            }
            if (string.IsNullOrEmpty(actionName))
            {
                return MvcHtmlString.Create("請設定Action Name");
            }
            if (string.IsNullOrEmpty(controllerName))
            {
                return MvcHtmlString.Create("請設定Controller Name");
            }

            string htmlFileUpload = GetFileUploadHtmlTag(id, htmlAttributes);

            TagBuilder ajaxCallBack = new TagBuilder("div");
            ajaxCallBack.MergeAttribute("style", "display:inline;");
            ajaxCallBack.MergeAttribute("id", id + "_callback");

            htmlFileUpload += ajaxCallBack.ToString(TagRenderMode.Normal);

            TagBuilder scriptTag = new TagBuilder("script");
            string ajaxScript = @"var files = $('#$id').get(0).files;
            if (files.length > 0) {
                var data = new FormData();
                data.append('file', files[0]);
                data.append('fileId', '$id');
                $.ajax({
                    type: 'POST',
                    url: '/{0}/{1}',
                    contentType: false,
                    processData: false,
                    data: data
                }).done(function (data) {
                    $('#$id_callback').text(data);
                });
            }";
            ajaxScript = ajaxScript.Replace("{0}", controllerName).Replace("{1}", actionName);

            string scriptContent = GetFileUploadScript();

            scriptContent = scriptContent
                .Replace("{0}", ajaxScript).Replace("$id", id);
            scriptTag.InnerHtml = scriptContent;
            string script = scriptTag.ToString(TagRenderMode.Normal);
            return MvcHtmlString.Create(htmlFileUpload + script);
        }


        private static string GetFileUploadScript()
        {
            string scriptContent = @"var wrapper = $('<div/>').css({ height: 0, width: 0, 'overflow': 'hidden', 'display': 'none' });
                                    var fileInput_$id = $('#$id').wrap(wrapper);
                                    $('#$id_img').click(function () {
                                        fileInput_$id.click();
                                    }).show();
                                    fileInput_$id.change(function () {
                                        if ($(this).val() != '') {
                                            $this = $(this).val().replace(/C:\\fakepath\\/i, '');
                                            $('#$id_text').val($this);
                                            {0}
                                        }
                                    });";
            return scriptContent;
        }

        private static string GetFileUploadHtmlTag(string id, IDictionary<string, object> htmlAttributes)
        {
            TagBuilder fileTag = new TagBuilder("input");
            fileTag.MergeAttribute("type", "file");
            fileTag.MergeAttribute("id", id);
            fileTag.MergeAttribute("name", id);
            TagBuilder inputTag = new TagBuilder("input");
            inputTag.MergeAttribute("id", id + "_text");
            if (htmlAttributes != null)
            {
                inputTag.MergeAttributes(htmlAttributes);
            }
            inputTag.MergeAttribute("readonly", "");
            inputTag.MergeAttribute("style", "margin-right:5px");
            TagBuilder aTag = new TagBuilder("a");
            aTag.AddCssClass("btnFolder");
            aTag.MergeAttribute("id", id + "_img");

            string htmlFileUpload = "<div style='display:inline;'>" + fileTag.ToString(TagRenderMode.Normal) + inputTag.ToString(TagRenderMode.Normal) + aTag.ToString(TagRenderMode.Normal) + "</div>";
            return htmlFileUpload;
        }
    }
}
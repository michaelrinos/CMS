using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using SportsStore.Models.ViewModels;
using System.Collections.Generic;

namespace TutorCom.LEO.Infrastructure.TagHelpers {

	/// <summary>
	/// Used to generage pagination links given the right params
	/// </summary>
	[HtmlTargetElement("div", Attributes = "page-model")]
	public class PageLinkTagHelper : TagHelper {
		private IUrlHelperFactory urlHelperFactory;

		public PageLinkTagHelper(IUrlHelperFactory helperFactory)
		{
			urlHelperFactory = helperFactory;
		}

		[ViewContext]
		[HtmlAttributeNotBound]
		public ViewContext ViewContext { get; set; }

		public PagingInfo PageModel { get; set; }

		public string PageAction { get; set; }

		[HtmlAttributeName(DictionaryAttributePrefix = "page-url-")]
		public Dictionary<string, object> PageUrlValues { get; set; }
			= new Dictionary<string, object>();

		public bool PageClassesEnabled { get; set; } = false;
		public string PageClass { get; set; }
		public string PageClassNormal { get; set; }
		public string PageClassSelected { get; set; }

		public override void Process(TagHelperContext context,
				TagHelperOutput output)
		{
			IUrlHelper urlHelper = urlHelperFactory.GetUrlHelper(ViewContext);
			TagBuilder result = new TagBuilder("div");
			if (PageModel.TotalPages > 1)
			{

				if (PageModel.CurrentPage != 1)
				{
					TagBuilder first = new TagBuilder("a");
					PageUrlValues["Page"] = 1;
					first.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
					first.AddCssClass(PageClass);
					var fal = new TagBuilder("i");
					fal.AddCssClass("fas fa-angle-double-left");
					first.InnerHtml.AppendHtml(fal);
					result.InnerHtml.AppendHtml(first);
				}

				for (int i = (PageModel.CurrentPage > 2 ? PageModel.CurrentPage - 2 : 1); i <= (PageModel.TotalPages - PageModel.CurrentPage > 2 ? PageModel.CurrentPage + 2 : PageModel.TotalPages); i++)
				{
					TagBuilder tag = new TagBuilder("a");
					PageUrlValues["Page"] = i;
					tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
					if (PageClassesEnabled)
					{
						tag.AddCssClass(PageClass);
						tag.AddCssClass(i == PageModel.CurrentPage
							? PageClassSelected : PageClassNormal);
					}
					tag.InnerHtml.Append(i.ToString());
					result.InnerHtml.AppendHtml(tag);
				}
				/*

				for (int i = 1; i <= PageModel.TotalPages; i++)
				{
					TagBuilder tag = new TagBuilder("a");
					PageUrlValues["Page"] = i;
					tag.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
					if (PageClassesEnabled)
					{
						tag.AddCssClass(PageClass);
						tag.AddCssClass(i == PageModel.CurrentPage
							? PageClassSelected : PageClassNormal);
					}
					tag.InnerHtml.Append(i.ToString());
					result.InnerHtml.AppendHtml(tag);
				}
				-- */
				if (PageModel.TotalPages - PageModel.CurrentPage > 2)
				{
					TagBuilder elipse = new TagBuilder("a");
					PageUrlValues["Page"] = PageModel.TotalPages;
					elipse.Attributes["style"] = "pointer-events:none";
					elipse.AddCssClass(PageClass);
					elipse.InnerHtml.AppendHtml("...");

					result.InnerHtml.AppendHtml(elipse);

				}

				if (PageModel.CurrentPage != PageModel.TotalPages)
				{
					TagBuilder last = new TagBuilder("a");
					PageUrlValues["Page"] = PageModel.TotalPages;
					last.Attributes["href"] = urlHelper.Action(PageAction, PageUrlValues);
					last.AddCssClass(PageClass);

					var far = new TagBuilder("i");
					far.AddCssClass("fas fa-angle-double-right");
					last.InnerHtml.AppendHtml(far);

					result.InnerHtml.AppendHtml(last);
				}
			}

			output.Content.AppendHtml(result.InnerHtml);
		}
	}
}

using HtmlAgilityPack;
using InizioTest.Models;

namespace InizioTest.Services
{
    public class HtmlParser
    {
        public SearchResult ParseHtmlDocument(HtmlDocument htmlDocument)
        {
            string path = @"wwwroot/webOffline/josef hledík - Hledat Googlem.html"; //z webu dostávám pouze stránky o cookies.
                                                                                    //Nevím jak je obejít. Pro test jsem si uložil dvě stránky do wwwroot ručně.
            HtmlDocument document = new();
            document.Load(path); /*document = htmlDocument;*/             //....viz poznámka výše

            SearchResult searchResult = new SearchResult();

            var nodes = document.DocumentNode.SelectNodes("//a[@jsname]");

            if (nodes != null)
            {
                foreach (var node in nodes)
                {
                    string attributeValue = node.GetAttributeValue("jsname", "");
                    if (attributeValue == "UWckNb")
                    {
                        HtmlAttribute attHref = node.Attributes["href"];
                        var h3 = node.SelectSingleNode(".//h3");
                        Item item = new();
                        item.Title = h3.InnerText.Trim();
                        item.Link = attHref.Value;

                        searchResult.Items.Add(item);

                    }
                }
            }

            return searchResult;
        }
    }
}

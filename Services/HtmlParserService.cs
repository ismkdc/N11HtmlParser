using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using HtmlAgilityPack;
using n11api.Models;


namespace n11api.Services
{
    public class HtmlParserService
    {
        public IEnumerable<Product> GetProducts(string url, int page)
        {
            url = $"{url}?pg={page}";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var productsToParse = doc.DocumentNode.SelectNodes("//section//div[contains(@class, 'columnContent')]");
            var products = new List<Product>();

            foreach (HtmlNode item in productsToParse)
            {
                var product = new Product
                {
                    Name = HtmlEntity.DeEntitize(item.SelectSingleNode(".//h3[contains(@class, 'productName')]").InnerText.Trim()),
                    Url = HtmlEntity.DeEntitize(item.SelectSingleNode(".//a[contains(@class, 'plink')]").Attributes["href"].Value),
                    Id = HtmlEntity.DeEntitize(item.SelectSingleNode(".//a[contains(@class, 'plink')]").Attributes["data-id"].Value),
                    Price = HtmlEntity.DeEntitize(item.SelectSingleNode(".//ins/text()").InnerText.Trim()),
                };

                products.Add(product);
            }

            return products;
        }

        public ProductDetail GetProductDetail(string idOrUrl)
        {
            var url = idOrUrl.Contains("-") ? idOrUrl : $"https://urun.n11.com/x-{idOrUrl}";
            var web = new HtmlWeb();
            var doc = web.Load(url);

            var urlArr = url.Split('-');

            var productDetail = new ProductDetail
            {
                Id = urlArr[urlArr.Length - 1],
                Name = HtmlEntity.DeEntitize(doc.DocumentNode.SelectSingleNode("//h1[contains(@class, 'proName')]").InnerText.Trim()),
                ImgUrl = HtmlEntity.DeEntitize(doc.DocumentNode.SelectSingleNode("//div[contains(@class, 'imgObj')]//img").Attributes["data-original"].Value),
                Price = HtmlEntity.DeEntitize(doc.DocumentNode.SelectSingleNode("//ins").Attributes["content"].Value),
                Url = HtmlEntity.DeEntitize(doc.DocumentNode.SelectSingleNode("//link[contains(@rel, 'canonical')]").Attributes["href"].Value),
            };

            var installmentTable = doc.GetElementbyId("installmentTable");

            int counter = 0;
            foreach (HtmlNode item in installmentTable.SelectNodes("./tr[contains(@class, 'payDetail')]"))
            {
                var prices = item.SelectNodes("./td/div").Where(x => !x.HasAttributes).ToList();
                var installment = item.SelectSingleNode("./td[contains(@class, 'installment')]").InnerText.Trim();

                productDetail.InstallmentPrice["Axess"][counter] = prices.Count > 0 ? $"{prices[0].InnerText} x {installment}" : "";
                productDetail.InstallmentPrice["Bonus"][counter] = prices.Count > 1 ? $"{prices[1].InnerText} x {installment}" : "";
                productDetail.InstallmentPrice["CardFinans"][counter] = prices.Count > 2 ? $"{prices[2].InnerText} x {installment}" : "";
                productDetail.InstallmentPrice["Maximum"][counter] = prices.Count > 3 ? $"{prices[3].InnerText} x {installment}" : "";
                productDetail.InstallmentPrice["Paraf"][counter] = prices.Count > 4 ? $"{prices[4].InnerText} x {installment}" : "";
                productDetail.InstallmentPrice["World"][counter] = prices.Count > 5 ? $"{prices[5].InnerText} x {installment}" : "";

                counter++;
            }


            var dict = new Dictionary<string, object>();
            foreach (HtmlNode item in doc.DocumentNode.SelectNodes("//div[contains(@class, 'feaItem')]"))
            {
                var label = HtmlEntity.DeEntitize(item.SelectSingleNode("./span[contains(@class, 'label')]").InnerText.Trim());
                var dataItem = item.SelectSingleNode("./a[contains(@class, 'data')]/span") ??  item.SelectSingleNode("./span[contains(@class, 'data')]");
                var data = HtmlEntity.DeEntitize(dataItem.InnerText.Trim());
                dict.Add(label, data);
            }

            productDetail.Detail = dict.Aggregate(new ExpandoObject() as IDictionary<string, Object>,
                            (a, p) => { a.Add(p.Key, p.Value); return a; });

            return productDetail;
        }
    }
}

using System;
using System.Collections.Generic;

namespace n11api.Models
{
    public class ProductDetail
    {
        public string Id { get; set; }
        public string ImgUrl { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public Dictionary<string, string[]> InstallmentPrice { get; set; }
        public string Url { get; set; }       
        public dynamic Detail { get; set; }

        public ProductDetail()
        {
            InstallmentPrice = new Dictionary<string, string[]>();
            InstallmentPrice.Add("Axess", new string[4]);
            InstallmentPrice.Add("Bonus", new string[4]);
            InstallmentPrice.Add("CardFinans", new string[4]);
            InstallmentPrice.Add("Maximum", new string[4]);
            InstallmentPrice.Add("Paraf", new string[4]);
            InstallmentPrice.Add("World", new string[4]);
        }
    }
}

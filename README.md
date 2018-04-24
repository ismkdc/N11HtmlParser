# N11HtmlParser
This api have 2 method

## 1-)GetProducts by category url.

Usage: ``` http://localhost:60885/api/getproducts?url={categoryurl}&page={page} ```

Example: ``` http://localhost:60885/api/getproducts?url=https://www.n11.com/bilgisayar/dizustu-bilgisayar&page=1 ```

Result:
```
[
  {
    "id": "P208558443",
    "name": "Hp 250 G6 2XY72ES 15.6\" i5-7200U 8GB 256GB SSD 2GB R5 M330 FDOS",
    "price": "2.349,99",
    "url": "https://urun.n11.com/dizustu-bilgisayar/hp-250-g6-2xy72es-156-i5-7200u-8gb-256gb-ssd-2gb-r5-m330-fdos-P208558443"
  },
  {
    "id": "<ürün id>",
    ...
  }
]
```

## 2-)GetProductDetail by url or id

Usage: ``` http://localhost:60885/api/getproductdetail?idorurl={product id or url} ```

Example: ``` http://localhost:60885/api/getproductdetail?idorurl=https://urun.n11.com/dizustu-bilgisayar/hp-250-g6-2xy72es-156-i5-7200u-8gb-256gb-ssd-2gb-r5-m330-fdos-P208558443 ```

or
``` http://localhost:60885/api/getproductdetail?idorurl=P208558443 ```

Result: 

```
{
  "id": "P208558443",
  "imgUrl": "https://n11scdn1.akamaized.net/a1/1024/elektronik/dizustu-bilgisayar/hp-i5-7200u-256-gb-ssd-8gb-ram-2-gb-vga-156__0316532975999921.jpg",
  "name": "Hp 250 G6 2XY72ES 15.6\" i5-7200U 8GB 256GB SSD 2GB R5 M330 FDOS",
  "price": "2349.99",
  "installmentPrice": {
    "Axess": [ "783,33 TL x 3", "391,67 TL x 6", "2.451,23 TL x 9", "2.497,33 TL x 12" ],
    "Bonus": [ ... ],
    "CardFinans": [ ... ],
    ...
  },
  "url": "https://urun.n11.com/dizustu-bilgisayar/hp-250-g6-2xy72es-156-i5-7200u-8gb-256gb-ssd-2gb-r5-m330-fdos-P208558443",
  "detail": {
      "Marka" : "HP",
      "SSD": "256 Gb",
      "Sistem Belleği": "8 Gb",
      "İşlemci": "Intel Core İ5",
      "Ekran Boyutu": "15.6'' İnç",
      "Usb 3,0 Desteği": "Var",
      "Disk Kapasitesi": "256 Gb",
      "Ekran Kartı Belleği Paylaşımsız": "(2 Gb)",
      "İşletim Sistemi": "Free Dos"
  }
}
```

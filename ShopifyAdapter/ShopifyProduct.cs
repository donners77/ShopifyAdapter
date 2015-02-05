﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyAdapter
{
    public class Product
    {
        public string body_html { get; set; }
        public string created_at { get; set; }
        public string handle { get; set; }
        public int id { get; set; }
        public string product_type { get; set; }
        public string published_at { get; set; }
        public string published_scope { get; set; }
        public object template_suffix { get; set; }
        public string title { get; set; }
        public string updated_at { get; set; }
        public string vendor { get; set; }
        public string tags { get; set; }
        public List<Variant> variants { get; set; }
        public List<Option> options { get; set; }
        public List<object> images { get; set; }

        public Product() { }
    }

    public class Variant
    {
        public string barcode { get; set; }
        public string compare_at_price { get; set; }
        public string created_at { get; set; }
        public string fulfillment_service { get; set; }
        public int grams { get; set; }
        public int id { get; set; }
        public object inventory_management { get; set; }
        public string inventory_policy { get; set; }
        public string option1 { get; set; }
        public object option2 { get; set; }
        public object option3 { get; set; }
        public int position { get; set; }
        public string price { get; set; }
        public int product_id { get; set; }
        public bool requires_shipping { get; set; }
        public string sku { get; set; }
        public bool taxable { get; set; }
        public string title { get; set; }
        public string updated_at { get; set; }
        public int inventory_quantity { get; set; }
        public int old_inventory_quantity { get; set; }
        public object image_id { get; set; }
    }

    public class Option
    {
        public int id { get; set; }
        public string name { get; set; }
        public int position { get; set; }
        public int product_id { get; set; }
    }

    public class Products
    {
        public List<Product> products { get; set; }
    }

    public class RootProduct
    {
        public Product product { get; set; }
    }

}

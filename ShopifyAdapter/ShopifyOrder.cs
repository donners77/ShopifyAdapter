using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopifyAdapter
{
    public class ShopifyOrder
    {
        public bool buyer_accepts_marketing { get; set; }
        public object cancel_reason { get; set; }
        public object cancelled_at { get; set; }
        public string cart_token { get; set; }
        public object checkout_token { get; set; }
        public object closed_at { get; set; }
        public bool confirmed { get; set; }
        public string created_at { get; set; }
        public string currency { get; set; }
        public object device_id { get; set; }
        public string email { get; set; }
        public string financial_status { get; set; }
        public object fulfillment_status { get; set; }
        public string gateway { get; set; }
        public int id { get; set; }
        public string landing_site { get; set; }
        public object location_id { get; set; }
        public string name { get; set; }
        public object note { get; set; }
        public int number { get; set; }
        public string processed_at { get; set; }
        public string reference { get; set; }
        public string referring_site { get; set; }
        public string source_identifier { get; set; }
        public string source_name { get; set; }
        public object source_url { get; set; }
        public string subtotal_price { get; set; }
        public bool taxes_included { get; set; }
        public bool test { get; set; }
        public string token { get; set; }
        public string total_discounts { get; set; }
        public string total_line_items_price { get; set; }
        public string total_price { get; set; }
        public string total_price_usd { get; set; }
        public string total_tax { get; set; }
        public int total_weight { get; set; }
        public string updated_at { get; set; }
        public object user_id { get; set; }
        public object browser_ip { get; set; }
        public string landing_site_ref { get; set; }
        public int order_number { get; set; }
        public List<DiscountCode> discount_codes { get; set; }
        public List<NoteAttribute> note_attributes { get; set; }
        public string processing_method { get; set; }
        public string source { get; set; }
        public int checkout_id { get; set; }
        public List<TaxLine> tax_lines { get; set; }
        public string tags { get; set; }
        public List<LineItem> line_items { get; set; }
        public List<ShippingLine> shipping_lines { get; set; }
        public BillingAddress billing_address { get; set; }
        public ShippingAddress shipping_address { get; set; }
        public List<Fulfillment> fulfillments { get; set; }
        public ClientDetails client_details { get; set; }
        public List<Refund> refunds { get; set; }
        public PaymentDetails payment_details { get; set; }
        public Customer customer { get; set; }

        public ShopifyOrder() { }
    }

    public class DiscountCode
    {
        public string code { get; set; }
        public string amount { get; set; }
        public string type { get; set; }
    }

    public class NoteAttribute
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class TaxLine
    {
        public string price { get; set; }
        public double rate { get; set; }
        public string title { get; set; }
    }

    public class LineItem
    {
        public string fulfillment_service { get; set; }
        public object fulfillment_status { get; set; }
        public bool gift_card { get; set; }
        public int grams { get; set; }
        public int id { get; set; }
        public string price { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public bool requires_shipping { get; set; }
        public string sku { get; set; }
        public bool taxable { get; set; }
        public string title { get; set; }
        public int variant_id { get; set; }
        public string variant_title { get; set; }
        public object vendor { get; set; }
        public string name { get; set; }
        public string variant_inventory_management { get; set; }
        public List<object> properties { get; set; }
        public bool product_exists { get; set; }
        public int fulfillable_quantity { get; set; }
        public List<object> tax_lines { get; set; }
    }

    public class ShippingLine
    {
        public string code { get; set; }
        public string price { get; set; }
        public string source { get; set; }
        public string title { get; set; }
        public List<object> tax_lines { get; set; }
    }

    public class BillingAddress
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public object company { get; set; }
        public string country { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string phone { get; set; }
        public string province { get; set; }
        public string zip { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
        public string province_code { get; set; }
    }

    public class ShippingAddress
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public object company { get; set; }
        public string country { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public double latitude { get; set; }
        public double longitude { get; set; }
        public string phone { get; set; }
        public string province { get; set; }
        public string zip { get; set; }
        public string name { get; set; }
        public string country_code { get; set; }
        public string province_code { get; set; }
    }

    public class Receipt
    {
        public bool testcase { get; set; }
        public string authorization { get; set; }
    }

    public class Property
    {
        public string name { get; set; }
        public string value { get; set; }
    }

    public class LineItem2
    {
        public string fulfillment_service { get; set; }
        public object fulfillment_status { get; set; }
        public bool gift_card { get; set; }
        public int grams { get; set; }
        public int id { get; set; }
        public string price { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public bool requires_shipping { get; set; }
        public string sku { get; set; }
        public bool taxable { get; set; }
        public string title { get; set; }
        public int variant_id { get; set; }
        public string variant_title { get; set; }
        public object vendor { get; set; }
        public string name { get; set; }
        public string variant_inventory_management { get; set; }
        public List<Property> properties { get; set; }
        public bool product_exists { get; set; }
        public int fulfillable_quantity { get; set; }
        public List<object> tax_lines { get; set; }
    }

    public class Fulfillment
    {
        public string created_at { get; set; }
        public int id { get; set; }
        public int order_id { get; set; }
        public string service { get; set; }
        public string status { get; set; }
        public object tracking_company { get; set; }
        public string updated_at { get; set; }
        public string tracking_number { get; set; }
        public List<string> tracking_numbers { get; set; }
        public string tracking_url { get; set; }
        public List<string> tracking_urls { get; set; }
        public Receipt receipt { get; set; }
        public List<LineItem2> line_items { get; set; }
    }

    public class ClientDetails
    {
        public object accept_language { get; set; }
        public object browser_height { get; set; }
        public string browser_ip { get; set; }
        public object browser_width { get; set; }
        public object session_hash { get; set; }
        public object user_agent { get; set; }
    }

    public class LineItem3
    {
        public string fulfillment_service { get; set; }
        public object fulfillment_status { get; set; }
        public bool gift_card { get; set; }
        public int grams { get; set; }
        public int id { get; set; }
        public string price { get; set; }
        public int product_id { get; set; }
        public int quantity { get; set; }
        public bool requires_shipping { get; set; }
        public string sku { get; set; }
        public bool taxable { get; set; }
        public string title { get; set; }
        public int variant_id { get; set; }
        public string variant_title { get; set; }
        public object vendor { get; set; }
        public string name { get; set; }
        public string variant_inventory_management { get; set; }
        public List<object> properties { get; set; }
        public bool product_exists { get; set; }
        public int fulfillable_quantity { get; set; }
        public List<object> tax_lines { get; set; }
    }

    public class RefundLineItem
    {
        public int id { get; set; }
        public int line_item_id { get; set; }
        public int quantity { get; set; }
        public LineItem3 line_item { get; set; }
    }

    public class Receipt2
    {
    }

    public class Transaction
    {
        public string amount { get; set; }
        public string authorization { get; set; }
        public string created_at { get; set; }
        public string currency { get; set; }
        public string gateway { get; set; }
        public int id { get; set; }
        public string kind { get; set; }
        public object location_id { get; set; }
        public object message { get; set; }
        public int order_id { get; set; }
        public object parent_id { get; set; }
        public string source_name { get; set; }
        public string status { get; set; }
        public bool test { get; set; }
        public object user_id { get; set; }
        public object device_id { get; set; }
        public Receipt2 receipt { get; set; }
        public object error_code { get; set; }
    }

    public class Refund
    {
        public string created_at { get; set; }
        public int id { get; set; }
        public string note { get; set; }
        public int order_id { get; set; }
        public bool restock { get; set; }
        public int user_id { get; set; }
        public List<RefundLineItem> refund_line_items { get; set; }
        public List<Transaction> transactions { get; set; }
    }

    public class PaymentDetails
    {
        public object avs_result_code { get; set; }
        public object credit_card_bin { get; set; }
        public object cvv_result_code { get; set; }
        public string credit_card_number { get; set; }
        public string credit_card_company { get; set; }
    }

    public class DefaultAddress
    {
        public string address1 { get; set; }
        public string address2 { get; set; }
        public string city { get; set; }
        public object company { get; set; }
        public string country { get; set; }
        public object first_name { get; set; }
        public int id { get; set; }
        public object last_name { get; set; }
    }

    public class Customer
    {
        public bool accepts_marketing { get; set; }
        public string created_at { get; set; }
        public string email { get; set; }
        public string first_name { get; set; }
        public int id { get; set; }
        public string last_name { get; set; }
        // bombed out due to null value
        //public int last_order_id { get; set; }
        public object multipass_identifier { get; set; }
        public object note { get; set; }
        public int orders_count { get; set; }
        public string state { get; set; }
        public string total_spent { get; set; }
        public string updated_at { get; set; }
        public bool verified_email { get; set; }
        public string tags { get; set; }
        public string last_order_name { get; set; }
        public DefaultAddress default_address { get; set; }
    }
    
    public class Orders
    {
        public List<ShopifyOrder> orders { get; set; }
    }

    public class RootOrder
    {
        public ShopifyOrder order { get; set; }
    }

}

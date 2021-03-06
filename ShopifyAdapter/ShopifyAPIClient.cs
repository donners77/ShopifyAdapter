﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using Newtonsoft.Json;

namespace ShopifyAdapter
{
    public class ShopifyAPIClient
    {
        /*
        private static readonly string Shopify_StoreName = "rame-3";
        private static readonly string Shopify_APIKey = "bba2c431dcb900b527232f01251ae90b";
        private static readonly string Shopify_Password = "4ed6e81b690fe957ba286ae28cc63718";
        */

        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="StoreName">Shopify Store name ([storename].myshopify.com)</param>
        /// <param name="APIKey">unique API Key from Shopify</param>
        /// <param name="Password">Password that authorizing private app to access the store admin.</param>
        public ShopifyAPIClient(string StoreName, string APIKey, string Password)
        {
            this._storename = StoreName;
            this._apikey = APIKey;
            this._password = Password;
        }
        /// <summary>
        /// Get a specific shop product by Product ID
        /// </summary>
        /// <param name="ProductID">Store product_id</param>
        /// <returns></returns>
        public Product GetProduct(string ProductID)
        {
            Product p = new Product();
            string endpointURI = String.Format("admin/products/{0}.json", ProductID);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Format("https://{0}.myshopify.com/", _storename));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes(_apikey + ":" + _password)));

                HttpResponseMessage response = client.GetAsync(endpointURI).Result;
                if (response.IsSuccessStatusCode)
                {
                    var rp = Newtonsoft.Json.JsonConvert.DeserializeObject<RootProduct>(response.Content.ReadAsStringAsync().Result);
                    p = rp.product;
                }
                else
                {
                    p.id = (int)response.StatusCode;
                    p.title = response.ReasonPhrase;
                }
            }
            return p;
        }
        /// <summary>
        /// Get all store products
        /// </summary>
        /// <returns></returns>
        public List<Product> GetProducts()
        {
            List<Product> allProducts = new List<Product>();
            string endpointURI = "admin/products.json";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Format("https://{0}.myshopify.com/", _storename));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes(_apikey+ ":" + _password)));

                HttpResponseMessage response = client.GetAsync(endpointURI).Result;
                if (response.IsSuccessStatusCode)
                {
                    Products o = Newtonsoft.Json.JsonConvert.DeserializeObject<Products>(response.Content.ReadAsStringAsync().Result);
                    allProducts = o.products;
                }
                else
                {
                    Product errStatus = new Product();
                    errStatus.id = (int)response.StatusCode;
                    errStatus.title = response.ReasonPhrase;
                    allProducts.Add(errStatus);
                }
            }

            return allProducts;
        }
        /// <summary>
        /// Get all orders
        /// </summary>
        /// <returns></returns>
        public List<ShopifyOrder> GetOrders()
        {
            List<ShopifyOrder> allOrders = new List<ShopifyOrder>();
            string endpointURI = "admin/orders.json";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Format("https://{0}.myshopify.com/", _storename));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes(_apikey + ":" + _password)));

                HttpResponseMessage response = client.GetAsync(endpointURI).Result;
                if (response.IsSuccessStatusCode)
                {
                    Orders o = Newtonsoft.Json.JsonConvert.DeserializeObject<Orders>(response.Content.ReadAsStringAsync().Result);
                    allOrders = o.orders;
                }
                else
                {
                    ShopifyOrder errStatus = new ShopifyOrder();
                    errStatus.id = (int)response.StatusCode;
                    errStatus.name = response.ReasonPhrase;
                    allOrders.Add(errStatus);
                }
            }

            return allOrders;
        }
        /// <summary>
        /// Add a new product to Shopify store
        /// </summary>
        /// <param name="ShopifyProduct"></param>
        /// <returns>the new ProductID</returns>
        public int AddProduct(Product ShopifyProduct)
        {
            int productID = 000;
            string endpointURI = "admin/products.json";

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Format("https://{0}.myshopify.com/", _storename));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes(_apikey + ":" + _password)));

                Dictionary<string, string> dictProduct = new Dictionary<string, string>();
                dictProduct.Add("title", ShopifyProduct.title);
                dictProduct.Add("body_html", ShopifyProduct.body_html);
                dictProduct.Add("vendor", ShopifyProduct.vendor);
                dictProduct.Add("product_type", ShopifyProduct.product_type);
                dictProduct.Add("published", "true");

                StringContent p = new StringContent("{\"product\": " + JsonConvert.SerializeObject(dictProduct) + "}", Encoding.UTF8, "application/json");
                //RootProduct rp = new RootProduct();
                //rp.product = ShopifyProduct;
                //StringContent p = new StringContent(JsonConvert.SerializeObject(rp), Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PostAsJsonAsync(endpointURI, p).Result;
                if (response.IsSuccessStatusCode)
                {
                    Product newProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(response.Content.ReadAsStringAsync().Result);
                    productID = newProduct.id;
                }
                else
                    productID = (int)response.StatusCode;
            }
            return productID;
        }
        /// <summary>
        /// Update product
        /// </summary>
        /// <param name="ShopifyProduct"></param>
        /// <returns></returns>
        public int UpdateProduct(Product ShopifyProduct)
        {
            int productID = 0000;
            string endpointURI = String.Format("admin/products/{0}.json", ShopifyProduct.id);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Format("https://{0}.myshopify.com/", _storename));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes(_apikey + ":" + _password)));


                //RootProduct rp = new RootProduct();
                //rp.product = new Product();
                //rp.product.variants = new List<Variant>();
                //Variant v = new Variant();
                //v.inventory_quantity = 37;
                //rp.product.id = ShopifyProduct.id;
                //rp.product.variants.Add(v);

                //string ser = Newtonsoft.Json.JsonConvert.SerializeObject(rp);
                string ser = "{\"product\":{\"id\":412073932,\"variants\":[{\"product_id\":412073932,\"inventory_quantity\":37}]}}";
                StringContent jsondata = new StringContent(ser, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsJsonAsync(endpointURI, jsondata).Result;
                if (response.IsSuccessStatusCode)
                {
                    Product newProduct = Newtonsoft.Json.JsonConvert.DeserializeObject<Product>(response.Content.ReadAsStringAsync().Result);
                    productID = newProduct.id;
                }
                else
                    productID = (int)response.StatusCode;
            }
            return productID;
        }
        /// <summary>
        /// Set product variant inventory 
        /// </summary>
        /// <param name="VariantID"></param>
        /// <param name="NewInventory"></param>
        /// <returns></returns>
        public int SetInventory(string VariantID, int NewInventory)
        {
            int success = -1;
            string endpointURI = String.Format("admin/variants/{0}.json", VariantID);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Format("https://{0}.myshopify.com/", _storename));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes(_apikey + ":" + _password)));

                string ser = "{ \"variant\":{ \"id\": " + VariantID + ", \"inventory_quantity\": " + NewInventory + " } }";
                StringContent jsondata = new StringContent(ser, Encoding.UTF8, "application/json");

                HttpResponseMessage response = client.PutAsync(endpointURI, jsondata).Result;
                if (response.IsSuccessStatusCode)
                {
                    string resp = response.Content.ReadAsStringAsync().Result;
                    Variant v = JsonConvert.DeserializeObject<Variant>(resp);
                }
                success = (int)response.StatusCode;
            }
            return success;
        }
        
        public int DeleteProduct(string ProductID)
        {
            int success = -1;
            string endpointURI = String.Format("admin/products/{0}.json", ProductID);

            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri(String.Format("https://{0}.myshopify.com/", _storename));
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes(_apikey + ":" + _password)));

                HttpResponseMessage response = client.DeleteAsync(endpointURI).Result;
                success = (int)response.StatusCode;
            }

            return success;
        }


        #region "----- Core WEB API Methods -----------"
        
        //public object CallShopify(HttpMethods Method, string EndPoint, object CallParams)
        //{
        //    HttpResponseMessage response;

        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri(String.Format("https://{0}.myshopify.com/", _storename));
        //        client.DefaultRequestHeaders.Accept.Clear();
        //        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.Default.GetBytes(_apikey + ":" + _password)));

        //        if (CallParams !=null)
        //        {
        //            switch (Method)
        //            {
        //                case HttpMethods.GET:
        //                    response = client.GetAsync(EndPoint).Result;
        //                    break;
        //                case HttpMethods.DELETE:
        //                    response = client.DeleteAsync(EndPoint).Result;
        //                    break;
        //            }
        //        }

        //        if (response.IsSuccessStatusCode)
        //        {
        //            RootObject o = Newtonsoft.Json.JsonConvert.DeserializeObject<RootObject>(response.Content.ReadAsStringAsync().Result);
        //        }
        //        else
        //        {
        //            ShopifyProduct errStatus = new ShopifyProduct((int)response.StatusCode, response.StatusCode.ToString(), response.ReasonPhrase);
        //        }
        //    }
        //}

        /// <summary>
        /// Make an HTTP Request to the Shopify API
        /// </summary>
        /// <param name="method">method to be used in the request</param>
        /// <param name="path">the path that should be requested</param>
        /// <seealso cref="http://api.shopify.com/"/>
        /// <returns>the server response</returns>
        public object Call(HttpMethods method, string path)
        {
            return Call(method, path, null);
        }

        /// <summary>
        /// Make an HTTP Request to the Shopify API
        /// </summary>
        /// <param name="method">method to be used in the request</param>
        /// <param name="path">the path that should be requested</param>
        /// <param name="callParams">any parameters needed or expected by the API</param>
        /// <seealso cref="http://api.shopify.com/"/>
        /// <returns>the server response</returns>
        public object Call(HttpMethods method, string path, object callParams)
        {
            string url = String.Format("https://{0}.myshopify.com/{1}", _storename, path);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "application/json";
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(_apikey + ":" + _password));
            //request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.Default.GetBytes(apiKey + ":" + password)));
            request.Method = method.ToString();
            if (callParams != null)
            {
                if (method == HttpMethods.GET || method == HttpMethods.DELETE)
                {
                    // if no translator assume data is a query string
                    url = String.Format("{0}?{1}", url, callParams.ToString());

                    //// put params into query string
                    //StringBuilder queryString = new StringBuilder();
                    //foreach (string key in callParams.Keys)
                    //{
                    //    queryString.AppendFormat("{0}={1}", HttpUtility.UrlEncode(key), HttpUtility.UrlEncode(callParams[key]));
                    //}
                }
                else if (method == HttpMethods.POST || method == HttpMethods.PUT)
                {
                    string requestBody;
                    requestBody = JsonConvert.SerializeObject(callParams);
                    
                    //add the request body to the request stream
                    if (!String.IsNullOrEmpty(requestBody))
                    {
                        using (var ms = new MemoryStream())
                        {
                            using (var writer = new StreamWriter(request.GetRequestStream()))
                            {
                                writer.Write(requestBody);
                                writer.Close();
                            }
                        }
                    }
                }
            }

            var response = (HttpWebResponse)request.GetResponse();
            string result = null;

            using (Stream stream = response.GetResponseStream())
            {
                StreamReader sr = new StreamReader(stream);
                result = sr.ReadToEnd();
                sr.Close();
            }

            //At least one endpoint will return an empty string, that we need to account for.
            if (string.IsNullOrWhiteSpace(result))
                return null;

            return result;
        }

        /// <summary>
        /// Make a Get method HTTP request to the Shopify API
        /// </summary>
        /// <param name="path">the path where the API call will be made.</param>
        /// <seealso cref="http://api.shopify.com/"/>
        /// <returns>the server response</returns>
        public object Get(string path)
        {
            return Get(path, null);
        }

        /// <summary>
        /// Make a Get method HTTP request to the Shopify API
        /// </summary>
        /// <param name="path">the path where the API call will be made.</param>
        /// <param name="callParams">the querystring params</param>
        /// <seealso cref="http://api.shopify.com/"/>
        /// <returns>the server response</returns>
        public object Get(string path, NameValueCollection callParams)
        {
            return Call(HttpMethods.GET, path, callParams);
        }

        /// <summary>
        /// Make a Post method HTTP request to the Shopify API
        /// </summary>
        /// <param name="path">the path where the API call will be made.</param>
        /// <param name="data">the data that this path will be expecting</param>
        /// <seealso cref="http://api.shopify.com/"/>
        /// <returns>the server response</returns>
        public object Post(string path, object data)
        {
            return Call(HttpMethods.POST, path, data);
        }

        /// <summary>
        /// Make a Put method HTTP request to the Shopify API
        /// </summary>
        /// <param name="path">the path where the API call will be made.</param>
        /// <param name="data">the data that this path will be expecting</param>
        /// <seealso cref="http://api.shopify.com/"/>
        /// <returns>the server response</returns>
        public object Put(string path, object data)
        {
            return Call(HttpMethods.PUT, path, data);
        }

        /// <summary>
        /// Make a Delete method HTTP request to the Shopify API
        /// </summary>
        /// <param name="path">the path where the API call will be made.</param>
        /// <seealso cref="http://api.shopify.com/"/>
        /// <returns>the server response</returns>
        public object Delete(string path)
        {
            return Call(HttpMethods.DELETE, path);
        }

        #endregion

        private string _storename { get; set; }
        private string _apikey { get; set; }
        private string _password { get; set; }
    }

    /// <summary>
    /// The enumeration of HTTP Methods used by the API
    /// </summary>
    public enum HttpMethods
    {
        GET,
        POST,
        PUT,
        DELETE
    }

}

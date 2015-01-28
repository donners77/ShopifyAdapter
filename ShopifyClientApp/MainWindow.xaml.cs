using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ShopifyAdapter;

namespace ShopifyClientApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ShopifyAPIClient client = new ShopifyAPIClient(ConfigurationManager.AppSettings["Shopify_StoreName"],
                ConfigurationManager.AppSettings["Shopify_APIKey"],
                ConfigurationManager.AppSettings["Shopify_Password"]);

        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnGetData_Click(object sender, RoutedEventArgs e)
        {
            switch (cmbCategory.Text.ToLower())
            {
                case "get products":
                    Shop_GetProducts();
                    break;
                case "get orders":
                    Shop_GetOrders();
                    break;
                case "add product":
                    Shop_AddProduct();
                    break;
            }
            
        }

        private void Shop_GetProducts()
        {
            foreach (Product p in client.GetProducts())
            {
                txtResult.Text += String.Format("Product ID: {0} | Product Title: {1}\n", p.id, p.title);
            }
        }

        private void Shop_GetOrders()
        {
            foreach(ShopifyOrder o in client.GetOrders())
            {
                txtResult.Text += String.Format("Order ID: {0} | Created at: {1} | Confirmed: {2}\n", o.id, o.created_at, o.confirmed);
            }
        }

        private void Shop_AddProduct()
        {
            Product p = new Product();
            p.title = "EMC130SGHLSB-34-2-5";
            p.body_html = "<strong>EMC130SGHLSB-34-2-5 Electriscreen Electrimask Vertical Classic</strong>";
            p.vendor = "Stewart Filmscreen";
            p.product_type = "projector screen";
            
            int status = client.AddProduct(p);
            txtResult.Text = status.ToString();
        }
    }
}

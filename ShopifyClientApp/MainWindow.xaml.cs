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
            p.title = "SNDQ110HFHG4NZX";
            p.body_html = "Luxus Deluxe Screen Wall Quicksnap [00705]<br>Image Size: 54in x 96in (110.145in diag.)<br>Aspect Ratio 1.78:1<br>Material: FireHawk® G4 -Front Projection Seamless<br>Screen Material OD: 57.25in x 99.25in<br>Frame Overall Dimension: 60.625in x 102.625in<br>Finish: Velux<br>Mounting: New EZ Mount ";
            p.vendor = "Stewart Filmscreen";
            p.product_type = "projector screen";
            
            int status = client.AddProduct(p);
            txtResult.Text = status.ToString();
        }
    }
}

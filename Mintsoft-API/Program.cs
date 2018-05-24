using System;
using System.Net.Http.Headers;
using System.Text;
using System.Net.Http;
using System.Web;
using System.Net;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Mintsoft.API
{
    class Program
    {
        // Full Swagger - https://api.mintsoft.co.uk/swagger/ui/index
        public static String USERNAME = "test.client.luke";

        public static String PASSWORD = "password";

        static void Main(string[] args)
        {
            Program P = new Program();

            var apiKey = P.GetAPIKey();

            Console.WriteLine("APIKey:" + apiKey);

            var newOrder = new NewOrder
            {
                Address1 = "Address1",
                Address2 = "Address2",
                Address3 = "Address3",
                LastName = "LastName",
                FirstName = "FirstName",
                CompanyName = "CompanyName",
                PostCode = "PO1 1OD",
                Country = "GB",
                CourierService = "DPD Next Day",
                Warehouse = "Main Warehouse",
                Email = "luke@mintsoft.co.uk",
                County = "County",
                OrderNumber = Guid.NewGuid().ToString(),
                OrderItems = new List<NewOrderItem>
                {
                    new NewOrderItem
                    {
                        SKU = "test-sku",
                        Quantity = 1,
                        UnitPrice = 10,
                        UnitPriceVat = 2,

                    }
                }
            };

            var results = P.CreateOrder(apiKey, newOrder);
            // An Order could create two orders. One normal one dropship
            foreach (var result in results)
            {
                if (result.Success)
                {
                    Console.WriteLine("Order Successfully Created!");
                    Console.WriteLine("Internal Mintsoft OrderId:" + result.OrderId);


                }
                else
                {
                    Console.WriteLine("Unable to Create Order Error:" + result.Message);


                }




            }
            Console.ReadKey();
        }


        public String GetAPIKey()
        {
            var client = new WebClient();
            var queryString = HttpUtility.ParseQueryString(string.Empty);

            queryString["UserName"] = USERNAME;
            queryString["Password"] = PASSWORD;


            var uri = "https://api.mintsoft.co.uk/api/Auth?" + queryString;


            var response = client.DownloadString(uri);


            Console.WriteLine("Response:" + response);

            return response.Replace("\"", "");
        }

        public List<NewOrderResult> CreateOrder(String apiKey, NewOrder order)
        {
            var OrderJson = Newtonsoft.Json.JsonConvert.SerializeObject(order);

            Console.WriteLine(OrderJson);
            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Accept, "application/json");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var ResultJson = webClient.UploadString("https://api.mintsoft.co.uk/api/Order?APIKey=" + apiKey, "PUT", OrderJson);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<NewOrderResult>>(ResultJson);
        }
    }
}

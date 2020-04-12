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
        public static String USERNAME = "api.test.client";

        public static String PASSWORD = "password";

        static void Main(string[] args)
        {
            Program P = new Program();

            var apiKey = P.GetAPIKey();

            Console.WriteLine("APIKey:" + apiKey);

            #region CreateOrder
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
                        Discount = 1

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

                    // If its not a dropship order
                    if(result.OrderId != 0)
                    {
                        // Register Webhook so that when if Orders gets Despatched or Cancel Mintsoft will callback URL to let you know
                        var OrderWebhookResult = P.RegisterOrderWebhook(apiKey,result.OrderId, new NewOrderConnectAction
                        {
                            Type = "API",
                            AccountId = 0,
                            Complete = false,
                            ExtraCode1 = "https://webhook.site/026c1aeb-0bbe-47c6-a3f3-0be04ecf9846",// Despatch Webhook Callback
                            ExtraCode2 = "https://webhook.site/026c1aeb-0bbe-47c6-a3f3-0be04ecf9846",// Cancel Webhook Callback
                            SourceOrderId = Guid.NewGuid().ToString()
                        });

                        Console.WriteLine("Order Webhook Result:" + OrderWebhookResult.Success + " Message:" + OrderWebhookResult.Message);
                    }


                }
                else
                {
                    Console.WriteLine("Unable to Create Order Error:" + result.Message);


                }
            }

            #endregion

            #region ASN - Warehouse Deliveries

            var ASN = new NewASN
            {
                Comments = "Test Delivery",
                EstimatedDelivery = DateTime.Now.AddDays(1),
                GoodsInType = "Pallet",
                Quantity = 2, // Delivery Will be two pallets
                POReference = "My PO Reference",
                WarehouseId = 3, // Main Warehouse
                Supplier = "New Supplier",
                Items = new List<NewASNItem>
               {
                   new NewASNItem
                   {
                       SKU = "test-sku",
                       Quantity = 100
                   }
               }
            };

            var ASNResult =  P.CreateASN(apiKey,ASN);

            Console.WriteLine("ASN Result:" + ASNResult.Success + " Message:" + ASNResult.Message + "ASNId:" + ASNResult.ID);

            if(ASNResult.Success)
            {
                // Register a webhook so we get called back when ASN has been booked into warehouse
                var ASNWebhookResult = P.RegisterASNWebhook(apiKey, ASNResult.ID, new NewASNConnectAction
                {
                    Type = "API",
                    AccountId = 0,
                    Complete = false,
                    ExtraCode1 = "https://webhook.site/026c1aeb-0bbe-47c6-a3f3-0be04ecf9846",// BookedIn Webhook Callback
                    SourceASNId = Guid.NewGuid().ToString()
                });

                Console.WriteLine("ASN Webhook Result:" + ASNWebhookResult.Success + " Message:" + ASNWebhookResult.Message);
            }



            #endregion



            #region GetStockLevels

            var StockLevels = P.GetStockLevels(apiKey, 3, true);

            foreach(var StockLevel in StockLevels)
            {
                Console.WriteLine("SKU:" + StockLevel.SKU + " Level:" + StockLevel.Level);
                if (StockLevel.Breakdown != null)
                {
                    foreach (var Breakdown in StockLevel.Breakdown)
                    {
                        Console.WriteLine("-->" + Breakdown.Quantity);
                    }
                }
            }

            #endregion

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

        public APIResult RegisterOrderWebhook(String apiKey, int OrderId, NewOrderConnectAction Connect)
        {
            var ConnectJson = Newtonsoft.Json.JsonConvert.SerializeObject(Connect);

            Console.WriteLine(ConnectJson);
            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Accept, "application/json");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var ResultJson = webClient.UploadString("https://api.mintsoft.co.uk/api/Order/" + OrderId + "/ConnectActions?APIKey=" + apiKey, "PUT", ConnectJson);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<APIResult>(ResultJson);
        }

        public APIResult RegisterASNWebhook(String apiKey, int ASNId, NewASNConnectAction Connect)
        {
            var ConnectJson = Newtonsoft.Json.JsonConvert.SerializeObject(Connect);

            Console.WriteLine(ConnectJson);
            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Accept, "application/json");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var ResultJson = webClient.UploadString("https://api-test.mintsoft.co.uk/api/ASN/" + ASNId + "/ConnectActions?APIKey=" + apiKey, "PUT", ConnectJson);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<APIResult>(ResultJson);
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

        public List<StockLevelResult> GetStockLevels(String apiKey, int WarehouseId, bool Breakdown)
        {
    
            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Accept, "application/json");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var ResultJson = webClient.DownloadString("https://api.mintsoft.co.uk/api/Product/StockLevels?APIKey=" + apiKey + "&WarehouseId=" + WarehouseId + "&Breakdown=" + Breakdown);
            Console.WriteLine(ResultJson);
            return Newtonsoft.Json.JsonConvert.DeserializeObject<List<StockLevelResult>>(ResultJson);
        }

        public APIResult CreateASN(String apiKey, NewASN NewAsn)
        {
            var AsnJson = Newtonsoft.Json.JsonConvert.SerializeObject(NewAsn);

            Console.WriteLine(AsnJson);
            var webClient = new WebClient();
            webClient.Headers.Add(HttpRequestHeader.Accept, "application/json");
            webClient.Headers.Add(HttpRequestHeader.ContentType, "application/json");
            var ResultJson = webClient.UploadString("https://api.mintsoft.co.uk/api/ASN?APIKey=" + apiKey, "PUT", AsnJson);

            return Newtonsoft.Json.JsonConvert.DeserializeObject<APIResult>(ResultJson);
        }
    }
}

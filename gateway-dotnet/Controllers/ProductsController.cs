﻿using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net.Http;
using Newtonsoft.Json;

namespace gateway.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private static HttpClient catalogHttpClient = new HttpClient();
        private static HttpClient inventoryHttpClient = new HttpClient();

        private static String catalogApiHost;
        private static String inventoryApiHost;
        public static void Config()
        {
            try
            {
                // discover the URL of the services we are going to call
                catalogApiHost = "http://" + 
                    Environment.GetEnvironmentVariable("CATALOG_COOLSTORE_SERVICE_HOST") + ":" +
                    Environment.GetEnvironmentVariable("CATALOG_COOLSTORE_SERVICE_PORT");
                
                inventoryApiHost = "http://" +
                    Environment.GetEnvironmentVariable("INVENTORY_COOLSTORE_SERVICE_HOST") + ":" +
                    Environment.GetEnvironmentVariable("INVENTORY_COOLSTORE_SERVICE_PORT");

                // set up the Http conection pools
                inventoryHttpClient.BaseAddress = new Uri(inventoryApiHost);
                catalogHttpClient.BaseAddress = new Uri(catalogApiHost);
            }
            catch(Exception e)
            {
                Console.WriteLine("Checking ENV CATALOG_COOLSTORE_SERVICE_HOST=" + Environment.GetEnvironmentVariable("CATALOG_COOLSTORE_SERVICE_HOST"));
                Console.WriteLine("Checking ENV CATALOG_COOLSTORE_SERVICE_PORT=" + Environment.GetEnvironmentVariable("CATALOG_COOLSTORE_SERVICE_PORT"));
                Console.WriteLine("Checking ENV INVENTORY_COOLSTORE_SERVICE_HOST=" + Environment.GetEnvironmentVariable("INVENTORY_COOLSTORE_SERVICE_HOST"));
                Console.WriteLine("Checking ENV INVENTORY_COOLSTORE_SERVICE_PORT=" + Environment.GetEnvironmentVariable("INVENTORY_COOLSTORE_SERVICE_PORT"));
                Console.WriteLine("Failure to build location URLs for Catalog and Inventory services: " + e.Message);
                throw e;
            }
        }

        
        private readonly ILogger<ProductsController> _logger;

        public ProductsController(ILogger<ProductsController> logger)
        {
            _logger = logger;

        }

        [HttpGet]
        public IEnumerable<Products> Get()
        {            
            try
            {
                // get the product list
                IEnumerable<Products> productsList = GetCatalog();

                // update each item with their inventory value
                foreach(Products p in productsList)
                {
                    Inventory inv = GetInventory(p.ItemId);
                    if (inv != null)
                        p.Availability = new Availability(inv);
                }    

                return productsList;
            }
            catch(Exception e)
            {
                Console.WriteLine("Using Catalog service: " + catalogApiHost + " and Inventory service: " + inventoryApiHost);
                Console.WriteLine("Failure to get service data: " + e.Message);
                // on failures return error
                throw e;
            }
        }

        private IEnumerable<Products> GetCatalog()
        { 
            var data = catalogHttpClient.GetStringAsync("/api/catalog").Result;
            return JsonConvert.DeserializeObject<IEnumerable<Products>>(data);
        }
        private Inventory GetInventory(string itemId)
        {
            var data = inventoryHttpClient.GetStringAsync("/api/inventory/" + itemId).Result;
            return JsonConvert.DeserializeObject<Inventory>(data);
        }

    }
}
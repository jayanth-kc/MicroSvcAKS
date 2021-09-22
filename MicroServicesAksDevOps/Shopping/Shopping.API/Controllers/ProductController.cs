using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;
using Shopping.API.Data;
using Shopping.API.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shopping.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController 
    {
        private readonly ProductContext _context;
        private readonly ILogger<ProductController> _logger;

        public ProductController(ProductContext context, ILogger<ProductController> logger)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        [HttpGet]
        public async Task<IEnumerable<Product>> Get()
        {
            //var rng = new Random();
            //return Enumerable.Range(1, 5).Select(index => new Product
            //{
            //    Name = "asdf"
            //}).ToArray();

            // return ProductContext.Products.ToArray();

            return await _context
                           .Products
                           .Find(p => true)
                           .ToListAsync();
        }
    }
}

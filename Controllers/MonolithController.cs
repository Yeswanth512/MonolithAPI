
// Monolithic API Example in .NET Core
// File: MonolithController.cs

using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace MonolithAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private static List<Order> Orders = new List<Order>
        {
            new Order { Id = 1, CustomerName = "John", Product = "Laptop", Amount = 1200 },
            new Order { Id = 2, CustomerName = "Jane", Product = "Phone", Amount = 800 }
        };

        private static List<Customer> Customers = new List<Customer>
        {
            new Customer { Id = 1, Name = "John", Email = "john@example.com" },
            new Customer { Id = 2, Name = "Jane", Email = "jane@example.com" }
        };

        private static List<Product> Products = new List<Product>
        {
            new Product { Id = 1, Name = "Laptop", Price = 1200 },
            new Product { Id = 2, Name = "Phone", Price = 800 }
        };

        private static List<Invoice> Invoices = new List<Invoice>();

        [HttpGet("all")]
        public IActionResult GetAllOrders()
        {
            return Ok(Orders);
        }

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] Order order)
        {
            order.Id = Orders.Count + 1;
            Orders.Add(order);
            return CreatedAtAction(nameof(GetOrderById), new { id = order.Id }, order);
        }

        [HttpGet("{id}")]
        public IActionResult GetOrderById(int id)
        {
            var order = Orders.Find(o => o.Id == id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpGet("customer/{id}")]
        public IActionResult GetCustomerById(int id)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == id);
            if (customer == null) return NotFound();
            return Ok(customer);
        }

        [HttpPost("customer/create")]
        public IActionResult CreateCustomer([FromBody] Customer customer)
        {
            customer.Id = Customers.Count + 1;
            Customers.Add(customer);
            return CreatedAtAction(nameof(GetCustomerById), new { id = customer.Id }, customer);
        }

        [HttpGet("product/{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = Products.FirstOrDefault(p => p.Id == id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost("product/create")]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            product.Id = Products.Count + 1;
            Products.Add(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.Id }, product);
        }

        [HttpPost("invoice/create")]
        public IActionResult CreateInvoice([FromBody] Invoice invoice)
        {
            invoice.Id = Invoices.Count + 1;
            Invoices.Add(invoice);
            return CreatedAtAction(nameof(GetInvoiceById), new { id = invoice.Id }, invoice);
        }

        [HttpGet("invoice/{id}")]
        public IActionResult GetInvoiceById(int id)
        {
            var invoice = Invoices.FirstOrDefault(i => i.Id == id);
            if (invoice == null) return NotFound();
            return Ok(invoice);
        }
    }

    public class Order
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public string Product { get; set; }
        public decimal Amount { get; set; }
    }

    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Invoice
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}

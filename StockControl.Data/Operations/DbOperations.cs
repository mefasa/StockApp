using Microsoft.EntityFrameworkCore;
using StockApp.Context;
using StockControl.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StockControl.Data.Operations
{
    public class DbOperations
    {
        private readonly SDbContext _context;
        public DbOperations(SDbContext context) 
        {      
            _context = context;
        }
        public List<Customer> ListCustomers()
        {
            List<Customer> vs = new List<Customer>();
            try
            {
                vs = _context.Customers.ToList();

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vs;
        }
        public List<Product> ListProducts()
        {
            List<Product> vs = new List<Product>();
            try
            {
                vs = _context.Products.ToList();

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vs;
        }
        public List<OrderModel> ListOrders()
        {
            List<OrderModel> vs = new List<OrderModel>();
            try
            {
                var ords = _context.Orders.ToList();
                vs = ords.Select(s => new OrderModel()
                {
                    Id = s.Id,
                    CustomerId = s.CustomerId,
                    Customer = GetCustomer(s.CustomerId),
                    ProductId = s.ProductId,
                    Product = GetProduct(s.ProductId),
                    OrderDate = s.OrderDate,
                    Piece = s.Piece

                }).ToList();
                

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vs;
        }
        public Customer GetCustomer(string id)
        {
            Customer vs = new Customer();
            try
            {
                
                vs = _context.Customers.FirstOrDefault(m => m.Id == id);

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vs;
        }
        public Product GetProduct(string id)
        {
            Product vs = new Product();
            try
            {
                vs = _context.Products.FirstOrDefault(m => m.Id == id);

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vs;
        }
        public OrderModel GetOrder(string id)
        {
            OrderModel vs;
            try
            {
                var ord = _context.Orders.FirstOrDefault(m => m.Id == id);
                vs = new OrderModel()
                {
                    CustomerId = ord.CustomerId,
                    ProductId = ord.ProductId,
                    OrderDate = ord.OrderDate,
                    Customer = _context.Customers.Find(ord.CustomerId),
                    Product = _context.Products.Find(ord.ProductId),
                    Piece = ord.Piece,
                    Id = ord.Id
                };

            }
            catch (Exception exc)
            {

                throw exc;
            }
            return vs;
        }
        public bool CreateCustomer(Customer customer)
        {
            bool res = false;
            try
            {
                _context.Add(customer);
                _context.SaveChanges();
                res = true;
            }
            catch (Exception)
            {

                res = false;
            }
            return res;

        }
        public bool DeleteCustomer(string id)
        {
            bool res = false;
            try
            {
                var customer = _context.Customers.Find(id);
                _context.Customers.Remove(customer);
                _context.SaveChangesAsync();
                res = true;
            }
            catch (Exception)
            {

                res = false;
            }
            return res;

            
        }
        public void EditCustomer(Customer customer)
        {
            try
            {
                _context.Update(customer);
                _context.SaveChanges();
               
            }
            catch (DbUpdateConcurrencyException exc)
            {

                throw exc;
            }
        
        }
        public bool CreateProduct(Product product)
        {
            bool res = false;
            try
            {
                _context.Add(product);
                _context.SaveChanges();
                res = true;
            }
            catch (Exception)
            {

                res = false;
            }
            return res;

        }
        public bool DeleteProduct(string id)
        {
            bool res = false;
            try
            {
                var product = _context.Products.Find(id);
                _context.Products.Remove(product);
                _context.SaveChangesAsync();
                res = true;
            }
            catch (Exception)
            {

                res = false;
            }
            return res;


        }
        public void EditProduct(Product product)
        {
            try
            {
                _context.Update(product);
                _context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException exc)
            {

                throw exc;
            }

        }
        public bool CreateOrder(Order order)
        {
            bool res = false;
            try
            {
                _context.Add(order);
                _context.SaveChanges();
                var ordPiece = order.Piece;
                var pro = _context.Products.Find(order.ProductId);
                pro.Piece = pro.Piece - Convert.ToInt32(ordPiece);
                EditProduct(pro);
                res = true;
            }
            catch (Exception)
            {

                res = false;
            }
            return res;

        }
        public bool DeleteOrder(string id)
        {
            bool res = false;
            try
            {
                var order = _context.Orders.Find(id);
                _context.Orders.Remove(order);
                _context.SaveChangesAsync();
                var ordPiece = order.Piece;
                var pro = _context.Products.Find(order.ProductId);
                pro.Piece = pro.Piece + Convert.ToInt32(ordPiece);
                EditProduct(pro);
                res = true;
            }
            catch (Exception)
            {

                res = false;
            }
            return res;


        }
        public void EditOrder(Order order)
        {
            try
            {
                _context.Update(order);
                _context.SaveChanges();

            }
            catch (DbUpdateConcurrencyException exc)
            {

                throw exc;
            }

        }
    }
}

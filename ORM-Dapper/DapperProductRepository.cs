using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ORM_Dapper
{
    public class DapperProductRepository : IProductRepository
    {
        private readonly IDbConnection _connection;
        public DapperProductRepository(IDbConnection connection)
        {
            _connection = connection;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            return _connection.Query<Product>("SELECT * FROM Products;");
        }

        public void CreateProduct(string name, double price, int categoryId)
        {
           _connection.Execute("INSERT INTO Products (Name, Price, CategoryID) VALUES (@name, @price, @categoryId);",
                new { name = name, price = price, categoryId = categoryId });
        }

        public void DeleteProduct(int productId)
        {
            _connection.Execute("DELETE FROM Products WHERE ProductID = @productId;",
                new { productId = productId });

       
            _connection.Execute("DELETE FROM sales WHERE ProductID = @productID;",
               new { productID = productId });

            _connection.Execute("DELETE FROM products WHERE ProductID = @productID;",
               new { productID = productId });
        }

       
        public void UpdateProduct(Product product)
        {
            _connection.Execute("UPDATE Products SET Name = @Name, Price = @Price, CategoryID = @CategoryID WHERE ProductID = @ProductID;",
                new { Name = product.Name, Price = product.Price, CategoryID = product.CategoryID, ProductID = product.ProductID });
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using I_Shop.Models.ShopModels;
using I_Shop.Models;
using System.Threading.Tasks;


namespace I_Shop.Controllers
{
    public class ProductsController : ApiController
    {
        private readonly ShopContext db;

        public ProductsController()
        {
            db = new ShopContext();
        }

        // GET api/products

        public PagedResult<Product> Get(int pageNo = 1, int pageSize = 50, [FromUri] string[] sort = null, string search = null)
        {
            // Determine the number of records to skip
            int skip = (pageNo - 1) * pageSize;

            IQueryable<Product> queryable = db.Products;

            // Apply the search
            if (!String.IsNullOrEmpty(search))
            {
                string[] searchElements = search.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string searchElement in searchElements)
                {
                    string element = searchElement;
                    queryable = queryable.Where(c => c.Name.Contains(element) || c.Description.Contains(element));
                }
            }

            // Add the sorting

            if (sort != null)
                queryable = queryable.ApplySorting(sort);
            else
                queryable = queryable.OrderBy(c => c.ProductID);

            // Get the total number of records
            int totalItemCount = queryable.Count();

            // Retrieve the customers for the specified page
            var products = queryable
                .Skip(skip)
                .Take(pageSize)
                .ToList();

            // Return the paged results
            return new PagedResult<Product>(products, pageNo, pageSize, totalItemCount);
        }
    }
}
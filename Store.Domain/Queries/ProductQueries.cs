using System;
using System.Linq.Expressions;
using Store.Domain.Entities;

namespace Store.Domain.Queries
{
    public class ProductQueries
    {
        public static Expression<Func<Product, bool>> GetActiveProducts() => x => x.Active;
        public static Expression<Func<Product, bool>> GetActiveInactiveProducts() => x => x.Active == false;

    }
}
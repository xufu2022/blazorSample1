using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DailyReport.Shared.Models;
using Microsoft.EntityFrameworkCore;

namespace DailyReport.Application.Helpers
{
    public static class ApplicationPaginationExtensions
    {
        /// <summary>
        /// Paginates a query asynchronously and returns a PaginatedResult.
        /// </summary>
        public static async Task<PaginatedResult<T>> PaginateAsync<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize,
            CancellationToken cancellationToken = default)
        {
            var totalItemCount = await query.CountAsync(cancellationToken);
            var items = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(cancellationToken);

            return new PaginatedResult<T>
            {
                Items = items,
                TotalItemCount = totalItemCount,
                PageSize = pageSize,
                PageNumber = pageNumber,
                PageCount = (int)Math.Ceiling(totalItemCount / (double)pageSize)
            };
        }

        /// <summary>
        /// Applies dynamic sorting to an IQueryable.
        /// </summary>
        public static IQueryable<T> ApplySorting<T>(
            this IQueryable<T> query,
            string orderBy,
            bool descending = false)
        {
            if (string.IsNullOrEmpty(orderBy)) return query;

            var parameter = Expression.Parameter(typeof(T), "x");
            var property = Expression.Property(parameter, orderBy);
            var keySelector = Expression.Lambda(property, parameter);

            var methodName = descending ? "OrderByDescending" : "OrderBy";
            var method = typeof(Queryable).GetMethods()
                .First(m => m.Name == methodName && m.GetParameters().Length == 2)
                .MakeGenericMethod(typeof(T), property.Type);

            return (IQueryable<T>)method.Invoke(null, new object[] { query, keySelector });
        }

        /// <summary>
        /// Applies filtering to an IQueryable based on a predicate.
        /// </summary>
        public static IQueryable<T> ApplyFiltering<T>(
            this IQueryable<T> query,
            Expression<Func<T, bool>> predicate)
        {
            return query.Where(predicate);
        }

        /// <summary>
        /// Combines filtering, sorting, and pagination into a single workflow.
        /// </summary>
        public static async Task<PaginatedResult<T>> ApplyPaginationAsync<T>(
            this IQueryable<T> query,
            int pageNumber,
            int pageSize,
            string? orderBy = null,
            bool descending = false,
            Expression<Func<T, bool>>? filter = null,
            CancellationToken cancellationToken = default)
        {
            if (filter != null)
            {
                query = query.ApplyFiltering(filter);
            }

            if (!string.IsNullOrEmpty(orderBy))
            {
                query = query.ApplySorting(orderBy, descending);
            }

            return await query.PaginateAsync(pageNumber, pageSize, cancellationToken);
        }
    }
}

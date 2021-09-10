using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using MyLaboratory.Common.DataAccess.Data;
using MyLaboratory.Common.DataAccess.Models;
using MyLaboratory.Common.DataAccess.Contracts;
using System.Threading.Tasks;

namespace MyLaboratory.Common.DataAccess.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly ApplicationDbContext context;

        public CategoryRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// ���ѿ� ���� ī�װ����� ���մϴ�.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Category>> GetCategoryByRoleAsync(string role)
        {
            return await context.Categories
                    .Where(a => a.Role == role)
                    .OrderBy(a => a.Order)
                    .ToListAsync();
        }
        /// <summary>
        /// ī�װ��� �����մϴ�.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task CreateCategoryAsync(Category category)
        {
            await context.Categories.AddAsync(category);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// ī�װ��� ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task UpdateCategoryAsync(Category category)
        {
            context.Categories.Update(category);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// ī�װ��� �����մϴ�.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public async Task DeleteCategoryAsync(Category category)
        {
            context.Categories.Remove(category);
            await context.SaveChangesAsync();
        }
    }
}
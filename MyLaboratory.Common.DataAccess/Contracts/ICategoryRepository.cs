using System.Collections.Generic;
using System.Threading.Tasks;
using MyLaboratory.Common.DataAccess.Models;

namespace MyLaboratory.Common.DataAccess.Contracts
{
    public interface ICategoryRepository
    {
        /// <summary>
        /// ���ѿ� ���� ī�װ����� ���մϴ�.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<Category>> GetCategoryByRoleAsync(string role);
        /// <summary>
        /// ī�װ��� �����մϴ�.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task CreateCategoryAsync(Category category);
        /// <summary>
        /// ī�װ��� ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task UpdateCategoryAsync(Category category);
        /// <summary>
        /// ī�װ��� �����մϴ�.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task DeleteCategoryAsync(Category category);
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using MyLaboratory.Common.DataAccess.Models;

namespace MyLaboratory.Common.DataAccess.Contracts
{
    public interface ISubCategoryRepository
    {
        /// <summary>
        /// ���ѿ� ���� ����ī�װ����� ���մϴ�.
        /// </summary>
        /// <param name="role"></param>
        /// <returns></returns>
        public Task<IEnumerable<SubCategory>> GetSubCategoryByRoleAsync(string role);
        /// <summary>
        /// ����ī�װ��� �����մϴ�.
        /// </summary>
        /// <param name="subCategory"></param>
        /// <returns></returns>
        public Task CreateSubCategoryAsync(SubCategory subCategory);
        /// <summary>
        /// ����ī�װ��� ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task UpdateSubCategoryAsync(SubCategory subCategory);
        /// <summary>
        /// ����ī�װ��� �����մϴ�.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public Task DeleteSubCategoryAsync(SubCategory subCategory);
    }
}
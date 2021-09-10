using System.Collections.Generic;
using System.Threading.Tasks;
using MyLaboratory.Common.DataAccess.Models;

namespace MyLaboratory.Common.DataAccess.Contracts
{
    public interface IAssetRepository
    {
        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� �ڻ��� ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<List<Asset>> GetAssetsAsync(string email);

        /// <summary>
        /// �α��� ������ �ش��ϴ� Ư�� �ڻ��� ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public Task<Asset> GetAssetAsync(string email, string productName);

        /// <summary>
        /// �ڻ��� �����մϴ�.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public Task CreateAssetAsync(Asset asset);

        /// <summary>
        /// �ڻ��� ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public Task UpdateAssetAsync(Asset asset);
    }
}
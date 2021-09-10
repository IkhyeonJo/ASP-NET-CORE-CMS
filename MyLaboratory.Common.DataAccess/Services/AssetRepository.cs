using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLaboratory.Common.DataAccess.Models;
using MyLaboratory.Common.DataAccess.Data;
using MyLaboratory.Common.DataAccess.Contracts;

namespace MyLaboratory.Common.DataAccess.Services
{
    public class AssetRepository : IAssetRepository
    {
        private readonly ApplicationDbContext context;

        public AssetRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� �ڻ��� ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<List<Asset>> GetAssetsAsync(string email)
        {
            return await context.Assets.Where(x => x.AccountEmail == email).ToListAsync();
        }

        /// <summary>
        /// �α��� ������ �ش��ϴ� Ư�� �ڻ��� ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="productName"></param>
        /// <returns></returns>
        public async Task<Asset> GetAssetAsync(string email, string productName)
        {
            return await context.Assets.FirstOrDefaultAsync(a => a.AccountEmail == email && a.ProductName == productName);
        }

        /// <summary>
        /// �ڻ��� �����մϴ�.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public async Task CreateAssetAsync(Asset asset)
        {
            await context.Assets.AddAsync(asset);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// �ڻ��� ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="asset"></param>
        /// <returns></returns>
        public async Task UpdateAssetAsync(Asset asset)
        {
            context.Assets.Update(asset);
            await context.SaveChangesAsync();
        }
    }
}
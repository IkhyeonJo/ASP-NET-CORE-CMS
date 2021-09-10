using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLaboratory.Common.DataAccess.Data;
using MyLaboratory.Common.DataAccess.Models;
using MyLaboratory.Common.DataAccess.Contracts;

namespace MyLaboratory.Common.DataAccess.Services
{
    public class AccountRepository : IAccountRepository
    {
        private readonly ApplicationDbContext context;

        public AccountRepository(ApplicationDbContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// ��ϵ� ��� ������ ���մϴ�.
        /// </summary>
        /// <returns></returns>
        public async Task<List<Account>> GetAllAsync()
        {
            return await context.Accounts.ToListAsync();
        }
        /// <summary>
        /// �� ������ ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<Account> GetAccountAsync(string email)
        {
            return await context.Accounts.FirstOrDefaultAsync(x => x.Email == email);
        }
        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task CreateAccountAsync(Account account)
        {
            await context.Accounts.AddAsync(account);
            await context.SaveChangesAsync();
        }
        /// <summary>
        /// ������ ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task UpdateAccountAsync(Account account)
        {
            context.Accounts.Update(account);
            await context.SaveChangesAsync();
        }
    }
}
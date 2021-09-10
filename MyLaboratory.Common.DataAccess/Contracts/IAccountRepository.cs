using System.Collections.Generic;
using System.Threading.Tasks;
using MyLaboratory.Common.DataAccess.Models;

namespace MyLaboratory.Common.DataAccess.Contracts
{
    public interface IAccountRepository
    {
        /// <summary>
        /// ��ϵ� ��� ������ ���մϴ�.
        /// </summary>
        /// <returns></returns>
        public Task<List<Account>> GetAllAsync();
        /// <summary>
        /// �� ������ ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<Account> GetAccountAsync(string email);
        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Task CreateAccountAsync(Account account);
        /// <summary>
        /// ������ ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public Task UpdateAccountAsync(Account account);
    }
}
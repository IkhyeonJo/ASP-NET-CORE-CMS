using System.Collections.Generic;
using System.Threading.Tasks;
using MyLaboratory.Common.DataAccess.Models;

namespace MyLaboratory.Common.DataAccess.Contracts
{
    public interface IIncomeRepository
    {
        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� ������ ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<List<Income>> GetIncomesAsync(string email);

        /// <summary>
        /// �α��� ������ �ش��ϴ� �ݳ�� ������ ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public Task<List<Income>> GetCurrentYearMonthIncomesAsync(string email, string yearMonth);

        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public Task CreateIncomeAsync(Income income);

        /// <summary>
        /// ������ ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public Task UpdateIncomeAsync(Income income);

        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public Task DeleteIncomeAsync(Income income);
    }
}
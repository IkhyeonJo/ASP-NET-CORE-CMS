using System.Collections.Generic;
using System.Threading.Tasks;
using MyLaboratory.Common.DataAccess.Models;

namespace MyLaboratory.Common.DataAccess.Contracts
{
    public interface IFixedIncomeRepository
    {
        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� ���������� ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<List<FixedIncome>> GetFixedIncomesAsync(string email);

        /// <summary>
        /// ���������� �����մϴ�.
        /// </summary>
        /// <param name="fixedIncome"></param>
        /// <returns></returns>
        public Task CreateFixedIncomeAsync(FixedIncome fixedIncome);

        /// <summary>
        /// ���������� ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="fixedIncome"></param>
        /// <returns></returns>
        public Task UpdateFixedIncomeAsync(FixedIncome fixedIncome);

        /// <summary>
        /// ���������� �����մϴ�.
        /// </summary>
        /// <param name="fixedIncome"></param>
        /// <returns></returns>
        public Task DeleteFixedIncomeAsync(FixedIncome fixedIncome);
    }
}
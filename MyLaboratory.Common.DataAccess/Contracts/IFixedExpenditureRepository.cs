using System.Collections.Generic;
using System.Threading.Tasks;
using MyLaboratory.Common.DataAccess.Models;

namespace MyLaboratory.Common.DataAccess.Contracts
{
    public interface IFixedExpenditureRepository
    {
        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� ���������� ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<List<FixedExpenditure>> GetFixedExpendituresAsync(string email);

        /// <summary>
        /// ���������� �����մϴ�.
        /// </summary>
        /// <param name="fixedExpenditure"></param>
        /// <returns></returns>
        public Task CreateFixedExpenditureAsync(FixedExpenditure fixedExpenditure);

        /// <summary>
        /// ���������� ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="fixedExpenditure"></param>
        /// <returns></returns>
        public Task UpdateFixedExpenditureAsync(FixedExpenditure fixedExpenditure);

        /// <summary>
        /// ���������� �����մϴ�.
        /// </summary>
        /// <param name="fixedExpenditure"></param>
        /// <returns></returns>
        public Task DeleteFixedExpenditureAsync(FixedExpenditure fixedExpenditure);
    }
}
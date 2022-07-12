using System.Collections.Generic;
using System.Threading.Tasks;
using MyLaboratory.Common.DataAccess.Models;

namespace MyLaboratory.Common.DataAccess.Contracts
{
    public interface IExpenditureRepository
    {
        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� ������ ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public Task<List<Expenditure>> GetExpendituresAsync(string email);

        /// <summary>
        /// �α��� ������ �ش��ϴ� �ݳ�� ������ ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public Task<List<Expenditure>> GetCurrentYearMonthExpendituresAsync(string email, string yearMonth);

        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="expenditure"></param>
        /// <returns></returns>
        public Task CreateExpenditureAsync(Expenditure expenditure);

        /// <summary>
        /// ������ ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="expenditure"></param>
        /// <returns></returns>
        public Task UpdateExpenditureAsync(Expenditure expenditure);

        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="expenditure"></param>
        /// <returns></returns>
        public Task DeleteExpenditureAsync(Expenditure expenditure);
    }
}
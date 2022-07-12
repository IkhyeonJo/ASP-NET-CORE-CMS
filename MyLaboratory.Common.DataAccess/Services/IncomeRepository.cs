using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLaboratory.Common.DataAccess.Data;
using MyLaboratory.Common.DataAccess.Models;
using MyLaboratory.Common.DataAccess.Contracts;
using MySqlConnector;

namespace MyLaboratory.Common.DataAccess.Services
{
    public class IncomeRepository : IIncomeRepository
    {
        private readonly ApplicationDbContext context;

        public IncomeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� ������ ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<List<Income>> GetIncomesAsync(string email)
        {
            return await context.Incomes.Where(x => x.AccountEmail == email).ToListAsync();
        }

        /// <summary>
        /// �α��� ������ �ش��ϴ� �ݳ�� ������ ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <param name="yearMonth"></param>
        /// <returns></returns>
        public async Task<List<Income>> GetCurrentYearMonthIncomesAsync(string email, string yearMonth)
        {
            return await context.Incomes.FromSqlInterpolated<Income>
                    (
                        @$"SELECT *
                            FROM Income
                            WHERE 
                            DATE_FORMAT(Created, '%Y-%m') = {yearMonth}
                            AND AccountEmail = {email}"
                    ).ToListAsync();
        }

        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public async Task CreateIncomeAsync(Income income)
        {
            await context.Incomes.AddAsync(income);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// ������ ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public async Task UpdateIncomeAsync(Income income)
        {
            context.Incomes.Update(income);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="income"></param>
        /// <returns></returns>
        public async Task DeleteIncomeAsync(Income income)
        {
            context.Incomes.Remove(income);
            await context.SaveChangesAsync();
        }
    }
}
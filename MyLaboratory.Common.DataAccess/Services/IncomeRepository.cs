using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLaboratory.Common.DataAccess.Data;
using MyLaboratory.Common.DataAccess.Models;
using MyLaboratory.Common.DataAccess.Contracts;

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
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLaboratory.Common.DataAccess.Data;
using MyLaboratory.Common.DataAccess.Models;
using MyLaboratory.Common.DataAccess.Contracts;

namespace MyLaboratory.Common.DataAccess.Services
{
    public class FixedIncomeRepository : IFixedIncomeRepository
    {
        private readonly ApplicationDbContext context;

        public FixedIncomeRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� ���������� ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<List<FixedIncome>> GetFixedIncomesAsync(string email)
        {
            return await context.FixedIncomes.Where(x => x.AccountEmail == email).ToListAsync();
        }

        /// <summary>
        /// ���������� �����մϴ�.
        /// </summary>
        /// <param name="fixedIncome"></param>
        /// <returns></returns>
        public async Task CreateFixedIncomeAsync(FixedIncome fixedIncome)
        {
            await context.FixedIncomes.AddAsync(fixedIncome);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// ���������� ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="fixedIncome"></param>
        /// <returns></returns>
        public async Task UpdateFixedIncomeAsync(FixedIncome fixedIncome)
        {
            context.FixedIncomes.Update(fixedIncome);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// ���������� �����մϴ�.
        /// </summary>
        /// <param name="fixedIncome"></param>
        /// <returns></returns>
        public async Task DeleteFixedIncomeAsync(FixedIncome fixedIncome)
        {
            context.FixedIncomes.Remove(fixedIncome);
            await context.SaveChangesAsync();
        }
    }
}
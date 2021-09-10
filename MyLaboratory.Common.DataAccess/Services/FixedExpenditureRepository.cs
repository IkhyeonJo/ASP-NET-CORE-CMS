using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLaboratory.Common.DataAccess.Data;
using MyLaboratory.Common.DataAccess.Models;
using MyLaboratory.Common.DataAccess.Contracts;

namespace MyLaboratory.Common.DataAccess.Services
{
    public class FixedExpenditureRepository : IFixedExpenditureRepository
    {
        private readonly ApplicationDbContext context;

        public FixedExpenditureRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� ���������� ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<List<FixedExpenditure>> GetFixedExpendituresAsync(string email)
        {
            return await context.FixedExpenditures.Where(x => x.AccountEmail == email).ToListAsync();
        }

        /// <summary>
        /// ���������� �����մϴ�.
        /// </summary>
        /// <param name="fixedExpenditure"></param>
        /// <returns></returns>
        public async Task CreateFixedExpenditureAsync(FixedExpenditure fixedExpenditure)
        {
            await context.FixedExpenditures.AddAsync(fixedExpenditure);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// ���������� ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="fixedExpenditure"></param>
        /// <returns></returns>
        public async Task UpdateFixedExpenditureAsync(FixedExpenditure fixedExpenditure)
        {
            context.FixedExpenditures.Update(fixedExpenditure);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// ���������� �����մϴ�.
        /// </summary>
        /// <param name="fixedExpenditure"></param>
        /// <returns></returns>
        public async Task DeleteFixedExpenditureAsync(FixedExpenditure fixedExpenditure)
        {
            context.FixedExpenditures.Remove(fixedExpenditure);
            await context.SaveChangesAsync();
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MyLaboratory.Common.DataAccess.Data;
using MyLaboratory.Common.DataAccess.Models;
using MyLaboratory.Common.DataAccess.Contracts;

namespace MyLaboratory.Common.DataAccess.Services
{
    public class ExpenditureRepository : IExpenditureRepository
    {
        private readonly ApplicationDbContext context;

        public ExpenditureRepository(ApplicationDbContext context)
        {
            this.context = context;
        }

        /// <summary>
        /// �α��� ������ �ش��ϴ� ��� ������ ���մϴ�.
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<List<Expenditure>> GetExpendituresAsync(string email)
        {
            return await context.Expenditures.Where(x => x.AccountEmail == email).ToListAsync();
        }

        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="expenditure"></param>
        /// <returns></returns>
        public async Task CreateExpenditureAsync(Expenditure expenditure)
        {
            await context.Expenditures.AddAsync(expenditure);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// ������ ������Ʈ�մϴ�.
        /// </summary>
        /// <param name="expenditure"></param>
        /// <returns></returns>
        public async Task UpdateExpenditureAsync(Expenditure expenditure)
        {
            context.Expenditures.Update(expenditure);
            await context.SaveChangesAsync();
        }

        /// <summary>
        /// ������ �����մϴ�.
        /// </summary>
        /// <param name="expenditure"></param>
        /// <returns></returns>
        public async Task DeleteExpenditureAsync(Expenditure expenditure)
        {
            context.Expenditures.Remove(expenditure);
            await context.SaveChangesAsync();
        }
    }
}
using Bitirme.Domain.Abstract;
using Bitirme.Domain.Data;
using Bitirme.Domain.Entities;

namespace Bitirme.Domain.Concrete
{
    public class GroupsMembersRepository : GenericRepository<GroupMembers,DataContext>,IGroupsMembersRepository
    {
        private DataContext context;
        public GroupsMembersRepository(DataContext context) : base(context)
        {
            this.context = context;
        }
    }
}

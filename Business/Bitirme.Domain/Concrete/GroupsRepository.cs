using Bitirme.Domain.Abstract;
using Bitirme.Domain.Data;
using Bitirme.Domain.Entities;

namespace Bitirme.Domain.Concrete
{
    public class GroupsRepository :GenericRepository<Groups ,DataContext>, IGroupsRepsoitory
    {
        private DataContext _context;

        public GroupsRepository(DataContext context) : base(context)
        {
            _context = context;
        }
    }
}

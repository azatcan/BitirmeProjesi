using Bitirme.Domain.Abstract;
using Bitirme.Domain.Entities;
using Bitirme.Infrastructure.Abstract;
using System.Linq.Expressions;

namespace Bitirme.Infrastructure.Concrete
{
    public class GroupMembersManager : GroupMembersService
    {
        IGroupsMembersRepository _groupsMembersRepository;

        public GroupMembersManager(IGroupsMembersRepository groupsMembersRepository)
        {
            _groupsMembersRepository = groupsMembersRepository;
        }

        public void Add(GroupMembers p)
        {
            _groupsMembersRepository.Add(p);
        }

        public void Delete(GroupMembers p)
        {
            _groupsMembersRepository.Delete(p);
        }

        public GroupMembers GetById(Guid id)
        {
            return _groupsMembersRepository.GetById(id);
        }

        public List<GroupMembers> List()
        {
            return _groupsMembersRepository.List();
        }

        public List<GroupMembers> List(Expression<Func<GroupMembers, bool>> filter)
        {
            return _groupsMembersRepository.List(filter);
        }

        public void Update(GroupMembers p)
        {
            _groupsMembersRepository.Update(p);
        }
    }
}

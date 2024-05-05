using Bitirme.Domain.Abstract;
using Bitirme.Domain.Entities;
using Bitirme.Infrastructure.Abstract;
using System.Linq.Expressions;

namespace Bitirme.Infrastructure.Concrete
{
    public class GroupsManager : GroupsService
    {
        IGroupsRepsoitory _groupsRepository;

        public GroupsManager(IGroupsRepsoitory groupsRepository)
        {
            _groupsRepository = groupsRepository;
        }

        public void Add(Groups p)
        {
            _groupsRepository.Add(p);
        }

        public void Delete(Groups p)
        {
            _groupsRepository.Delete(p);
        }

        public Groups GetById(Guid id)
        {
            return _groupsRepository.GetById(id);
        }

        public List<Groups> List()
        {
            return _groupsRepository.List();
        }

        public List<Groups> List(Expression<Func<Groups, bool>> filter)
        {
            return _groupsRepository.List(filter);
        }

        public void Update(Groups p)
        {
            _groupsRepository.Update(p);
        }
    }
}

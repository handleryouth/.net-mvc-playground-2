using AutoMapper;
using contactForm.Entities;
using contactForm.Models.CommonModel;
using contactForm.Repositories;

namespace contactForm.Services
{
    public interface IRegisterServices
    {
        Task<IEnumerable<RegisterEntities>> GetAll();
        Task<RegisterEntities> GetById(int id);
        Task Create(RegisterModel registerModel);
        Task Update(int id, RegisterModel registerModel);
        Task Delete(int id);
    }


    public class RegisterServices : IRegisterServices
    {

        private readonly IRegisterRepository _registerRepository;
        private readonly IMapper _mapper;

        public RegisterServices(IRegisterRepository registerRepository, IMapper mapper)
        {
            _registerRepository = registerRepository;
            _mapper = mapper;
        }

        public async Task Create(RegisterModel registerModel)
        {
            var user = _mapper.Map<RegisterEntities>(registerModel);

            await _registerRepository.Create(user);
        }

        public async Task Delete(int id)
        {
            await _registerRepository.Delete(id);
        }

        public Task<IEnumerable<RegisterEntities>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<RegisterEntities> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task Update(int id, RegisterModel registerModel)
        {
            throw new NotImplementedException();
        }
    }
}
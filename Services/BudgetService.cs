using AutoMapper;
using No_Overspend_Api.Infra.Models;

namespace No_Overspend_Api.Services
{
    public interface IBudgetService
    {

    }
    public class BudgetService : IBudgetService
    {
        private readonly NoOverspendContext _context;
        private readonly IMapper _mapper;
        public BudgetService(NoOverspendContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

    }
}

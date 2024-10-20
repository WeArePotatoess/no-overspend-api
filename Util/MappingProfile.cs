using AutoMapper;
using No_Overspend_Api.DTOs.Category.Response;
using No_Overspend_Api.DTOs.Transaction.Request;
using No_Overspend_Api.DTOs.Transaction.Response;
using No_Overspend_Api.Infra.Models;

namespace No_Overspend_Api.Util
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<category, CategoryView>()
                .ForMember(des => des.icon_content, opts => opts.MapFrom(src => src.icon.content));
            CreateMap<CreateCategoryRequest, category>();

            //Transaction
            CreateMap<CreateTransactionRequest, transaction>();
            CreateMap<transaction, TransactionView>()
                .ForMember(des => des.category_name, opts => opts.MapFrom(src => src.category.name));
        }
    }
}

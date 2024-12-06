using AutoMapper;
using sirius.Entities;
using sirius.Models.Hypothesis;
using sirius.Models.HypothesisCategory;
using sirius.Models.HypothesisHistory;
using sirius.Models.Livrable;
using sirius.Models.OperationalPrioritization;

namespace sirius.Configurations;

public class AutoMapperInitializer : Profile
{
	public AutoMapperInitializer()
	{
		CreateMappingsForHypotheseCategories();
		CreateMappingsForOperationalPrioritization();
		CreateMappingsForHypothesis();
		CreateMappingsForHypothesisHistory();
		CreateMappingsForLivrable();
	}

	private void CreateMappingsForLivrable()
	{
		CreateMap<Livrable, LivrableCreateDto>().ReverseMap();
		CreateMap<Livrable, LivrableUpdateDto>().ReverseMap();
		CreateMap<Livrable, LivrableViewDto>().ReverseMap();
	}
	private void CreateMappingsForHypothesis()
	{
		CreateMap<Hypothesis, HypothesisCreateDto>().ReverseMap();
			CreateMap<HypothesisCreateDto, Hypothesis>()
			.ForMember(dest => dest.CategoryId, opt => opt.MapFrom(src => src.CategoryId));

		CreateMap<Hypothesis, HypothesisUpdateDto>().ReverseMap();
		CreateMap<Hypothesis, HypothesisViewDto>()
			.ForMember(dest => dest.HypothesisHistories, opt => opt.MapFrom(src => src.History.ToList()))
			.ForMember(dest => dest.HypothesisCategoryName, opt => opt.MapFrom(src => src.Category.Name))
			.ForMember(dest => dest.HypothesisTypeName, opt => opt.MapFrom(src => src.Type))
			.ForMember(dest => dest.HypothesisCategoryId, opt => opt.MapFrom(src => src.Category.Id))
			.ReverseMap();

	}
	private void CreateMappingsForHypotheseCategories()
	{
		
		CreateMap<HypothesisCategory, CategoryCreateDto>().ReverseMap();
		CreateMap<HypothesisCategory, CategoryUpdateDto>().ReverseMap();
		CreateMap<HypothesisCategory, CategoryViewDto>().ForMember(dest => dest.Hypothesis, opt => opt.MapFrom(src => src.Hypothesis.ToList())).ReverseMap();
	}
	private void CreateMappingsForHypothesisHistory()
	{
		CreateMap<HypothesisHistory, HistoryViewDto>()
			.ForMember(dest => dest.HypothesisId, opt => opt.MapFrom(src => src.Hypothesis.Id))
			.ForMember(dest => dest.HypothesisName, opt => opt.MapFrom(src => src.Hypothesis.Nom))
			.ForMember(dest => dest.HypothesisDescription, opt => opt.MapFrom(src => src.Hypothesis.Description))
			.ForMember(dest => dest.HypothesisType, opt => opt.MapFrom(src => src.Hypothesis.Type))
			.ReverseMap();
		CreateMap<HypothesisHistory, HistoryCreateDto>().ReverseMap();
		CreateMap<HypothesisHistory, HistoryUpdateDto>().ReverseMap();

	}
	private void CreateMappingsForOperationalPrioritization()
	{
		CreateMap<OperationalPrioritization, PrioritizationCreateDto>().ReverseMap();
		CreateMap<OperationalPrioritization, PrioritizationUpdateDto>().ReverseMap();
		CreateMap<OperationalPrioritization, PrioritizationViewDto>().ReverseMap();
	}
}
using Barnamenevisan.Localizing.Repository;
using Kidemy.Domain.Interfaces.Slider;
using Kidemy.Infra.Data.Context;

namespace Kidemy.Infra.Data.Repositories.Slider
{
    public class SliderRepository : EfRepository<Domain.Models.Slider.Slider, int>, ISliderRepository
    {
        public SliderRepository(KidemyContext context) : base(context)
        {

        }
    }
}

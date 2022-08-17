using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.Reception.config
{
    public class Mapper : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<AddUserRequest, UserMessage>()
                .Map(dest => dest.Password, src => MD5Encryption.Encrypt(src.Password,false));
            config.ForType<Area, AreaResponse>()
                .Map(dest => dest.Label, src => src.Name)
                .Map(dest=>dest.Value, src => src.AreaId);
            config.ForType<City, AreaResponse>()
                .Map(dest => dest.Label, src => src.Name)
                .Map(dest => dest.Value, src => src.CityId);
            config.ForType<Province, AreaResponse>()
                .Map(dest => dest.Label, src => src.Name)
                .Map(dest => dest.Value, src => src.ProvinceId);
        }
    }
}

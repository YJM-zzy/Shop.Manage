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
        }
    }
}

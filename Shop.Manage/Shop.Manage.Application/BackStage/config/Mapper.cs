using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Manage.Application.BackStage.config
{
    public class Mapper:IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.ForType<AddBusiness, BusinessMessage>()
                .Map(Ts => Ts.Password, Td => MD5Encryption.Encrypt(Td.Password,false));
        }
    }
}

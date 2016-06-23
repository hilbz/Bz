using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bz.Application.Services.Dto
{
    public class IdInput<TId>:IInputDto
    {
        public TId Id { get; set; }

        public IdInput()
        {

        }

        public IdInput(TId id)
        {
            Id = id;
        }
    }
}

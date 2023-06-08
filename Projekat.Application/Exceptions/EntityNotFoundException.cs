using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Application.Exceptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(int id, Type type) : base($"Entity of type {type.Name} with an id of {id} was not found")
        {

        }
    }
}

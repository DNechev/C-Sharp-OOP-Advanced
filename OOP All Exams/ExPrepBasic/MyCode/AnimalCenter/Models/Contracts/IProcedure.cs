using System.Collections;
using System.Collections.Generic;

namespace AnimalCentre.Models.Contracts
{
    public interface IProcedure
    {
        ICollection<Animal> ProcedureHistory  { get;}
    }
}

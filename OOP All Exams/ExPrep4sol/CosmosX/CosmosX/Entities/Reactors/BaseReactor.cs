using CosmosX.Entities.Containers.Contracts;
using CosmosX.Entities.Modules.Absorbing.Contracts;
using CosmosX.Entities.Modules.Energy.Contracts;
using CosmosX.Entities.Reactors.Contracts;
using System.Text;

namespace CosmosX.Entities.Reactors
{
    public abstract class BaseReactor : IReactor
    {
        private readonly IContainer moduleContainer;
        private readonly int id;

        protected BaseReactor(int id, IContainer moduleContainer)
        {
            this.Id = id;
            this.moduleContainer = moduleContainer;
        }

        public int Id { get; }

        public virtual long TotalEnergyOutput 
            => this.moduleContainer.TotalEnergyOutput;

        public virtual long TotalHeatAbsorbing 
            => this.moduleContainer.TotalHeatAbsorbing;

        public int ModuleCount 
            => this.moduleContainer.ModulesByInput.Count;


        public void AddEnergyModule(IEnergyModule energyModule)
        {
            this.moduleContainer.AddEnergyModule(energyModule);
        }

        public void AddAbsorbingModule(IAbsorbingModule absorbingModule)
        {
            this.moduleContainer.AddAbsorbingModule((IAbsorbingModule)absorbingModule);
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            string result = sb.AppendLine($"{this.GetType().Name} - {this.Id}")
                .AppendLine($"Energy Output: {this.TotalEnergyOutput}")
                .AppendLine($"Heat Absorbing: {this.TotalHeatAbsorbing}")
                .AppendLine($"Modules: {this.ModuleCount}")
                .ToString()
                .Trim();

            return result;
        }
    }
}
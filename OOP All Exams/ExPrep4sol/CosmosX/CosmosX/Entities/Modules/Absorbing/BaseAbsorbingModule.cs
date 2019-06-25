using CosmosX.Entities.Modules.Absorbing.Contracts;

namespace CosmosX.Entities.Modules.Absorbing
{
    public abstract class BaseAbsorbingModule : BaseModule, IAbsorbingModule
    {
        protected BaseAbsorbingModule(int id, int heatAbsorbing)
            : base(id)
        {
            this.HeatAbsorbing = heatAbsorbing;
        }

        public int HeatAbsorbing { get; private set; }

        public override string ToString()
        {
            return base.ToString() + $"Heat Absorbing: {this.HeatAbsorbing}";
        }
    }
}
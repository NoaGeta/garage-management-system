namespace Ex03.GarageLogic
{
    public abstract class EnergySource
    {
        protected float m_CurrentEnergy;
        protected readonly float r_MaxEnergy;

        protected EnergySource(float i_CurrentEnergy, float i_MaxEnergy)
        {
            if (i_CurrentEnergy < 0 || i_CurrentEnergy > i_MaxEnergy)
            {
                throw new ValueRangeException(0, i_MaxEnergy);
            }

            m_CurrentEnergy = i_CurrentEnergy;
            r_MaxEnergy = i_MaxEnergy;
        }
        public float EnergyPercentage
        {
            get
            {
                return (m_CurrentEnergy / r_MaxEnergy) * 100;
            }
        }
        public float MaxEnergy
        {
            get { return r_MaxEnergy; }
        }

    }
}

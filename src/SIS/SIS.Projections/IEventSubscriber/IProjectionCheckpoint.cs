namespace SIS.Projections
{
    public interface IProjectionCheckpoint
    { 
        int? Value { get; }
        void Update(int? value);
    }
}

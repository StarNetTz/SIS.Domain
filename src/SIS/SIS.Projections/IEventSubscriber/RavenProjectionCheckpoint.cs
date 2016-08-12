
namespace SIS.Projections
{
    public class Checkpoint
    {
        public string Id { get; set; }
        public int? Value { get; set; }
    }



    class RavenProjectionCheckpoint : IProjectionCheckpoint
    {
        public string Id { get; set; }
        public int? Value { get; set; }

        public RavenProjectionCheckpoint(string id)
        {
            Id = id;
            Initialize();
        }

        private void Initialize()
        {
            using (var ses = RavenGlobal.DocumentStore.OpenSession())
            {
                var doc = ses.Load<Checkpoint>(Id);
                if (doc == null)
                {
                    doc = new Checkpoint() { Id = Id };
                    ses.Store(doc);
                    ses.SaveChanges();
                } 
                else
                {
                    Value = doc.Value;
                }
            }
        }

        public void Update(int? value)
        {
            Value = value;
            var doc = new Checkpoint() { Id = Id, Value = Value };
            using (var ses = RavenGlobal.DocumentStore.OpenSession())
            {
                ses.Store(doc);
                ses.SaveChanges();
            }
        }
    }
}

using SIS.PL.Commands;
using SIS.ReadModel;
using StarNet.DDD;

namespace SIS
{
    public class AddCompetitionValidator : Validator<AddCompetition>
    {
        ICompetitionReadModel ReadModel;

        public AddCompetitionValidator(ICompetitionReadModel readModel)
        {
            ReadModel = readModel;
        }

        public override void Validate(AddCompetition obj)
        {
            var r = GetValidationStatus(obj);
            if (!r.IsValid)
                r.ThrowFirstBrokenRuleAsDomainError();
        }

        private void ValidateName(AddCompetition obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Name))
                BrokenRules.Add("InvalidCompetitionName", "Competition name cannot be null, empty or whitespace only!");
        }

        public override ValidationResult GetValidationStatus(AddCompetition obj)
        {
            ValidateName(obj);
            ValidateNameUniqueness(obj);
            return new ValidationResult(BrokenRules);
        }

        private void ValidateNameUniqueness(AddCompetition obj)
        {  
            if (!ReadModel.IsCompetitionNameUnique(obj.Id, obj.Name))
                BrokenRules.Add("DuplicateCompetitionName", string.Format("Competition name \"{0}\" is already assigned to another competition!", obj.Name));
        }
    }
}

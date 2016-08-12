using SIS.PL.Commands;
using SIS.ReadModel;
using StarNet.DDD;

namespace SIS
{
    public class RenameCompetitionValidator : Validator<RenameCompetition>
    {
        ICompetitionReadModel ReadModel;

        public RenameCompetitionValidator(ICompetitionReadModel readModel)
        {
            ReadModel = readModel;
        }

        public override void Validate(RenameCompetition obj)
        {
            var r = GetValidationStatus(obj);
            if (!r.IsValid)
                r.ThrowFirstBrokenRuleAsDomainError();
        }

        public override ValidationResult GetValidationStatus(RenameCompetition obj)
        {
            ValidateName(obj);
            ValidateNameUniqueness(obj);
            return new ValidationResult(BrokenRules);
        }

        private void ValidateName(RenameCompetition obj)
        {
            if (string.IsNullOrWhiteSpace(obj.Name))
                BrokenRules.Add("InvalidCompetitionName", "Competition name cannot be null, emptyo whitespace only!");
        }

        private void ValidateNameUniqueness(RenameCompetition obj)
        {
            if (!ReadModel.IsCompetitionNameUnique(obj.Id, obj.Name))
            {
                BrokenRules.Add("DuplicateCompetitionName", string.Format("Competition name \"{0}\" is already assigned to another competition!", obj.Name));
            }
        }
    }
}

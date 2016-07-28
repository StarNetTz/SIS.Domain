using SIS.PL;
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
                BrokenRules.Add("InvalidCompetitionName", "Competition name cannot be null, emptyo whitespace only!");
        }

        public override ValidationResult GetValidationStatus(AddCompetition obj)
        {
            ValidateName(obj);
            ValidateNameUniqueness(obj);
            return new ValidationResult(BrokenRules);
        }

        private void ValidateNameUniqueness(AddCompetition obj)
        {
            var cmpt = ReadModel.GetByName(obj.Name);
            if ((cmpt != null) && (cmpt.Id != obj.Id) && (cmpt.Name == obj.Name))
            {
                BrokenRules.Add("DuplicateCompetitionName", string.Format("Competition name \"{0}\" is already assigned to another competition!", cmpt.Name));
            }
        }
    }

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

        public override ValidationResult GetValidationStatus (RenameCompetition obj)
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
            var cmpt = ReadModel.GetByName(obj.Name);
            if ((cmpt != null) && (cmpt.Id != obj.Id) && (cmpt.Name == obj.Name))
            {
                BrokenRules.Add("DuplicateCompetitionName", string.Format("Competition name \"{0}\" is already assigned to another competition!", cmpt.Name));
            }
        }
    }

}

namespace Sibir.Domain.Shared.ViewModels
{
    public record ProjectCreateViewModel
    (
        string Title,
        string CompanyConsumer,
        string CompanyExecuter,
        string DateOfStart,
        string DateOfFinish,
        int Priority
    );
}

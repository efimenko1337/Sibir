namespace Sibir.BL.ViewModels
{
    public record ProjectViewViewModel
    (
        Guid Id,
        string Tittle,
        string ExecuterCompany,
        string ConsumerComapny,
        string DateOfStart,
        string DateOfFinish,
        int Priority,
        Guid ManagerId,
        string ManagerName
    );
}

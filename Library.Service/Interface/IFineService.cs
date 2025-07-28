using Library.Common.Models;


namespace Library.Service.Interface
{
    public interface IFineService
    {
        public IEnumerable<StudentFineGrideModel> GetAllStudentFines();
        public bool MarkFinePaid(Guid id, string status);
    }
}

using Library.Common.Models;

namespace Library.Manager.Interface
{
    public interface IFineManager
    {
        public IEnumerable<StudentFineGrideModel> GetAllStudentFines();
        public bool MarkFinePaid(Guid id, string status);
    }
}

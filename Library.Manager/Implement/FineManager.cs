using Library.Common.Models;
using Library.Manager.Interface;
using Library.Service.Interface;

namespace Library.Manager.Implement
{
    public class FineManager : IFineManager
    {
        private readonly IFineService _fineService;
        public FineManager(IFineService fineService)
        {
            _fineService = fineService;
        }

        public IEnumerable<StudentFineGrideModel> GetAllStudentFines()
        {
            return _fineService.GetAllStudentFines();
        }

        public IEnumerable<StudentFineGrideModel> GetStudentFinesById(string id)
        {
            return _fineService.GetStudentFinesById(id);
        }

        public bool MarkFinePaid(Guid id, string status)
        {
            return _fineService.MarkFinePaid(id, status);
        }
    }
}

using System.Threading.Tasks;
using Application.Services.Students;
using Application.Services.Students.Responses;
using Domain.Search;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace WebApp.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly IStudentService _studentService;

        public PaginatedList<StudentResponse> Students { get; set; }

        public IndexModel(IStudentService studentService)
        {
            _studentService = studentService;
        }

        public async Task OnGet(string sortOrder,
                                string currentFilter,
                                string searchString,
                                int?   pageNumber)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["NameSortParm"] = string.IsNullOrEmpty(sortOrder)
                                           ? "name_desc"
                                           : "";
            ViewData["DateSortParm"] = sortOrder == "Date"
                                           ? "date_desc"
                                           : "Date";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewData["CurrentFilter"] = searchString;

            const int pageSize  = 3;
            var       pageIndex = pageNumber ?? 1;

            var searchArgs = new SearchArgs
            {
                SearchText = searchString,
                Offset     = (pageIndex - 1) * pageSize,
                Limit      = pageSize,
                SortOption = new SortOptionArgs()
            };

            switch (sortOrder)
            {
                case "name_desc":
                    searchArgs.SortOption.SortOrder    = SortOrder.Descending;
                    searchArgs.SortOption.PropertyName = "LastName";
                    break;
                case "Date":
                    searchArgs.SortOption.SortOrder    = SortOrder.Ascending;
                    searchArgs.SortOption.PropertyName = "EnrollmentDate";
                    break;
                case "date_desc":
                    searchArgs.SortOption.SortOrder    = SortOrder.Descending;
                    searchArgs.SortOption.PropertyName = "EnrollmentDate";
                    break;
                default:
                    searchArgs.SortOption.SortOrder    = SortOrder.Ascending;
                    searchArgs.SortOption.PropertyName = "LastName";
                    break;
            }

            var students = await _studentService.SearchStudentsAsync(searchArgs);

            Students = PaginatedList<StudentResponse>.CreateAsync(students.Values, students.RecordsTotal, pageIndex, pageSize);
        }
    }
}

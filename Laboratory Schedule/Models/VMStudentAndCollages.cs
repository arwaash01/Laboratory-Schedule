using Microsoft.AspNetCore.Mvc.Rendering;

namespace Laboratory_Schedule.Models
{
    public class VMStudentAndCollages
    {
        public Request Request { get; set; }

        public SelectList CollagesSelectList  { get; set; }

        public SelectList AvailablDates { get; set; }

    }
}

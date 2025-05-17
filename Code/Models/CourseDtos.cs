// DTOs for Course CRUD
namespace dotnet9WebAPI.ai.gen.DTOs
{
    public class CourseCreateDto
    {
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
    }

    public class CourseReadDto
    {
        public int CourseId { get; set; }
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
    }

    public class CourseUpdateDto
    {
        public string Title { get; set; } = string.Empty;
        public int Credits { get; set; }
    }
}

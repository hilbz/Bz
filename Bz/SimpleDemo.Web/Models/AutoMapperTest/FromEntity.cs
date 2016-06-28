using AutoMapper;

namespace SimpleDemo.Web.Models
{
    public class SourceEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }


    public class DestEntity
    {
        public int Id { get; set; }

        public string Name { get; set; }

    }

    public static class InitailAutoMapper
    {
        public static void Initail()
        {
            Mapper.CreateMap<SourceEntity, DestEntity>();
        }
    }
}
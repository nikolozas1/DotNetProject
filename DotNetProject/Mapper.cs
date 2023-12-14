using Riok.Mapperly.Abstractions;

namespace DotNetProject
{
    [Mapper]
    public partial class Mapper
    {
        public partial Book DTOToBook(BookDTO book);
    }
}

using ManagementCompanyAPI.Models;

namespace ManagementCompanyAPI.Interfaces
{
    public interface ICatalogRepository
    {
        ICollection<Catalog> GetCatalogs();
        Catalog GetCatalog(int Id);

        bool CatalogExists(int Id);
        bool CreateCatalog(Catalog catalog);
        bool UpdateCatalog(Catalog catalog);
        bool DeleteCatalog(Catalog catalog);
        bool Save();
    }
}

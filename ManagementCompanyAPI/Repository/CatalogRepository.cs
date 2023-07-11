using AutoMapper;
using ManagementCompanyAPI.Data;
using ManagementCompanyAPI.Interfaces;
using ManagementCompanyAPI.Models;
using System.Diagnostics.Eventing.Reader;

namespace ManagementCompanyAPI.Repository
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CatalogRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public bool CatalogExists(int id)
        {
            return _context.Catalogs.Any(c => c.Id == id);
        }

        public bool CreateCatalog(Catalog catalog)
        {
            _context.Add(catalog);
            return Save();
        }

        public bool DeleteCatalog(Catalog catalog)
        {
            _context.Remove(catalog);
            return Save();
        }

        public ICollection<Catalog> GetCatalogs()
        {
                return _context.Catalogs.ToList();
        }

        public Catalog GetCatalog(int id)
        {
            return _context.Catalogs.Where(c => c.Id == id).FirstOrDefault();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool UpdateCatalog(Catalog catalog)
        {
            _context.Update(catalog);
            return Save();
        }
    }
}

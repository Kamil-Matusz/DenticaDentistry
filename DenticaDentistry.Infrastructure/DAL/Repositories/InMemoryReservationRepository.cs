using Dentica_Dentistry.Core.Entities;
using Dentica_Dentistry.Core.Repositories;

namespace Dentica_Dentistry.Application.Repositories;

internal class InMemoryReservationRepository : IReservationRepository
{
    private readonly List<DentistIndustry> _dentistIndustries;

    public InMemoryReservationRepository()
    {
        _dentistIndustries = new (){
            new DentistIndustry(1, "Lakierowanie zębow", 100.00, "Naprawa szkliwa zębów"),
            new DentistIndustry(2, "Czyszczenie Kamienia", 200.00, "Czyszczenie zębów z nasady kamieniowej"),
            new DentistIndustry(3, "Leczenie Kanałowe", 100.00, "Usuwanie obumarłych fragmentów zębów"),
            new DentistIndustry(4, "Leczenie Próchnicy", 100.00, "Usuwanie prochnicy z zęba"),
            new DentistIndustry(5, "Wizyta Kontrolna", 50.00, "Kontrolne badanie zębów"),
        };
    }

    public IEnumerable<DentistIndustry> GetAllReservation() => _dentistIndustries;

    public DentistIndustry GetReservation(int id) => _dentistIndustries.SingleOrDefault(x => x.DentistIndustryId == id);

    public void Add(DentistIndustry dentistIndustry) => _dentistIndustries.Add(dentistIndustry);

    public void Update(DentistIndustry dentistIndustry)
    {
        throw new NotImplementedException();
    }

    public void Delete(DentistIndustry dentistIndustry) => _dentistIndustries.Remove(dentistIndustry);
}
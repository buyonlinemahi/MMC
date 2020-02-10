using System.Collections.Generic;

namespace MMCApp.Domain.Models.ClientModel
{
    public class PatientClientDetailViewModel
    {
        public IEnumerable<ClientEmployer> ClientEmployerDetails { get; set; }
        public int cEmpTotalCount { get; set; }

        public IEnumerable<ClientInsurer> ClientInsurerDetails { get; set; }
        public int cinsTotalCount { get; set; }

        public IEnumerable<ClientManagedCareCompany> ClientManagedCareCompanyDetails { get; set; }
        public int cMmcTotalCount { get; set; }

        public IEnumerable<ClientThirdPartyAdministrator> ClientThirdPartyAdministratorDetails { get; set; }
        public int cTpaTotalCount { get; set; }

        public Client ClientDetail { get; set; }

        public IEnumerable<ClaimAdministratorAllByClientID> ClaimAdministratorAllByClientIDDetails { get; set; }

    }
}

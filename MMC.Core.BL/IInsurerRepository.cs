using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IInsurerRepository
    {
        int addInsurer(Insurer _insurer);
        int updateInsurer(Insurer _insurer);
        void deleteInsurer(int _insurerId);
        IEnumerable<Insurer> getInsurersAll();
        Insurer getInsurerByID(int _insurerId);
        BLModel.Paged.Insurer getInsurersByName(string SearchText, int _skip, int _take);

        #region InsuranceBranch
        int addInsuranceBranch(InsuranceBranch _insuranceBranch);
        int updateInsuranceBranch(InsuranceBranch _insuranceBranch);
        void deleteInsuranceBranch(int _insuranceBranchID);
        BLModel.Paged.InsuranceBranch getInsuranceBranchesByInsurerID(int _insurerID, int _skip, int _take);
        IEnumerable<InsuranceBranch> getInsuranceBranchesAllByInsurerID(int _insurerID);
        InsuranceBranch getInsuranceBranchByID(int _insuranceBranchID);
        #endregion
    }
}

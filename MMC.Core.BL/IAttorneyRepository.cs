using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;


namespace MMC.Core.BL
{
    public interface IAttorneyRepository
    {
        #region Attorney
        int addAttorney(Attorney _attorney);
        int updateAttorney(Attorney _attorney);
        void deleteAttorney(int _attorneyID);
        IEnumerable<Attorney> getAttorneyAll();
        Attorney getAttorneyByID(int _id);
        BLModel.Paged.Attorney getAttorneyByAttorneyFirmID(int _attorneyFirmID, int _skip, int _take);
        BLModel.Paged.Attorney getAttorneyRecordsByFirmTypeID(int _attorneyFirmTypeID);
        #endregion
        #region AttorneyFirm
        int addAttorneyFirm(AttorneyFirm _attorneyFirm);
        int updateAttorneyFirm(AttorneyFirm _attorneyFirm);
        void deleteAttorneyFirm(int _attorneyFirmID);
        IEnumerable<AttorneyFirm> getAttorneyFirmAll();
        AttorneyFirm getAttorneyFirmByID(int _id);
        BLModel.Paged.AttorneyFirm getAttorneyAndAttorneyFirmByName(string _searchText, int _skip, int _take);
        #endregion
    }
}

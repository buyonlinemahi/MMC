using MMC.Core.Data.Model;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BL
{
    public interface IMedicalGroupRepository
    {
        int addMedicalGroup(MedicalGroup _medicalGroup);
        int updateMedicalGroup(MedicalGroup _medicalGroup);
        void deleteMedicalGroup(int _medicalGroupId);
        IEnumerable<MedicalGroup> getMedicalGroupsAll();
        MedicalGroup getMedicalGroupByID(int _medicalGroupId);
        BLModel.Paged.MedicalGroup getMedicalGroupsByName(string SearchText, int _skip, int _take);
    }
}

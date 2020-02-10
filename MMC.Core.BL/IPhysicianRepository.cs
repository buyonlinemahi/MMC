using MMC.Core.Data.Model;
using System;
using System.Collections.Generic;
using BLModel = MMC.Core.BL.Model;


namespace MMC.Core.BL
{
    public interface IPhysicianRepository 
    {
        int addPhysician(Physician _physician);
        int updatePhysican(Physician _physician);
        void deletePhysican(int _physicanID);
        IEnumerable<Physician> getPhysicianAll();
        BLModel.Physician getPhysicianByID(int _id);
        BLModel.Paged.Physician getPhysicianByName(string SearchText, int _skip, int _take);
        BLModel.Paged.Physician getPhysicianByNPI(string SearchText, int _skip, int _take);
        BLModel.Paged.Physician getPhysicianBySpeciality(Int32 SearchID, int _skip, int _take);
    }
}

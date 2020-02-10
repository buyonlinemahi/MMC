using MMC.Core.BL;
using MMC.Core.Data.Model;
using MMC.Core.Data.SQLServer;
using Omu.ValueInjecter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using BLModel = MMC.Core.BL.Model;

namespace MMC.Core.BLImplementation
{
    public class PhysicianRepository : IPhysicianRepository
    {
        private readonly BaseRepository<Physician> _physicianRepository;
        public PhysicianRepository()
        {
            _physicianRepository = new BaseRepository<Physician>();
        }

        public int addPhysician(Physician _physician)
        {
            return _physicianRepository.Add(_physician).PhysicianId;
        }

        public int updatePhysican(Physician _physician)
        {
            return _physicianRepository.Update(_physician);
        }

        public void deletePhysican(int _physicanID)
        {
            _physicianRepository.Delete(_physicanID);
        }

        public IEnumerable<Physician> getPhysicianAll()
        {
            return _physicianRepository.GetAll();
        }

        public BLModel.Physician getPhysicianByID(int _id)
        {
            return getPhysicianByIDIntake(phy => phy.PhysicianId == _id);
        }

        public BLModel.Physician getPhysicianByIDIntake(Expression<Func<Physician, bool>> _where)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _physicanByName = (from phys in _MMCDbContext.physicians.Where(_where)
                                   join spty in _MMCDbContext.specialties
                                   on phys.PhysSpecialtyId equals spty.SpecialtyID
                                   join st in _MMCDbContext.states
                                   on phys.PhysStateId equals st.StateId
                                   select new
                                   {
                                       phys.PhysAddress1,
                                       phys.PhysAddress2,
                                       phys.PhysCity,
                                       phys.PhysEMail,
                                       phys.PhysFax,
                                       phys.PhysFirstName,
                                       phys.PhysicianId,
                                       phys.PhysLastName,
                                       phys.PhysNotes,
                                       phys.PhysNPI,
                                       phys.PhysPhone,
                                       phys.PhysQualification,
                                       phys.PhysStateId,
                                       phys.PhysSpecialtyId,
                                       phys.PhysZip,
                                       PhysSpecialtyName = spty.SpecialtyName,
                                       PhysStateName = st.StateName
                                   }).OrderByDescending(phys => phys.PhysicianId).ToList();
            return _physicanByName.Select(phys => new BLModel.Physician().InjectFrom(phys)).Cast<BLModel.Physician>().SingleOrDefault();
        }

        public BLModel.Paged.Physician getPhysicianByName(string SearchText, int _skip, int _take)
        {
            return getPhysicianRecords(phys => phys.PhysFirstName.StartsWith(SearchText) || phys.PhysLastName.StartsWith(SearchText) || ((phys.PhysFirstName).Trim() + " " + (phys.PhysLastName).Trim()).StartsWith(SearchText), _skip, _take);
        }

        public BLModel.Paged.Physician getPhysicianByNPI(string SearchText, int _skip, int _take)
        {
            return getPhysicianRecords(phys => phys.PhysNPI.StartsWith(SearchText), _skip, _take);
        }

        public BLModel.Paged.Physician getPhysicianBySpeciality(Int32 SearchID, int _skip, int _take)
        {
            return getPhysicianRecords(phys => phys.PhysSpecialtyId == SearchID, _skip, _take);
        }

        private BLModel.Paged.Physician getPhysicianRecords(Expression<Func<Physician, bool>> _where, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _physicanByName = (from phys in _MMCDbContext.physicians.Where(_where)
                                   join spty in _MMCDbContext.specialties
                                   on phys.PhysSpecialtyId equals spty.SpecialtyID
                                   join st in _MMCDbContext.states
                                   on phys.PhysStateId equals st.StateId
                                   select new
                                   {
                                       phys.PhysAddress1,
                                       phys.PhysAddress2,
                                       phys.PhysCity,
                                       phys.PhysEMail,
                                       phys.PhysFax,
                                       phys.PhysFirstName,
                                       phys.PhysicianId,
                                       phys.PhysLastName,
                                       phys.PhysNotes,
                                       phys.PhysNPI,
                                       phys.PhysPhone,
                                       phys.PhysQualification,
                                       phys.PhysSpecialtyId,
                                       phys.PhysZip,
                                       phys.PhysStateId,
                                       PhysSpecialtyName = spty.SpecialtyName,
                                       PhysStateName = st.StateName
                                   }).OrderByDescending(phys => phys.PhysicianId).ToList();

            return new BLModel.Paged.Physician
            {
                PhysicianDetails = _physicanByName.Select(phys => new BLModel.Physician().InjectFrom(phys)).Cast<BLModel.Physician>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _physicanByName.Count()
            };
        }
    }
}
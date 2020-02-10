using MMC.Core.BL;
using MMC.Core.BLImplementation.SPImplementation;
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
    public class AttorneyRepository : IAttorneyRepository
    {
        private readonly BaseRepository<Attorney> _attorneyRepository;
        private readonly BaseRepository<AttorneyFirm> _attorneyFirmRepository;
        private readonly BaseRepository<AttorneyFirmType> _attorneyFirmTypeRepository;

        public AttorneyRepository()
        {
            _attorneyRepository = new BaseRepository<Attorney>();
            _attorneyFirmRepository = new BaseRepository<AttorneyFirm>();
            _attorneyFirmTypeRepository = new BaseRepository<AttorneyFirmType>();
        }

        #region Attorney
        public int addAttorney(Attorney _attorney)
        {
            return _attorneyRepository.Add(_attorney).AttorneyID;
        }

        public int updateAttorney(Attorney _attorney)
        {
            return _attorneyRepository.Update(_attorney);
        }

        public void deleteAttorney(int _attorneyID)
        {
            _attorneyRepository.Delete(_attorneyID);
        }

        public IEnumerable<Attorney> getAttorneyAll()
        {
            return _attorneyRepository.GetAll();
        }

        public Attorney getAttorneyByID(int _id)
        {
            return _attorneyRepository.GetById(_id);
        }

        public BLModel.Paged.Attorney getAttorneyByAttorneyFirmID(int _attorneyFirmID, int _skip, int _take)
        {

            return getAttorneyRecords(attorney => attorney.AttorneyFirmID == _attorneyFirmID, _skip, _take);
        }

        public BLModel.Paged.Attorney getAttorneyRecords(Expression<Func<Attorney, bool>> _where, int _skip, int _take)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _attorneyRecordsByAttorneyFirmID = (from attroneys in _MMCDbContext.attorneys.Where(_where)
                                                    select new
                                                    {
                                                        attroneys.AttorneyID,
                                                        attroneys.AttorneyFirmID,
                                                        attroneys.AttorneyName,
                                                        attroneys.AttPhone,
                                                        attroneys.AttFax,
                                                        attroneys.AttEmail
                                                    }).OrderByDescending(atto => atto.AttorneyID).ToList();

            return new BLModel.Paged.Attorney
            {
                AttorneyDetails = _attorneyRecordsByAttorneyFirmID.Select(att => new BLModel.Attorney().InjectFrom(att)).Cast<BLModel.Attorney>().Skip(_skip).Take(_take).ToList(),
                TotalCount = _attorneyRecordsByAttorneyFirmID.Count()
            };
        }

        public BLModel.Paged.Attorney getAttorneyRecordsByFirmTypeID(int _attorneyFirmTypeID)
        {
            MMCDbContext _MMCDbContext = new MMCDbContext();
            var _attorneyByFirmTypeID = (from attFirms in _MMCDbContext.attorneyFirms
                                                  join att in _MMCDbContext.attorneys
                                                  on attFirms.AttorneyFirmID equals att.AttorneyFirmID
                                                  where (attFirms.AttorneyFirmTypeID == _attorneyFirmTypeID)
                                                  select new
                                                  {
                                                      att.AttorneyID,
                                                      att.AttorneyFirmID,
                                                      att.AttorneyName,
                                                      att.AttPhone,
                                                      att.AttFax,
                                                      att.AttEmail
                                                  }).OrderByDescending(att => att.AttorneyID).ToList();
            return new BLModel.Paged.Attorney
            {
                AttorneyDetails = _attorneyByFirmTypeID.Select(att => new BLModel.Attorney().InjectFrom(att)).Cast<BLModel.Attorney>().ToList(),
                TotalCount = _attorneyByFirmTypeID.Count()
            };
        }
        #endregion  

        #region AttorneyFirm
        public int addAttorneyFirm(AttorneyFirm _attorneyFirm)
        {
            return _attorneyFirmRepository.Add(_attorneyFirm).AttorneyFirmID;
        }

        public int updateAttorneyFirm(AttorneyFirm _attorneyFirm)
        {
            return _attorneyFirmRepository.Update(_attorneyFirm);
        }

        public void deleteAttorneyFirm(int _attorneyFirmID)
        {
            _attorneyFirmRepository.Delete(_attorneyFirmID);
        }

        public IEnumerable<AttorneyFirm> getAttorneyFirmAll()
        {
            return _attorneyFirmRepository.GetAll();
        }

        public AttorneyFirm getAttorneyFirmByID(int _id)
        {
            return _attorneyFirmRepository.GetById(_id);
        }
        
            

        public BLModel.Paged.AttorneyFirm getAttorneyAndAttorneyFirmByName(string _searchText, int _skip, int _take)
        {
            SPImpl _spImpl = new SPImpl();            
            return new BLModel.Paged.AttorneyFirm
            {
                AttorneyFirmDetails = _spImpl.getAttorneyFirmByAttorneyNameorFirm(_searchText,_skip,_take).ToList(),
                TotalCount = _spImpl.getAttorneyFirmByAttorneyNameorFirmCOUNT(_searchText)
            };
        }
        #endregion
    }
}

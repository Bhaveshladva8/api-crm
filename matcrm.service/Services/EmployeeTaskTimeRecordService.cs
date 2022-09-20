using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using matcrm.data;
using matcrm.data.Models.Dto;
using matcrm.data.Models.Tables;

namespace matcrm.service.Services
{
    public partial class EmployeeTaskTimeRecordService : Service<EmployeeTaskTimeRecord>, IEmployeeTaskTimeRecordService
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        public EmployeeTaskTimeRecordService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<EmployeeTaskTimeRecord> CheckInsertOrUpdate(EmployeeTaskTimeRecordDto timeRecordDto)
        {
            var employeeTaskTimeRecordObj = _mapper.Map<EmployeeTaskTimeRecord>(timeRecordDto);
            // var existingItem = _unitOfWork.EmployeeTaskTimeRecordRepository.GetMany (t => t.TenantName == tenant.TenantName && t.IsDeleted == false && t.IsBlocked == false).Result.FirstOrDefault ();
            // var existingItem = _unitOfWork.EmployeeTaskTimeRecordRepository.GetMany(t => t.EmployeeTaskId == timeRecordDto.EmployeeTaskId && t.IsDeleted == false).Result.FirstOrDefault();

            return await InsertEmployeeTaskTimeRecord(employeeTaskTimeRecordObj);
            // if (existingItem != null)
            // {
            //     existingItem.Duration = timeRecordDto.Duration;
            //     return await UpdateEmployeeTaskTimeRecord(existingItem);
            // }
            // else
            // {
            //     return await InsertEmployeeTaskTimeRecord(timeRecord);
            // }
        }

        public async Task<EmployeeTaskTimeRecord> InsertEmployeeTaskTimeRecord(EmployeeTaskTimeRecord employeeTaskTimeRecordObj)
        {
            employeeTaskTimeRecordObj.CreatedOn = DateTime.UtcNow;
            var newItem = await _unitOfWork.EmployeeTaskTimeRecordRepository.AddAsync(employeeTaskTimeRecordObj);
            await _unitOfWork.CommitAsync();

            return newItem;
        }

        public async Task<EmployeeTaskTimeRecord> UpdateEmployeeTaskTimeRecord(EmployeeTaskTimeRecord employeeTaskTimeRecord)
        {
            // EmployeeTaskTimeRecord.CreatedOn = DateTime.UtcNow;
            var newItem = await _unitOfWork.EmployeeTaskTimeRecordRepository.UpdateAsync(employeeTaskTimeRecord, employeeTaskTimeRecord.Id);
            await _unitOfWork.CommitAsync();

            return newItem;
        }

        public long GetTotalEmployeeTaskTimeRecord(long EmployeeTaskId)
        {
            var timeRecords = _unitOfWork.EmployeeTaskTimeRecordRepository.GetMany(t => t.EmployeeTaskId == EmployeeTaskId && t.IsDeleted == false).Result.ToList();
            var total = timeRecords.Sum(t => t.Duration).Value;
            return total;
        }

        public async Task<List<EmployeeTaskTimeRecord>> DeleteTimeRecordByTaskId(long EmployeeTaskId)
        {
            var employeeTaskTimeRecordsList = _unitOfWork.EmployeeTaskTimeRecordRepository.GetMany(t => t.EmployeeTaskId == EmployeeTaskId && t.IsDeleted == false).Result.ToList();
            if (employeeTaskTimeRecordsList != null && employeeTaskTimeRecordsList.Count() > 0)
            {
                foreach (var item in employeeTaskTimeRecordsList)
                {
                    item.IsDeleted = true;
                    item.DeletedOn = DateTime.UtcNow;
                    await _unitOfWork.EmployeeTaskTimeRecordRepository.UpdateAsync(item, item.Id);
                }
                await _unitOfWork.CommitAsync();
            }
            return employeeTaskTimeRecordsList;
        }
    }

    public partial interface IEmployeeTaskTimeRecordService : IService<EmployeeTaskTimeRecord>
    {
        Task<EmployeeTaskTimeRecord> CheckInsertOrUpdate(EmployeeTaskTimeRecordDto timeRecordDto);
        long GetTotalEmployeeTaskTimeRecord(long taskId);
        Task<List<EmployeeTaskTimeRecord>> DeleteTimeRecordByTaskId(long TaskId);
    }
}
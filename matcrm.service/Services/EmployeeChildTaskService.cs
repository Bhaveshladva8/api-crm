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
    public partial class EmployeeChildTaskService : Service<EmployeeChildTask>, IEmployeeChildTaskService
    {
        private IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public EmployeeChildTaskService(IUnitOfWork unitOfWork,
            IMapper mapper) : base(unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public EmployeeChildTask CheckInsertOrUpdate(EmployeeChildTaskDto model)
        {
            var employeeChildTaskObj = _mapper.Map<EmployeeChildTask>(model);
            var existingItem = _unitOfWork.EmployeeChildTaskRepository.GetMany(t => t.Id == employeeChildTaskObj.Id && t.IsActive == true && t.IsDeleted == false).Result.FirstOrDefault();
            if (existingItem == null)
            {
                return InsertChildTask(employeeChildTaskObj);
            }
            else
            {
                employeeChildTaskObj.CreatedBy = existingItem.CreatedBy;
                employeeChildTaskObj.CreatedOn = existingItem.CreatedOn;
                employeeChildTaskObj.Id = existingItem.Id;
                return UpdateChildTask(employeeChildTaskObj, existingItem.Id);
            }
        }

        public EmployeeChildTask InsertChildTask(EmployeeChildTask employeeChildTaskObj)
        {
            employeeChildTaskObj.CreatedOn = DateTime.UtcNow;
            var newItem = _unitOfWork.EmployeeChildTaskRepository.Add(employeeChildTaskObj);
            _unitOfWork.CommitAsync();
            return newItem;
        }

        public EmployeeChildTask UpdateChildTask(EmployeeChildTask updatedItem, long existingId)
        {
            updatedItem.UpdatedOn = DateTime.UtcNow;
            var update = _unitOfWork.EmployeeChildTaskRepository.UpdateAsync(updatedItem, existingId).Result;
            _unitOfWork.CommitAsync();

            return update;
        }

        public List<EmployeeChildTask> GetAllActive()
        {
            return _unitOfWork.EmployeeChildTaskRepository.GetMany(t => t.IsActive == true && t.IsDeleted == false).Result.ToList();
        }

        public List<EmployeeChildTask> GetAllActiveBySubTaskIds(List<long> SubTaskIds)
        {
            return _unitOfWork.EmployeeChildTaskRepository.GetMany(t => SubTaskIds.Contains(t.EmployeeSubTaskId.Value) && t.IsActive == true && t.IsDeleted == false).Result.ToList();
        }

        public List<EmployeeChildTask> GetAll()
        {
            return _unitOfWork.EmployeeChildTaskRepository.GetMany(t => t.IsDeleted == false).Result.ToList();
        }

        public List<EmployeeChildTask> GetAllChildTaskBySubTask(long SubTaskId)
        {
            return _unitOfWork.EmployeeChildTaskRepository.GetMany(t => t.EmployeeSubTaskId == SubTaskId && t.IsActive == true && t.IsDeleted == false).Result.ToList();
        }

        public EmployeeChildTask GetChildTaskById(long ChildTaskId)
        {
            return _unitOfWork.EmployeeChildTaskRepository.GetMany(t => t.Id == ChildTaskId && t.IsActive == true && t.IsDeleted == false).Result.FirstOrDefault();
        }

        public async Task<EmployeeChildTask> Delete(long ChildTaskId)
        {
            var employeeChildTaskObj = _unitOfWork.EmployeeChildTaskRepository.GetMany(t => t.Id == ChildTaskId && t.IsDeleted == false).Result.FirstOrDefault();
            if (employeeChildTaskObj != null)
            {
                employeeChildTaskObj.IsDeleted = true;
                employeeChildTaskObj.DeletedOn = DateTime.UtcNow;
                await _unitOfWork.EmployeeChildTaskRepository.UpdateAsync(employeeChildTaskObj, employeeChildTaskObj.Id);
                await _unitOfWork.CommitAsync();
            }

            return employeeChildTaskObj;
        }
    }

    public partial interface IEmployeeChildTaskService : IService<EmployeeChildTask>
    {
        EmployeeChildTask CheckInsertOrUpdate(EmployeeChildTaskDto model);
        List<EmployeeChildTask> GetAll();
        List<EmployeeChildTask> GetAllActive();
        List<EmployeeChildTask> GetAllActiveBySubTaskIds(List<long> SubTaskIds);
        List<EmployeeChildTask> GetAllChildTaskBySubTask(long SubTaskId);
        Task<EmployeeChildTask> Delete(long ChildTaskId);
        EmployeeChildTask GetChildTaskById(long ChildTaskId);
    }
}
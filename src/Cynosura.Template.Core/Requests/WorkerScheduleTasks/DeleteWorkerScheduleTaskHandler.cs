using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using Cynosura.Core.Data;
using Cynosura.Core.Services;
using Cynosura.Template.Core.Entities;

namespace Cynosura.Template.Core.Requests.WorkerScheduleTasks
{
    public class DeleteWorkerScheduleTaskHandler : IRequestHandler<DeleteWorkerScheduleTask>
    {
        private readonly IEntityRepository<WorkerScheduleTask> _workerScheduleTaskRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public DeleteWorkerScheduleTaskHandler(IEntityRepository<WorkerScheduleTask> workerScheduleTaskRepository,
            IUnitOfWork unitOfWork,
            IStringLocalizer<SharedResource> localizer)
        {
            _workerScheduleTaskRepository = workerScheduleTaskRepository;
            _unitOfWork = unitOfWork;
            _localizer = localizer;
        }

        public async Task<Unit> Handle(DeleteWorkerScheduleTask request, CancellationToken cancellationToken)
        {
            var workerScheduleTask = await _workerScheduleTaskRepository.GetEntities()
                .Where(e => e.Id == request.Id)
                .FirstOrDefaultAsync();
            if (workerScheduleTask == null)
            {
                throw new ServiceException(_localizer["{0} {1} not found", _localizer["Worker Schedule Task"], request.Id]);
            }
            _workerScheduleTaskRepository.Delete(workerScheduleTask);
            await _unitOfWork.CommitAsync();
            return Unit.Value;
        }

    }
}
